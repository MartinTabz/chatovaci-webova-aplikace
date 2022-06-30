using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Talkero.Data;
using Talkero.Models;

namespace Talkero.Controllers
{
    public class UzivatelController : Controller
    {
        private TalkeroData _databaze;

        public UzivatelController(TalkeroData databaze)
        {
            _databaze = databaze;
        }

        // Zobrazení stranky přihlásit
        [HttpGet]
        public IActionResult Prihlasit()
        {
            return View();
        }

        // Zobrazení stranky zaregistrovat
        [HttpGet]
        public IActionResult Registrovat()
        {
            return View();
        }

        // Zobrazení profilu a možnost ho upravovat
        [HttpGet]
        public IActionResult Profil(int? id)
        {
            // Akce očekává id (klíč) právě přihlášeného uživatele, toho poté hledá v databázi a následně ho uloží do proměnné
            Uzivatel? upravovanyUzivatel = _databaze.Uzivatele
                .Where(u => u.Id == id)
                .FirstOrDefault();

            // Vrátí zobrazení editování profilu
            return View(upravovanyUzivatel);
        }

        [HttpPost]
        public IActionResult Registrovat(string mail, string nick, string heslo, string hesloKontrola, bool souhlas, bool novinky, Uzivatel model)
        {
            // Vytvoří lokální proměnou uživatele který již má zadaný nick, pokud však nenalezne identický nick výsledek je NULL
            Uzivatel? existujiciNick = _databaze.Uzivatele
                .Where(u => u.Nick == nick)
                .FirstOrDefault();

            // Vytvoří lokální proměnou uživatele který již má zadaný mail, pokud však nenalezne identický mail výsledek je NULL
            Uzivatel? existujiciMail = _databaze.Uzivatele
                .Where(u => u.Mail == mail)
                .FirstOrDefault();


            // Pokud jakékoliv z těchto povinných polí jsou prázná, ukáže se error
            if (mail == null || nick == null || heslo == null)
                ModelState.AddModelError("Nevyplneno", "Toto pole je povinné");
            // Pokud jakékoliv z těchto povinných polí jsou prázná, ukáže se error
            if (heslo != hesloKontrola)
                ModelState.AddModelError("Kontrola", "Hesla se neschodují");
            // Pokud uživatel nesouhlasí, nebo zapomene souhlasit s podmínkami, vyběhne mu error a nepustí dál
            if (souhlas != true)
                ModelState.AddModelError("Souhlas", "Pro registraci musíte souhlasit s podmínkami");
            // Pokud se najde uživatel, který již využívá zadaný email a tedy výsledek podmínky s lokální proměnné není NULL, vyběhne mu error a nepustí dál
            if (existujiciMail != null)
                ModelState.AddModelError("Mail", "Tento e-mail už je používán");
            // Pokud se najde uživatel, který již využívá zadaný nick a tedy výsledek podmínky s lokální proměnné není NULL, vyběhne mu error a nepustí dál
            if (existujiciNick != null)
                ModelState.AddModelError("Nick", "Toto uživatelské jméno už je zabrané");
            // Pokud má heslo méně jak 6 písmen, ukáže se error
            if (heslo.Length < 6)
                ModelState.AddModelError("Heslo", "Příliš krátké heslo");
            // Pokud se uživatel úspěšně dostane přes všechny podmínky zadané údaje jsou zpracovány
            mail = mail.Trim().ToLower();
            nick = nick.Trim().ToLower();
            heslo = heslo.Trim();


            if(!ModelState.IsValid)
            {
                return View(model);
            }

            // Dále se vytvoří nový uživatel s daným vyplněním modelu
            Uzivatel novyUzivatel = new Uzivatel()
            {
                Mail = mail,
                Nick = nick,
                // Heslo je hashováno přes BCrypt knihovnu
                Heslo = BCrypt.Net.BCrypt.HashPassword(heslo),
                Overen = false,
                Profilovka = "~/img/profilovky/new_profilovka.png",
                Banner = "~/img/bannery/new_banner.png",
                Zablokovan = false,
                DuvodZablokovani = "Zatím nezablokován",
                ZadaOOdblokovani = false,
                JeAdmin = false,
                SouhlasiSPodminkami = true,
                ZasilatNovinky = novinky,
            };

            // a následně se uloží do databáze
            _databaze.Add(novyUzivatel);
            _databaze.SaveChanges();

            // Pokud je vše úspěné uživatel je odkázán na přihlášení.
            return RedirectToAction("Prihlasit");
        }

        [HttpPost]
        public IActionResult Prihlasit(string nick_log, string heslo_log, Uzivatel model, bool zapamatovat)
        {
            // Kvůli zkoušení validace je přihlášení malinko na prasáka

            // Každopádně zde zjistí, jestli input nick a hesla je nula/NULL a pokud ano znovu se objeví login a s errorem
            if (nick_log == null || nick_log.Trim().Length == 0)
            {
                ModelState.AddModelError("Nick", "Uživatelské jméno musí být vyplněno");
                return View(model);
            }
            if (heslo_log == null || heslo_log.Trim().Length == 0)
            {
                ModelState.AddModelError("Heslo", "Heslo musí být vyplněno");
                return View(model);
            }

            // Zde v databázi hledá, jestli existuje v ní existuje uživatel se stejným nickem jako který je ten vložený
            Uzivatel? prihlasovanyUzivatel = _databaze.Uzivatele
                .Where(u => u.Nick == nick_log)
                .FirstOrDefault();

            // Pokud nikoho takového v databázi nenalezne znovu se objeví login a s errorem
            if (prihlasovanyUzivatel == null)
            {
                ModelState.AddModelError("Uzivatelske", "Neexistující uživatelské jméno");
                return View(model);
            }

            // Bool zajištuje, jestli se hashované heslo v databázi shoduje s tím inputovaným
            bool validPassword = BCrypt.Net.BCrypt.Verify(heslo_log, prihlasovanyUzivatel.Heslo);
            // Pokud se hesla neschodují znovu se objeví login a s errorem
            if (validPassword == false)
            {
                ModelState.AddModelError("Heslo", "Chybné heslo");
                return View(model);
            }

            // Pokud vše proběhne úspěšně zapíší se Http session stringy a hotovo (S tímto se pak dá dále pracovat)
            HttpContext.Session.SetString("Prihlaseny", prihlasovanyUzivatel.Nick);
            HttpContext.Session.SetString("PrihlasenyMail", prihlasovanyUzivatel.Mail);
            HttpContext.Session.SetInt32("Identita", prihlasovanyUzivatel.Id);

            // A v posledním kroku se vrátí na index
            return RedirectToAction("Index", "Home");
        }
    }
}
