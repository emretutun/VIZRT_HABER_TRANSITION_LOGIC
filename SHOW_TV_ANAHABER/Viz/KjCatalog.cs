using System.Collections.Generic;
using System.Linq;

namespace SHOW_TV_ANAHABER.Viz
{
    /// <summary>Alt bant (KJ) türleri ve alttan akan "az sonra" yazısı.</summary>
    public enum KjType
    {
        Haber,
        SiyahHaber,   // sahnede "yas" diye geçer
        OzelHaber,
        SicakHaber,
        SonDakika,
        AzSonra       // akan yazı; metni bitince kendi kalkar
    }

    /// <summary>Bir KJ türünün sahnedeki container ve director adları.</summary>
    public sealed class KjDefinition
    {
        public KjType Type { get; }
        public string DisplayName { get; }

        /// <summary>Başlığın yazıldığı gizli metin container'ı ($ işaretsiz ad).</summary>
        public string TextContainer { get; }

        /// <summary>TAKE öncesi açılan grup; yoksa null.</summary>
        public string TextGroup { get; }

        /// <summary>Ekranda KJ yokken ilk giriş animasyonu.</summary>
        public string InDirector { get; }

        /// <summary>KJ ekrandayken yeni başlık: küpü döndürerek metni değiştirir. Yoksa null.</summary>
        public string IntraDirector { get; }

        /// <summary>Çıkış animasyonu.</summary>
        public string OutDirector { get; }

        /// <summary>Akan yazı gibi kendi kendine biten grafik: ekranda "açık" sayılmaz.</summary>
        public bool SelfTerminating { get; }

        /// <summary>
        /// Türe özel sağ üst kulakçık (GraphicCatalog anahtarı); yoksa null.
        /// Haber logosu her türle gelir, kulakçık ise türe göre değişir: tür değişince eskisi iner yenisi gelir.
        /// </summary>
        public string EarLogoKey { get; }

        public KjDefinition(KjType type, string displayName, string textContainer, string textGroup,
                            string inDirector, string intraDirector, string outDirector,
                            bool selfTerminating, string earLogoKey)
        {
            Type = type;
            DisplayName = displayName;
            TextContainer = textContainer;
            TextGroup = textGroup;
            InDirector = inDirector;
            IntraDirector = intraDirector;
            OutDirector = outDirector;
            SelfTerminating = selfTerminating;
            EarLogoKey = earLogoKey;
        }
    }

    /// <summary>
    /// Engine konsolu denemelerinden çıkarılan tür tablosu.
    /// Kaynak: docs/viz-komut-notlari.md
    /// </summary>
    public static class KjCatalog
    {
        /// <summary>Alt bant türlerinin başlığının yazıldığı gizli metin container'ı.</summary>
        public const string KjText = "edt_txt_haber";

        /// <summary>Gizli metnin bulunduğu, TAKE öncesi açılan grup.</summary>
        public const string KjGroup = "sub_edt_txt_haber_group";

        /// <summary>Akan yazının metin container'ı (grubu yok).</summary>
        public const string TickerText = "edt_txt_akanyazi";

        /// <summary>Her türün ilk girişinde gelen haber logosu (GraphicCatalog anahtarı).</summary>
        public const string HaberLogoKey = "haberlogo";

        private static readonly Dictionary<KjType, KjDefinition> Map = new Dictionary<KjType, KjDefinition>
        {
            //                                                                                                                                              kulakçık
            { KjType.Haber,      new KjDefinition(KjType.Haber,      "Haber",       KjText, KjGroup, "dir_in_haberkj",      "dir_intra_haberkj",      "dir_out_haberkj",      false, null) },
            { KjType.SiyahHaber, new KjDefinition(KjType.SiyahHaber, "Siyah Haber", KjText, KjGroup, "dir_in_yaskj",        "dir_intra_yaskj",        "dir_out_yaskj",        false, null) },
            { KjType.OzelHaber,  new KjDefinition(KjType.OzelHaber,  "Özel Haber",  KjText, KjGroup, "dir_in_ozelhaberkj",  "dir_intra_ozelhaberkj",  "dir_out_ozelhaberkj",  false, "ozelhaberlogo") },
            { KjType.SicakHaber, new KjDefinition(KjType.SicakHaber, "Sıcak Haber", KjText, KjGroup, "dir_in_sicakhaberkj", "dir_intra_sicakhaberkj", "dir_out_sicakhaberkj", false, "sicak_haber_logo") },
            { KjType.SonDakika,  new KjDefinition(KjType.SonDakika,  "Son Dakika",  KjText, KjGroup, "dir_in_sondakikakj",  "dir_intra_sondakikakj",  "dir_out_sondakikakj",  false, "sondakikalogo") },
            // Akan yazı: metin bitince sahne kendisi kaldırır. Açılacak grubu yok.
            { KjType.AzSonra,    new KjDefinition(KjType.AzSonra,    "Az Sonra (akan yazı)", TickerText, null, "dir_in_azsonrakj", null, "dir_out_azsonrakj", true, null) },
        };

        public static KjDefinition Get(KjType type)
        {
            return Map[type];
        }

        public static IEnumerable<KjDefinition> All
        {
            get { return Map.Values; }
        }

        /// <summary>Tüm türlerin kulakçık anahtarları (birbirini dışlar).</summary>
        public static IEnumerable<string> EarLogoKeys
        {
            get { return Map.Values.Where(d => d.EarLogoKey != null).Select(d => d.EarLogoKey).Distinct(); }
        }
    }
}
