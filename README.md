# BFN DevOps

Windows/IIS sunucuları için küçük bir ASP.NET Core yönetim paneli.

## İlk çalıştırma

1. Sunucuda .NET 8 Hosting Bundle ve IIS Management Scripts and Tools bileşenini kurun.
2. Uygulama havuzu kimliğine IIS yapılandırmasını ve `Iis:SitesRoot` dizinini yönetme yetkisi verin.
3. `dotnet Bfn.DevOps.dll` ile veya IIS üzerinden uygulamayı başlatın.
4. İlk giriş: `admin` / `admin`. Uygulama hemen yeni ve güçlü bir parola ister.

SQLite veritabanı `App_Data/devops.db` altında ilk açılışta otomatik oluşur. Üretimde `appsettings.Production.json` ile `Iis:SitesRoot` değerini değiştirin. Uygulamayı internete doğrudan açmayın; VPN veya benzeri bir erişim katmanı ve HTTPS kullanın.

## Geliştirme

```powershell
dotnet restore
dotnet run
```

IIS yüklü olmayan veya IIS yapılandırmasına yetkisi bulunmayan makinede kullanıcı ekranları çalışır; IIS ekranı açıklayıcı bir uyarı gösterir.
