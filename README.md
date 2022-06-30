
# Chatovací Aplikace

Chatovací aplikace s účelem testování a učení


### Featury

- Vytváření účtu
- Chatovaní pro všechny
- Úprava profilu
- Nahrávání obrázků


### Autor

- [@martynss96](https://www.instagram.com/martynss96/)


### Licence

[MIT](https://choosealicense.com/licenses/mit/)


## Připravení Projektu A Databáze

### Vytvoření projektu
1) Otevřít Visual Studio Code
2) Vytvořit nový projekt
![Screenshot](https://media.discordapp.net/attachments/991984205372334090/991984230814990376/unknown.png)

3) Najděte **ASP.NET Core Web App (Model-View-Controller)**, vyberte a pokračujte dále
![Screenshot](https://cdn.discordapp.com/attachments/991984205372334090/991985184100585472/unknown.png)

4) Vyberte umístění projektu, pojmenujte ho a pojmenujte solution 

5) Vyberte **.NET 6** framework, zbytek nastavení nechte ve výchozím nastavení a projekt vytvořte
![Screenshot](https://cdn.discordapp.com/attachments/991984205372334090/991986360187629598/unknown.png)

### Propojení s databází pomocí EntityFrameworkCore
1) Stáhněte sy tyto NuGet balíčky
- Microsoft.AspNetCore.Session
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
![Screenshot](https://cdn.discordapp.com/attachments/991984205372334090/991989992371003402/a.png)

2) Connection string v ***appsetting.json***
```json
"ConnectionStrings": {
    "NazevConStringu": "Server=(localdb)\\mssqllocaldb;Database=NazevDatabaze;Trusted_Connection=True;MultipleActiveResultSets=true"
},
```
![Screenshot](https://cdn.discordapp.com/attachments/991984205372334090/991992610388455424/aa.png)

3) Definování connection stringu v ***Program.cs***
```csharp
using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<Nazev.Data.NazevData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NazevConStringu")));
```
4) Vytvoření "datové centrály"
![Screenshot](https://cdn.discordapp.com/attachments/991984205372334090/991996438978510978/aaaa.png)
Vytvořte do složky s projektem složku s názvem data **"Data"** a dovnitř vytvořte soubor třídy s pojmenováním "***NazevProjektu***Data"
```csharp
using Microsoft.EntityFrameworkCore;
using Talkero.Models;

namespace *NazevProjektu*.Data
{
    public class *NazevProjektuData* : DbContext
    {
        // Zde bude definován každý model
        // Příklad:
        public DbSet<Models.Uzivatel> Uzivatele { get; set; }
        // Popis:
        // Models.Uzivatel = Soubor ve složce models se samotným modelem
        // Uzivatele = Název tabulky v databázi (Většinou vývá množné číslo názvu souboru)


        public *NazevProjektuData* (DbContextOptions<*NazevProjektuData*> options) : base(options) { }
    }
}
```

**Hotovo - Nyní máte základní použitelné propojení databáze s projektem.**

