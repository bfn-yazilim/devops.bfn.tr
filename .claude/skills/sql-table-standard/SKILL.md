---
name: sql-table-standard
description: Standardizes EF Core (SQLite) entity/tablo olusturmayi ortak audit kolonlari ve migration akisiyla. Use when creating or revising an entity under Models/**.cs or adding/altering a table via EF Core migrations.
---

# Skill: EF Core Tablo Olusturma Standardi

## Amac

Bu proje (Bfn.DevOps) EF Core + SQLite kullanir. Tablolar `Database/**` altinda elle yazilan SQL script'leriyle degil, `Models/**.cs` altindaki C# entity siniflarindan code-first migration ile olusturulur (`Data/ApplicationDbContext.cs`). Bu skill, yeni/mevcut entity'lerin ayni isimlendirme ve audit kolon standardiyla yazilmasini saglar.

## Ne Zaman Kullanilir

- `Models/DevOpsModels.cs` (veya ilgili Models dosyasi) altinda yeni bir entity/tablo eklenirken
- Mevcut bir entity standarda gore revize edilirken
- Yeni ayar/icerik/is nesnesi icin tablo eklenirken

## Zorunlu Kurallar

1. Tablo primary key kolonu proje konvansiyonuna uyar: `public int Id { get; set; }` (veya `long Id` yuksek hacimli loglar icin, orn. `DeploymentRun`). PK kolon adini `IdTabloAdi` gibi degistirme — mevcut tum FK'lar ve controller kodu `Id` bekliyor.
2. Audit kolonlari standart olarak eklenir, bu sira ile: `UId`, `CreUser`, `CreDate`, `ModUser`, `ModDate`, `DelUser`, `DelDate`, `Client`, `ClientIp`, `IsDeleted`. `UId` audit kolonlarinin EN BASINDA yer alir (CreUser'dan once), `IsDeleted` EN SONDA. Bunun icin yeni entity sinifini `Models/DevOpsModels.cs` icindeki `AuditableEntity` base class'indan turet — kolonlari tek tek yazma; siralama zaten `ApplicationDbContext.OnModelCreating` icindeki `auditColumnOrder` dizisi ile otomatik uygulanir.
3. `UId` -> `Guid`, `AuditableEntity` icinde `= Guid.NewGuid()` ile client-side default alir.
4. `CreDate` -> `DateTime`, `AuditableEntity` icinde `= DateTime.UtcNow` ile client-side default alir.
5. `ModUser`/`DelUser`/`CreUser` -> `string?` (int DEGIL). Bu projede kullanici Id'leri ASP.NET Identity GUID string'i (`ApplicationUser.Id`), int degil. Mevcut `CreatedByUserId`, `AssignedUserId` gibi alanlarla ayni tip.
6. Soft-delete bayragi `IsDeleted` adiyla yazilir (`Deleted` DEGIL) -> `bool`, DB tarafinda `HasDefaultValue(false)` ile.
7. `Client`/`ClientIp` -> `string?`, `[MaxLength(50)]`.
8. `CreUser`/`ModUser`/`DelUser` -> `[MaxLength(450)]` (Identity Id uzunlugu ile ayni).
9. PK/FK/Index isimlendirmesi EF Core konvansiyonuna birakilir (`PK_TabloAdi`, `FK_...`, `IX_...` otomatik uretilir). MSSQL tarzi `DF_TabloAdi_KolonAdi` constraint adlandirmasi SQLite'ta anlamli degildir, kullanilmaz.
10. Script `USE/SET ANSI_NULLS/SET QUOTED_IDENTIFIER` bloklariyla baslamaz — bu MSSQL'e ozgudur, bu projede yoktur.

## Uygulama Adimlari

1. `Models/DevOpsModels.cs` icine yeni `sealed class TabloAdi : AuditableEntity { public int Id { get; set; } ... }` ekle. Is alani kolonlarini normal sekilde tanimla (`[Required, MaxLength(...)]` vb.).
2. `Data/ApplicationDbContext.cs` icine `public DbSet<TabloAdi> TabloAdilar => Set<TabloAdi>();` ekle.
3. Gerekliyse `OnModelCreating` icinde index/FK/conversion tanimlarini ekle. `AuditableEntity`'den tureyen her entity icin `UId`/`CreDate`/`IsDeleted` default'lari VE kolon sirasi (`UId` en basta, `IsDeleted` en sonda) zaten `OnModelCreating` basindaki `foreach` dongusuyle (`auditColumnOrder` dizisi) otomatik uygulanir — tekrar elle yazma.
4. Uygulama KAPALIYKEN (bin/dll kilitli olmamali) migration olustur: `dotnet ef migrations add <Anlamli Isim>`.
5. **SQLite kisitina dikkat**: mevcut satirli bir tabloya `NOT NULL` kolon eklerken default sabit (constant) olmali — `randomblob(...)` veya `CURRENT_TIMESTAMP` gibi ifadeler `ALTER TABLE ADD COLUMN` icin SQLite tarafindan reddedilir ("Cannot add a column with non-constant default"). `AuditableEntity` icin `UId`/`CreDate` default'lari bu yuzden sabit literal (`'00000000-0000-0000-0000-000000000000'`, `'1970-01-01 00:00:00'`) olarak tanimlanmistir. Yeni bir migration'da bu tur kolonlar eklenirse, migration dosyasinin `Up()` metodunun sonuna mevcut satirlari gercek deger ile dolduran bir `migrationBuilder.Sql(...)` backfill bloğu ekle (bkz. ornek).
6. `dotnet ef database update` ile migration'i uygula, ardindan mevcut satirlarin backfill edilip edilmedigini dogrula (ornegin `node -e "const {DatabaseSync}=require('node:sqlite'); ..."` ile veya app'i baslatip DB Explorer sayfasindan kontrol ederek).

## Kontrol Listesi

- Yeni entity `AuditableEntity`'den turetildi mi (`: AuditableEntity`)?
- `CreUser`/`ModUser`/`DelUser` tipi `string?` mi (int degil)?
- Soft-delete bayragi `IsDeleted` mi (`Deleted` degil)?
- Tablo kolon sirasinda `UId` audit kolonlarinin en basinda, `IsDeleted` en sonunda mi (otomatik, elle kontrol etmene gerek yok — sadece `AuditableEntity`'den turetildiginden emin ol)?
- `ApplicationDbContext`'e `DbSet<>` eklendi mi?
- Migration `NOT NULL` + non-constant default iceren bir ADD COLUMN yapiyorsa, sabit literal default + backfill `Sql()` bloğu var mi?
- Migration uygulanirken app kapali mi (dll kilit hatasi olmamali)?
- Backfill sonrasi mevcut satirlarda placeholder deger (`00000000-...`, `1970-01-01...`) kalmadi mi?

## Ornek: Yeni Entity

```csharp
// Models/DevOpsModels.cs
public sealed class SyContentSetting : AuditableEntity
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Key { get; set; } = "";
    [MaxLength(4000)] public string? Value { get; set; }
}
```

```csharp
// Data/ApplicationDbContext.cs
public DbSet<SyContentSetting> ContentSettings => Set<SyContentSetting>();
```

## Ornek: Mevcut Tabloya Sonradan Audit Kolonu Eklerken Migration Backfill

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<Guid>(
        name: "UId", table: "SyContentSetting", type: "TEXT",
        nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'");
    migrationBuilder.AddColumn<DateTime>(
        name: "CreDate", table: "SyContentSetting", type: "TEXT",
        nullable: false, defaultValueSql: "'1970-01-01 00:00:00'");
    migrationBuilder.AddColumn<bool>(
        name: "IsDeleted", table: "SyContentSetting", type: "INTEGER",
        nullable: false, defaultValue: false);

    // Backfill: mevcut satirlari gercek deger ile doldur (constant default'lar sadece
    // ADD COLUMN'un gecmesi icindi, gercek deger degil)
    migrationBuilder.Sql(@"UPDATE ""SyContentSetting"" SET ""UId"" =
        lower(hex(randomblob(4))) || '-' || lower(hex(randomblob(2))) || '-4' || substr(lower(hex(randomblob(2))),2) || '-' ||
        substr('89ab', abs(random()) % 4 + 1, 1) || substr(lower(hex(randomblob(2))),2) || '-' || lower(hex(randomblob(6)))
        WHERE ""UId"" = '00000000-0000-0000-0000-000000000000';");
    migrationBuilder.Sql(@"UPDATE ""SyContentSetting"" SET ""CreDate"" = CURRENT_TIMESTAMP WHERE ""CreDate"" = '1970-01-01 00:00:00';");
}
```

## Ornek Prompt

"Models altinda `SyContentSetting` adinda yeni bir entity ekle, AuditableEntity'den turesin, ApplicationDbContext'e DbSet ekle ve migration olustur."
