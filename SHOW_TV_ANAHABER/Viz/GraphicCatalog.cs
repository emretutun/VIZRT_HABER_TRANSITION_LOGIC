using System.Collections.Generic;
using System.Linq;

namespace SHOW_TV_ANAHABER.Viz
{
    /// <summary>
    /// Tek director çiftiyle (in/out) kontrol edilen basit grafikler: logolar, sosyal medya, ihbar hattı, isim KJ'leri.
    /// Metin gerektirenlerde TextContainer doludur.
    /// </summary>
    public sealed class GraphicDefinition
    {
        public string Key { get; }
        public string DisplayName { get; }
        public string Group { get; }
        public string InDirector { get; }
        public string OutDirector { get; }
        public string TextContainer { get; }
        public string DefaultText { get; }

        public bool NeedsText
        {
            get { return TextContainer != null; }
        }

        public GraphicDefinition(string key, string displayName, string group, string inDirector, string outDirector,
                                 string textContainer = null, string defaultText = null)
        {
            Key = key;
            DisplayName = displayName;
            Group = group;
            InDirector = inDirector;
            OutDirector = outDirector;
            TextContainer = textContainer;
            DefaultText = defaultText;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    /// <summary>
    /// Engine konsolu denemelerinden çıkarılmıştır (2026-10-14).
    /// Kaynak: docs/viz-komut-notlari.md bölüm 3g.
    /// </summary>
    public static class GraphicCatalog
    {
        public const string GroupLogo = "Logo";
        public const string GroupSosyal = "Sosyal / İhbar";
        public const string GroupIsim = "İsim KJ";

        private static readonly List<GraphicDefinition> Items = new List<GraphicDefinition>
        {
            // ---- Logolar (sağ üst) -- LOG İLE DOĞRULANDI
            new GraphicDefinition("haberlogo",        "Show Haber Logo",            GroupLogo, "dir_in_haberlogo",        "dir_out_haberlogo"),
            new GraphicDefinition("ozelhaberlogo",    "Özel Haber Logo",            GroupLogo, "dir_in_ozelhaberlogo",    "dir_out_ozelhaberlogo"),
            new GraphicDefinition("sondakikalogo",    "Son Dakika Logo",            GroupLogo, "dir_in_sondakikalogo",    "dir_out_sondakikalogo"),
            new GraphicDefinition("sicak_haber_logo", "Sıcak Haber Logo",           GroupLogo, "dir_in_sicak_haber_logo", "dir_out_sicak_haber_logo"),
            new GraphicDefinition("hbk",              "Herkes Bunu Konuşuyor Logo", GroupLogo, "dir_in_hbk",              "dir_out_hbk"),
            new GraphicDefinition("sbnd",             "Sokak Ne Diyor Logo",        GroupLogo, "dir_in_sbnd",             "dir_out_sbnd"),
            new GraphicDefinition("neler",            "Neler Oldu Neler Logo",      GroupLogo, "dir_in_neler",            "dir_out_neler"),
            new GraphicDefinition("bih",              "Bir İnsan Hikayesi Logo",    GroupLogo, "dir_in_bih",              "dir_out_bih"),
            new GraphicDefinition("bhd",              "Bu Habere Dikkat Logo",      GroupLogo, "dir_in_bhd",              "dir_out_bhd"),

            // ---- Sosyal medya / ihbar -- LOG İLE DOĞRULANDI
            new GraphicDefinition("facebook_twitter", "Facebook / Twitter",         GroupSosyal, "dir_in_facebook_twitter", "dir_out_facebook_twitter"),
            new GraphicDefinition("whatsapp",         "Whatsapp İhbar",             GroupSosyal, "dir_in_whatsapp",         "dir_out_whatsapp", "edt_txt_whatsapp_tel", "0536 2669000"),
            new GraphicDefinition("bip",              "BiP İhbar",                  GroupSosyal, "dir_in_bip",              "dir_out_bip",      "edt_txt_bipno",        "0536 266 90 02"),

            // ---- İsim KJ'leri. Spiker LOG İLE DOĞRULANDI; muhabir ve kamera aynı kalıp VARSAYIMI.
            new GraphicDefinition("spikerkj",         "Spiker KJ",                  GroupIsim, "dir_in_spikerkj",  "dir_out_spikerkj",  "edt_txt_spiker"),
            new GraphicDefinition("muhabirkj",        "Muhabir KJ",                 GroupIsim, "dir_in_muhabirkj", "dir_out_muhabirkj", "edt_txt_muhabir"),
            new GraphicDefinition("kamerakj",         "Kamera KJ",                  GroupIsim, "dir_in_kamerakj",  "dir_out_kamerakj",  "edt_txt_kamera"),
        };

        public static IEnumerable<GraphicDefinition> All
        {
            get { return Items; }
        }

        public static IEnumerable<GraphicDefinition> InGroup(string group)
        {
            return Items.Where(g => g.Group == group);
        }

        public static GraphicDefinition Get(string key)
        {
            return Items.FirstOrDefault(g => g.Key == key);
        }

        public static GraphicDefinition ByDisplayName(string name)
        {
            return Items.FirstOrDefault(g => g.DisplayName == name);
        }
    }
}
