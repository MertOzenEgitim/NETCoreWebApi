# .NET Core Web API
## Giriş ve Tanımlar
* .NET Framework → .NET Core → .NET 5/6/7/8
* ASP.NET Core nedir?
* *Web API nedir, REST nedir? JSON vs XML
* *MVC vs Web API farkı
## Merhaba .NET Core Web API
* IIS vs Kestrel
* * IIS (Internet Information Services) ASP.NET Core için ne yapar?
* * Reverse Proxy Nedir?
* * Normal vs Reverse
* * Neden Reverse Proxy Kullanılır?
* Proje Oluşturma ve İlk Endpoint
* * CLI ile proje oluştur
* * Varsayılan yapı
* * İlk endpoint (Ping)
* launchSettings.json
* * commandName
* * applicationUrl
* * environmentVariables
* appsettings.json ve Ortam Bazlı Config
* * appsettings.json
* * appsettings.Development.json
* * appsettings.Production.json
* * Program.cs üzerinden okuma
* Program.cs ve Middleware Pipeline
* * Program.cs Temel Yapı
* Startup.cs vs Program.cs
* * ASP.NET Core 3.1 / 5
* * ASP.NET Core 6 / 7 / 8 / 9
* NET CLI Kullanımı
* * Temel komutlar: new / run / build / publish
* IIS Üzerinde Yayına Alma
* * IIS Kurulumu
* * IIS’te yeni site ekleme 
## Routing ve Controller Mantığı
* [ApiController], [Route("api/[controller]")]
* [HttpGet], [HttpPost], [HttpPut], [HttpDelete]
* Route parametreleri ve query string
* ActionResult dönüşleri
* * Ok
* * NotFound
* * CreatedAtAction
* Routing & Action Eşleşmesi
* * HTTP metodu (GET, POST, PUT, DELETE)
* * Route şablonu (sabit/dinamik segmentler)
* * Parametre tipleri (constraint varsa)
## 
* Model binding kaynakları (route, query, body)
* [FromBody], [FromRoute], [FromQuery]
* Validation: [Required], [Range], [StringLength], [EmailAddress]
* Otomatik 400 BadRequest dönüşü


