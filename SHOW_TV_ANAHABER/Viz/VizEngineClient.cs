using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SHOW_TV_ANAHABER.Viz
{
    /// <summary>
    /// Viz Engine'e TCP (varsayılan port 6100) üzerinden komut gönderen ince istemci.
    ///
    /// Protokol çok basittir:
    ///   - Her komut düz metindir ve sonunda NUL (0x00) karakteri bulunur.
    ///   - Komutun önüne bir kimlik numarası konur: "12 MAIN_SCENE*TREE*/4/1*GEOM*TEXT GET"
    ///   - Engine cevabı aynı kimlikle, NUL ile bitirerek döner: "12 RAMAZAN'DA ..."
    ///   - Kimlik -1 verilirse Engine cevap göndermez (gönder-unut).
    ///
    /// Bu sınıf tek bağlantı tutar ve komutları sırayla gönderir (lock ile).
    /// Yayın uygulamalarında komutlar birbirine karışmasın diye bu yeterlidir.
    /// </summary>
    public sealed class VizEngineClient : IDisposable
    {
        private readonly object _sync = new object();
        private TcpClient _tcp;
        private NetworkStream _stream;
        private int _nextId;

        public string Host { get; }
        public int Port { get; }

        /// <summary>Bağlanma ve okuma için milisaniye cinsinden zaman aşımı.</summary>
        public int TimeoutMs { get; set; } = 3000;

        /// <summary>Engine metinleri UTF-8 gönderir/alır (3.14'te test edildi).</summary>
        public Encoding Encoding { get; set; } = new UTF8Encoding(false);

        public bool IsConnected
        {
            get
            {
                lock (_sync)
                {
                    return _tcp != null && _tcp.Connected;
                }
            }
        }

        /// <summary>Gönderilen her komut ve alınan cevap burada duyurulur (log için).</summary>
        public event Action<string, string> CommandLogged;

        public VizEngineClient(string host = "127.0.0.1", int port = 6100)
        {
            Host = host;
            Port = port;
        }

        public void Connect()
        {
            lock (_sync)
            {
                Disconnect();

                var tcp = new TcpClient();
                var ar = tcp.BeginConnect(Host, Port, null, null);
                if (!ar.AsyncWaitHandle.WaitOne(TimeoutMs))
                {
                    tcp.Close();
                    throw new TimeoutException("Viz Engine'e bağlanılamadı: " + Host + ":" + Port);
                }
                tcp.EndConnect(ar);

                _tcp = tcp;
                _stream = tcp.GetStream();
                _stream.ReadTimeout = TimeoutMs;
                _stream.WriteTimeout = TimeoutMs;
                _nextId = 0;
            }
        }

        public void Disconnect()
        {
            lock (_sync)
            {
                if (_stream != null) _stream.Dispose();
                if (_tcp != null) _tcp.Close();
                _stream = null;
                _tcp = null;
            }
        }

        /// <summary>
        /// Komutu gönderir ve Engine'in cevabını (kimlik numarası soyulmuş halde) döndürür.
        /// Engine hata verirse cevap "ERROR ..." ile başlar; bunu VizCommandException olarak fırlatırız.
        /// </summary>
        public string Send(string command)
        {
            return Send(command, TimeoutMs);
        }

        /// <summary>
        /// Uzun süren komutlar için (örn. "SCENE*... LOAD" saniyeler sürer) okuma zaman aşımını
        /// bu çağrıya özel verebilirsiniz.
        /// </summary>
        public string Send(string command, int readTimeoutMs)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Komut boş olamaz.", nameof(command));

            lock (_sync)
            {
                EnsureConnected();

                int id = ++_nextId;
                byte[] bytes = Encoding.GetBytes(id + " " + command + "\0");
                _stream.Write(bytes, 0, bytes.Length);
                _stream.Flush();

                int previousTimeout = _stream.ReadTimeout;
                string raw;
                try
                {
                    _stream.ReadTimeout = readTimeoutMs;
                    raw = ReadUntilNul();
                }
                finally
                {
                    _stream.ReadTimeout = previousTimeout;
                }
                string response = StripId(raw, id);

                CommandLogged?.Invoke(command, response);

                if (response.StartsWith("ERROR", StringComparison.OrdinalIgnoreCase))
                    throw new VizCommandException(command, response);

                return response;
            }
        }

        /// <summary>
        /// Cevap beklemeden gönderir (kimlik -1). Director START gibi hızlı ardışık komutlarda kullanışlıdır.
        /// </summary>
        public void SendNoReply(string command)
        {
            lock (_sync)
            {
                EnsureConnected();

                byte[] bytes = Encoding.GetBytes("-1 " + command + "\0");
                _stream.Write(bytes, 0, bytes.Length);
                _stream.Flush();
                CommandLogged?.Invoke(command, "(cevap beklenmedi)");
            }
        }

        /// <summary>Hata fırlatmayan sürüm; Engine komutu reddederse null döner.</summary>
        public string TrySend(string command)
        {
            try { return Send(command); }
            catch (VizCommandException) { return null; }
        }

        private void EnsureConnected()
        {
            if (_stream == null)
                throw new InvalidOperationException("Önce Connect() çağrılmalı.");
        }

        private string ReadUntilNul()
        {
            var buf = new byte[64 * 1024];
            var pending = new MemoryStream();

            while (true)
            {
                int n = _stream.Read(buf, 0, buf.Length);
                if (n <= 0)
                    throw new IOException("Viz Engine bağlantıyı kapattı.");

                int nul = Array.IndexOf(buf, (byte)0, 0, n);
                if (nul < 0)
                {
                    pending.Write(buf, 0, n);
                    continue;
                }

                pending.Write(buf, 0, nul);
                return Encoding.GetString(pending.ToArray());
            }
        }

        private static string StripId(string raw, int expectedId)
        {
            // Cevap "12 metin" biçimindedir. Baştaki kimliği ve bir boşluğu atarız.
            string prefix = expectedId + " ";
            if (raw.StartsWith(prefix, StringComparison.Ordinal))
                return raw.Substring(prefix.Length);

            // Boş cevaplarda sadece "12" gelebilir.
            if (raw == expectedId.ToString())
                return string.Empty;

            return raw;
        }

        public void Dispose()
        {
            Disconnect();
        }
    }

    public sealed class VizCommandException : Exception
    {
        public string Command { get; }
        public string Response { get; }

        public VizCommandException(string command, string response)
            : base("Viz komutu reddedildi.\nKomut: " + command + "\nCevap: " + response)
        {
            Command = command;
            Response = response;
        }
    }
}
