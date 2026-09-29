# BFN DevOps

Windows/IIS sunucuları için ASP.NET Core yönetim ve yayın orkestrasyon paneli.

## Özellikler

- Korunan kurucu `admin` hesabı ve ilk girişte zorunlu parola değişikliği
- `Administrator` rolüne kısıtlı proje/IIS site/Application Pool yönetimi ve kullanıcı yönetimi; giriş yapan herkes için ortak `Genel` görev panosu
- Sürükle-bırak sıralanabilen, JSON ayarlı yayın adımları
- Git, npm, .NET publish, dosya, IIS, App Pool ve health-check adım türleri
- Kalıcı kolon/kart sıralamalı sürükle-bırak görev panosu
- İleride agent entegrasyonu için kart ataması ve deployment run veri modeli
- Tailwind CSS 4 + DaisyUI 5; `Ayarlar` sayfasından kullanıcı bazlı kalıcı tema seçimi
- Giriş ekranında da (oturum açmadan) tema deneyebilme; seçim çerez ile hatırlanır ve hesaba giriş yapılınca kullanıcı tercihine döner
- Sürüm dizini/health-check/IIS dizin geçişine uygun kesintisiz yayın modeli

## İlk çalıştırma

1. Sunucuda .NET 8 Hosting Bundle ve IIS Management Scripts and Tools bileşenini kurun.
2. Uygulama havuzu kimliğine IIS yapılandırmasını ve `Iis:SitesRoot` dizinini yönetme yetkisi verin.
3. `dotnet Bfn.DevOps.dll` ile veya IIS üzerinden uygulamayı başlatın.
4. İlk giriş: `admin` / `admin`. Uygulama hemen yeni ve güçlü bir parola ister.

SQLite veritabanı `App_Data/devops.db` altında ilk açılışta otomatik oluşur. Üretimde `appsettings.Production.json` ile `Iis:SitesRoot` değerini değiştirin. Uygulamayı internete doğrudan açmayın; VPN veya benzeri bir erişim katmanı ve HTTPS kullanın.

## Geliştirme

```powershell
dotnet restore
npm install
npm run css:build
dotnet run
```

CSS geliştirmesi sırasında `npm run css:watch` kullanılabilir.

## Kesintisiz yayın yaklaşımı

Çalışan dizinin üzerine doğrudan dosya yazmayın. Önerilen akış yeni bir `releases/<sürüm>` dizini oluşturmak, clone/install/publish işlemlerini burada tamamlamak, health-check çalıştırmak ve son aşamada IIS physical path değerini yeni sürüme çevirmektir. Eski sürüm geri dönüş için korunmalıdır. `DeploymentRun` ve sıralı `DeploymentStep` tabloları bu yürütücünün ve gelecek agent bağlantısının temelidir. Gerçek komut yürütme servisi özellikle ayrı tutulmuştur; kimlik/secret kasası ve komut izin listesi belirlenmeden kullanıcı girdisini PowerShell olarak çalıştırmayın.

IIS yüklü olmayan veya IIS yapılandırmasına yetkisi bulunmayan makinede kullanıcı ekranları çalışır; IIS ekranı açıklayıcı bir uyarı gösterir.
