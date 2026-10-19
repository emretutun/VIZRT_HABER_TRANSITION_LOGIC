# SHOW TV Ana Haber – Viz Reji Uygulaması

Vizrt Viz Engine üzerinde çalışan haber grafiklerini (KJ, logo, flot, kelebek) yöneten bir Windows Forms uygulaması.
.NET Framework 4.8, Viz Artist/Engine 3.14 ile geliştirildi.

Bu depo aynı zamanda bir **öğrenme günlüğü**dür: Viz komut dilini ve "transition logic" yaklaşımını sıfırdan öğrenirken
yazıldı. Aşağıdaki bölümler hem uygulamayı hem de bu kavramları anlatır.

> Not: Örnek veri katmanı (`SHOW_TV_ANAHABER/Data/`) ve kişisel çalışma notları bu depoda yer almaz.

---

## 1. Viz Engine ile nasıl konuşulur

Viz Engine 6100 portunda düz metin komut kabul eder. Komut, sonunda NUL (0x00) karakteri olan tek bir satırdır.
Başına bir kimlik numarası konur; Engine cevabı aynı kimlikle döner. Kimlik `-1` ise cevap beklenmez.

```
1 RENDERER*MAIN_LAYER*TREE*$edt_txt_haber*GEOM*TEXT GET      → 1 ESENYURT'TA UYUŞTURUCU TRAFİĞİ!
-1 RENDERER*MAIN_LAYER*STAGE*DIRECTOR*dir_in_haberkj START      → (cevap yok)
```

Komut dilinin iki parçası vardır:

| Parça | Ayırıcı | Örnek |
|---|---|---|
| **Adres** | `*` | `RENDERER*MAIN_LAYER*TREE*$edt_txt_haber*GEOM*TEXT` |
| **Komut** | boşluk | `GET`, `SET değer`, `START`, `CONTINUE`, `SHOW 0.0`, `STOP` |

Sık kullanılan adresler:

- `RENDERER*MAIN_LAYER` / `RENDERER*BACK_LAYER` / `RENDERER*FRONT_LAYER`: üç katman. Her katmana ayrı sahne yüklenir.
- `...*TREE*$ad` : sahne ağacında adıyla container. `...*TREE*/4/1` : indeks yoluyla container.
- `...*STAGE*DIRECTOR*ad` : stage'deki bir director (animasyon zaman çizgisi).
- `SCENE*yol LOAD` : sahneyi belleğe al (saniyeler sürer, cevap beklenir). `RENDERER*MAIN_LAYER SET_OBJECT SCENE*yol` : katmana koy.

Dikkat: `STAGE START` ve `RENDERER*...` yazma komutları Artist editör modunda reddedilir. Komutla oynatmak için
Artist **On Air** modunda olmalı ya da gerçek Engine'e bağlanılmalı.

Uygulamadaki karşılığı: [`Viz/VizEngineClient.cs`](SHOW_TV_ANAHABER/Viz/VizEngineClient.cs) (TCP istemci) ve
[`Viz/VizCommands.cs`](SHOW_TV_ANAHABER/Viz/VizCommands.cs) (komut metinleri). "Viz Konsolu" sekmesinden elle komut gönderip
cevabı görebilirsin; öğrenmenin en hızlı yolu bu.

---

## 2. Transition Logic nedir, bu sahnede nasıl uygulanıyor

**Fikir:** ekrandaki her bağımsız grafik bir *katman*dır ve her katmanın bir *durumu* vardır (kapalı, açık, değişiyor).
Kontrol uygulaması animasyon detayı bilmez; sadece "bu katman şu duruma geçsin" der, geçişi sahne yapar.
Böylece alt bant değişirken logo etkilenmez, logo inerken alt bant durur, geçişler hep pürüzsüzdür.

Vizrt'nin resmi Transition Logic'inde bu, Toggle plugini olan bir master sahne + ControlObject'li object sahnelerle kurulur.
**Bu projedeki sahne (`SHOW_HABER/HABER2016/ShowHaber2016-2`) resmi TL değil**, ama aynı fikri kendi yöntemiyle kurmuş:

| TL kavramı | Bu sahnede karşılığı |
|---|---|
| Katman | Alt bant küpü, haber logosu, kulakçıklar, akan yazı, yer KJ… her biri ayrı director grubu |
| Durum geçişi | `dir_in_X` (giriş), `dir_out_X` (çıkış), `dir_intra_X` (ekrandayken değişim) |
| A/B yuvası | Küpün ön yüzü `grp_on_*` ve arka yüzü `grp_arka_*` |
| Object sahne metni | Gizli `edt_txt_haber` container'ı |
| Geçişi yapan şey | `dir_intra_*` içindeki action keyframe'ler |

**Küp geçişi adım adım** (uygulamanın yaptığı tek şey 1 ve 2):

1. Yeni başlık ekranda görünmeyen `edt_txt_haber` metnine yazılır.
2. `dir_intra_haberkj` başa sarılıp oynatılır.
3. Director'un action keyframe'i gizli metni küpün arka yüzüne kopyalar.
4. Küp döner; izleyici eski başlığın yenisine dönüştüğünü görür.
5. Son keyframe ön yüzü de günceller ve küpü sessizce başa alır. Sistem bir sonraki geçişe hazırdır.

Yani uygulama *yüz takibi yapmaz*; sahne kendi iç durumunu kendisi yönetir. Uygulamanın görevi **durum makinesi**dir:
ekranda hangi tür var, logolar açık mı, animasyon ne zaman biter. Bu, [`Viz/ShowHaberController.cs`](SHOW_TV_ANAHABER/Viz/ShowHaberController.cs)
sınıfında.

Bir KJ verildiğinde uygulamanın gönderdiği dizi (Engine konsolunda denenmiş haliyle):

```
-1 RENDERER*MAIN_LAYER*TREE*$edt_txt_haber*GEOM*TEXT SET <başlık>
-1 RENDERER*MAIN_LAYER*TREE*$sub_edt_txt_haber_group*ACTIVE SET 1
-1 RENDERER*MAIN_LAYER*STAGE START                                   (sahne yüklendikten sonra bir kez)
-1 RENDERER*MAIN_LAYER*STAGE*DIRECTOR*dir_in_haberkj SHOW 0.0        (ekranda KJ yoksa: giriş)
-1 RENDERER*MAIN_LAYER*STAGE*DIRECTOR*dir_in_haberkj CONTINUE
-1 RENDERER*MAIN_LAYER*STAGE*DIRECTOR*dir_in_haberlogo SHOW 0.0      (logo da birlikte)
-1 RENDERER*MAIN_LAYER*STAGE*DIRECTOR*dir_in_haberlogo CONTINUE
```

Aynı tür ekrandayken ikinci başlıkta sadece `dir_intra_haberkj` oynar. Her director için `END_TIME GET` okunur ve
animasyon bitene kadar düğmeler kilitlenir.

---

## 3. Uygulamanın yapısı

```
SHOW_TV_ANAHABER/
├─ Form1.cs / Form1.Designer.cs     Ana ekran (Designer ile düzenlenebilir)
├─ Viz/
│  ├─ VizEngineClient.cs            TCP istemci: Send (cevaplı), SendNoReply (-1), log olayı
│  ├─ VizCommands.cs                Komut metinleri (katman, container, director, stage)
│  ├─ ShowHaberController.cs        Ana sahnenin durum makinesi: KJ, akan yazı, yer, canlı, logolar, flot, Tümünü Al
│  ├─ KjCatalog.cs                  Alt bant türleri (director adları, kulakçık eşlemesi)
│  ├─ GraphicCatalog.cs             Logolar, sosyal medya, isim KJ'leri (tek director çiftli grafikler)
│  └─ GenericScenePublisher.cs      Veri tanımlı herhangi bir tipi oynatan genel motor (kelebekler, DB satırları)
├─ Data/                           Örnek veri katmanı (depoda yok): sahne tipi ve haber modelleri, JSON örnek veri
├─ Config/AppSettings.cs            appsettings.json (Engine adresleri, program modu, veri kaynağı)
└─ Ui/                              Kelebek paneli, ayarlar penceresi, metin sorma
tools/vizcmd.ps1                    Engine'e dosyadan komut gönderen PowerShell yardımcısı
```

### Ekranlar

- **Haberler:** tarih → akış → haber → habere bağlı sahne satırları. Satır seçip VER (Ctrl+Q) / AL (Ctrl+W).
  Ctrl/Shift ile birden çok satır seçilirse (metin + isim + yer) hepsi birlikte verilir; çakışan satırlar birlikte verilmez.
  VER sonrası seçim alttaki satıra geçer, alt alta dizili başlıklar art arda Ctrl+Q ile verilir.
  "Farklı KJ tipinde ver" düğmeleri aynı metni başka türle basar. Sağda sahne tipleri (çift tık: yeni satır), logo listesi.
  Tümünü Al (araç çubuğu) onay sorar ve katmanları animasyonsuz boşaltır; Ctrl+E her grafiği çıkış animasyonuyla alır.
- **Listeler:** playlist'ler ve sahneleri (ana reji Engine'i).
- **Kelebekler:** iki Viz hedefi, her biri kendi playlist'i; pencere sahneleri arka katmanda.
- **Viz Konsolu:** bağlantı, ham komut, tüm komut/cevap logu (aynı log `bin\Debug\log.txt` dosyasına da yazılır).

### Katmanlar ve grafik türleri

| Grafik | Katman | Giriş | Çıkış |
|---|---|---|---|
| Alt bant (haber, özel, sıcak, siyah, son dakika) | MAIN | `dir_in_*kj` + logolar | `dir_out_*kj` + logolar |
| Aynı tür yeni başlık | MAIN | `dir_intra_*kj` (küp döner) | – |
| Akan yazı (az sonra) | MAIN | `dir_in_azsonrakj` | kendisi biter, `dir_out_azsonrakj` ile kesilir |
| Logolar, sosyal medya, isim KJ | MAIN | `dir_in_X` | `dir_out_X` |
| Flot (FLOT-01…20) | BACK | sahne yükle + `dir_in_flot` | `dir_out_flot`, sonra katman boşaltılır |
| Kelebek (pencere sahneleri) | BACK | sahne yükle + metin + `STAGE START` | `STAGE STOP` + katman boşalt |

---

## 4. Kurulum

1. Visual Studio ile `SHOW_TV_ANAHABER.slnx` aç ve derle. Derleme için yerelde tutulan `Data/` klasörü gerekir.
2. İlk çalıştırmada exe yanında `appsettings.json` ve örnek verilerin bulunduğu `data\` klasörü oluşur.
3. Test için Viz Artist'i aç, sahneyi yükle, **On Air** moduna al; uygulamada "Tekrar Bağlan".
4. `appsettings.json` içinde `EngineHost/EnginePort` ana Engine'i, `Kelebek1Host/2Host` kelebek hedeflerini belirler.
   Bu dosya ve `data\` klasörü kaynak kontrolüne girmez (`.gitignore`).

---

## 5. Yeni bir grafik nasıl eklenir

1. Artist'te sahneyi aç; metin container'ının adını (`edt_txt_…`) ve director adlarını not al. Ya da Viz Konsolu'ndan:
   `RENDERER*MAIN_LAYER*TREE GET` ve `RENDERER*MAIN_LAYER*STAGE*DIRECTOR GET`.
2. Viz Konsolu'ndan dene: metni `SET`, director'u `SHOW 0.0` + `CONTINUE`. Ekranda doğru geldiyse,
3. `data\` klasöründeki sahne tipi dosyasına bir satır, alan dosyasına tipin alanlarını ekle. Uygulama yeniden okur,
   tip sağ listede görünür; VER/AL genel motorla çalışır. Kod değişmez.

Ana sahnenin KJ türleri gibi özel davranış gerekiyorsa `KjCatalog` / `GraphicCatalog` sabit tablolarına eklenir.

---
