using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SHOW_TV_ANAHABER.Config
{
    /// <summary>
    /// Uygulama ayarları. exe'nin yanındaki appsettings.json dosyasında tutulur.
    /// Veritabanı bağlantı dizeleri boş bırakılır; kullanıcı kendi (SELECT yetkili) hesabını yazar.
    /// Bu dosya kaynak kontrolüne girmemeli (şifre içerir).
    /// </summary>
    [DataContract]
    public sealed class AppSettings
    {
        public const string FileName = "appsettings.json";

        // ---- Viz Engine'ler ------------------------------------------------------

        /// <summary>Ana reji makinesi (haber KJ'leri, logolar, flotlar).</summary>
        [DataMember(Order = 1)] public string EngineHost { get; set; } = "127.0.0.1";
        [DataMember(Order = 2)] public int EnginePort { get; set; } = 6100;

        /// <summary>Kelebek hedefleri: Viz #1 ve Viz #2 ayrı makineler olabilir. Test için ikisi de localhost.</summary>
        [DataMember(Order = 3)] public string Kelebek1Host { get; set; } = "127.0.0.1";
        [DataMember(Order = 4)] public int Kelebek1Port { get; set; } = 6100;
        [DataMember(Order = 5)] public string Kelebek2Host { get; set; } = "127.0.0.1";
        [DataMember(Order = 6)] public int Kelebek2Port { get; set; } = 6100;

        // ---- Program modu / kontrolcü --------------------------------------------------

        /// <summary>Kontrolcü adı ("Program Modu").</summary>
        [DataMember(Order = 10)] public string ControllerName { get; set; } = "ShowTVRejiAnaHaber";

        /// <summary>Kelebek playlist numaraları ve kelebek sahne kategorisi numarası.</summary>
        [DataMember(Order = 11)] public int KelebekPlaylist1 { get; set; } = 25;
        [DataMember(Order = 12)] public int KelebekPlaylist2 { get; set; } = 31;
        [DataMember(Order = 13)] public int KelebekGuiDirId { get; set; } = 14;

        // ---- Veri kaynağı ---------------------------------------------------------------------

        /// <summary>true: veritabanı yerine exe yanındaki JSON klasörü kullanılır (tablolarla aynı ad ve sütunlar).</summary>
        [DataMember(Order = 15)] public bool UseJsonData { get; set; } = true;

        /// <summary>JSON tablo dosyalarının klasörü (exe'ye göre).</summary>
        [DataMember(Order = 16)] public string DataFolder { get; set; } = "data";

        public string DataFolderPath
        {
            get { return Path.IsPathRooted(DataFolder ?? "") ? DataFolder : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFolder ?? "data"); }
        }

        // ---- Veritabanı (SADECE SELECT yetkili hesap) -------------------------------------
        // Örnek: server=HOST;port=3306;user id=KULLANICI;password=SIFRE;database=VERITABANI;charset=utf8;
        // Boş bırakılırsa uygulama veritabanı olmadan çalışır (listeler boş kalır).

        /// <summary>Sahne tipleri, kontrolcüler, playlist'ler (sahne tanımları veritabanı).</summary>
        [DataMember(Order = 20)] public string VizrtDbConnectionString { get; set; } = "";

        /// <summary>Haberler, akışlar, habere bağlı sahneler (haber veritabanı).</summary>
        [DataMember(Order = 21)] public string NewsDbConnectionString { get; set; } = "";

        public bool HasVizrtDb { get { return !string.IsNullOrWhiteSpace(VizrtDbConnectionString); } }
        public bool HasNewsDb { get { return !string.IsNullOrWhiteSpace(NewsDbConnectionString); } }

        // ---- Yükle / kaydet --------------------------------------------------------------

        public static string FilePath
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName); }
        }

        /// <summary>Son Load() çağrısında dosya okunamadıysa hata metni; sorun yoksa null.</summary>
        public static string LastLoadError { get; private set; }

        public static AppSettings Load()
        {
            LastLoadError = null;
            try
            {
                if (!File.Exists(FilePath))
                {
                    var fresh = new AppSettings();
                    fresh.Save();   // ilk çalıştırmada şablon dosya oluşsun, kullanıcı doldursun
                    return fresh;
                }
                using (var fs = File.OpenRead(FilePath))
                {
                    var ser = new DataContractJsonSerializer(typeof(AppSettings));
                    return (AppSettings)ser.ReadObject(fs) ?? new AppSettings();
                }
            }
            catch (Exception ex)
            {
                LastLoadError = ex.Message;
                return new AppSettings();
            }
        }

        public void Save()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(ms, Encoding.UTF8, true, true, "  "))
                {
                    var ser = new DataContractJsonSerializer(typeof(AppSettings));
                    ser.WriteObject(writer, this);
                }
                File.WriteAllBytes(FilePath, ms.ToArray());
            }
        }
    }
}
