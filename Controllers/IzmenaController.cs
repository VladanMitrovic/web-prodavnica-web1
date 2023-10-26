using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class IzmenaController : ApiController
    {
        public List<Korisnik> CitajJson()
        {
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Registrovani.json";
            List<Korisnik> korisnici = new List<Korisnik>();
            try
            {
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    string json = r.ReadToEnd();
                    korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(json);
                    return korisnici ?? new List<Korisnik>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Greška pri čitanju JSON fajla: " + ex.Message);
                return new List<Korisnik>();
            }
        }

        public List<Proizvod> CitajJsonProd()
        {
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";
            List<Proizvod> proizvodi = new List<Proizvod>();
            try
            {
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    string json = r.ReadToEnd();
                    proizvodi = JsonConvert.DeserializeObject<List<Proizvod>>(json);
                    return proizvodi ?? new List<Proizvod>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Greška pri čitanju JSON fajla: " + ex.Message);
                return new List<Proizvod>();
            }
        }

        public List<Recenzija> CitajJsonRecenzija()
        {
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Recenzije.json";
            List<Recenzija> recenzije = new List<Recenzija>();
            try
            {
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    string json = r.ReadToEnd();
                    recenzije = JsonConvert.DeserializeObject<List<Recenzija>>(json);
                    return recenzije ?? new List<Recenzija>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Greška pri čitanju JSON fajla: " + ex.Message);
                return new List<Recenzija>();
            }
        }

        public List<Porudzbina> CitajJsonPorudzbine()
        {
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Porudzbine.json";
            List<Porudzbina> porudzbine = new List<Porudzbina>();
            try
            {
                using (StreamReader r = new StreamReader(jsonPath))
                {
                    string json = r.ReadToEnd();
                    porudzbine = JsonConvert.DeserializeObject<List<Porudzbina>>(json);
                    return porudzbine ?? new List<Porudzbina>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Greška pri čitanju JSON fajla: " + ex.Message);
                return new List<Porudzbina>();
            }
        }

        [HttpPost]
        [Route("api/izmena/GetKorisnik")]
        public Korisnik GetKorisnik(Korisnik k)
        {
            List<Korisnik> listKorisnici = CitajJson();

            foreach (var item in listKorisnici)
            {
                if (item.Korisnicko_Ime == k.Korisnicko_Ime)
                {
                    return item;
                }

            }
            return null;
        }

        [HttpPost]
        [Route("api/izmena/IzmenaKorisnika")]
        public bool IzmenaKorisnika(Korisnik k)
        {
            List<Korisnik> sviKorisnici = CitajJson();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Registrovani.json";

            foreach (var item in sviKorisnici)
            {
                if (item.Korisnicko_Ime == k.Korisnicko_Ime)
                {
                    item.Lozinka = k.Lozinka;
                    item.Pol = k.Pol;
                    item.Ime = k.Ime;
                    item.Prezime = k.Prezime;
                    item.Email = k.Email;
                    item.Datum_Rodjenja = k.Datum_Rodjenja;

                    var jsonString = JsonConvert.SerializeObject(sviKorisnici, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("api/izmena/GetProizvod")]
        public Proizvod GetProizvod(Proizvod prod)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            foreach (var item in sviProizvodi)
            {
                if (item.Id == prod.Id)
                {
                    return item;
                }
            }
            return null;
        }

        [HttpPost]
        [Route("api/izmena/IzmenaProizvoda")]
        public bool IzmenaProizvoda(Proizvod prod)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";

            foreach (var item in sviProizvodi)
            {
                if (item.Id == prod.Id)
                {
                    item.Naziv = prod.Naziv;
                    item.Cena = prod.Cena;
                    item.Kolicina = prod.Kolicina;
                    item.Opis = prod.Opis;
                    item.Grad = prod.Grad;
                    //item.Slika = prod.Slika;

                    if (prod.Kolicina <= 0)
                    {
                        item.Dostupnost = false;
                    }
                    else
                    {
                        item.Dostupnost = true;
                    }

                    var jsonString = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }


        [HttpPost]
        [Route("api/izmena/GetRecenzija")]
        public Recenzija GetRecenzija(Recenzija rec)
        {
            List<Recenzija> sveRecenzije = CitajJsonRecenzija();
            foreach (var item in sveRecenzije)
            {
                if (item.Id == rec.Id)
                {
                    return item;
                }
            }
            return null;
        }

        [HttpPost]
        [Route("api/izmena/GetRecenzije")]
        public List<Recenzija> GetRecenzije(Recenzija rec)
        {
            List<Recenzija> sveRecenzije = CitajJsonRecenzija();
            List<Recenzija> specificneRecenzije = new List<Recenzija>();
            foreach (var item in sveRecenzije)
            {
                if (item.Proizvod.Id == rec.Proizvod.Id && item.Brisanje == BrisanjeRecenzije.NijeObrisana && item.Odobreno == true)
                {
                    specificneRecenzije.Add(item);
                }
            }

            return specificneRecenzije;
        }


        [HttpPost]
        [Route("api/izmena/IzmenaRecenzije")]
        public bool IzmenaRecenzije(Recenzija rec)
        {
            List<Recenzija> sveRecenzije = CitajJsonRecenzija();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Recenzije.json";

            foreach (var item in sveRecenzije)
            {
                if (item.Id == rec.Id)
                {
                    item.Naslov = rec.Naslov;
                    item.Sadrzaj_recenzije = rec.Sadrzaj_recenzije;

                    var jsonString = JsonConvert.SerializeObject(sveRecenzije, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }
    }
}
