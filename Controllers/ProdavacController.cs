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
    public class ProdavacController : ApiController
    {
        TestController testController = new TestController();

        public bool UpisiJson(Proizvod prod)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";
            bool found = true;
            if (sviProizvodi == null)
            {
                sviProizvodi = new List<Proizvod>();
                sviProizvodi.Add(prod);
            }
            else
            {
                if (sviProizvodi != null)
                {
                    foreach (var item in sviProizvodi)
                    {
                        if (item.Id.Equals(prod.Id))
                        {
                            found = false;
                            break;
                        }

                    }
                    //return true;

                }
                else
                {
                    sviProizvodi.Add(prod);
                }
            }
            if (found)
            {
                sviProizvodi.Add(prod);
                var path = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
                File.WriteAllText(jsonPath, path);
                return true;
            }
            else
            {
                return false;
            }

        }
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

        public Korisnik GetKorisnik(string korIme)
        {
            List<Korisnik> listKorisnici = CitajJson();

            foreach (var item in listKorisnici)
            {
                if (item.Korisnicko_Ime == korIme)
                {
                    return item;
                }

            }
            return null;
        }

        [HttpPost]
        [Route("api/prodavac/GetProizvodi")]

        public List<Proizvod> GetProizvodi(Korisnik k)
        {
            List<Proizvod> proizvodiOdProdavca = new List<Proizvod>();

            List<Proizvod> listProizvodi = CitajJsonProd();
            Korisnik trenutniKorisnik = GetKorisnik(k.Korisnicko_Ime);
            List<int> idProds = trenutniKorisnik.Objavljeni_Proizvodi;

            if (idProds != null)
            {
                foreach (var i in idProds)
                {
                    foreach (var item in listProizvodi)
                    {
                        if (item.Id == i)
                        {
                            proizvodiOdProdavca.Add(item);
                        }
                    }
                }
                return listProizvodi;
            }
            return null;
        }

        [HttpPost]
        [Route("api/prodavac/GetProdavac")]
        public Korisnik GetProdavac(Korisnik k)
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
        [Route("api/prodavac/IzmenaProdavac")]
        public bool IzmenaProdavac(Korisnik k)
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
        [Route("api/prodavac/sortiraj")]
        public List<Proizvod> Sortiraj(SortiranjePomoc sort)
        {
            List<Proizvod> listProizvodi = CitajJsonProd();
            if (sort.Sortiranje == "RastuceNaziv")
            {
                return listProizvodi.OrderBy(proizvod => proizvod.Naziv).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceNaziv")
            {
                return listProizvodi.OrderByDescending(proizvod => proizvod.Naziv).ToList();
            }
            else if (sort.Sortiranje == "RastuceCena")
            {
                return listProizvodi.OrderBy(proizvod => proizvod.Cena).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceCena")
            {
                return listProizvodi.OrderByDescending(proizvod => proizvod.Cena).ToList();
            }
            else if (sort.Sortiranje == "RastuceDatum")
            {
                return listProizvodi.OrderBy(proizvod => proizvod.Datum_Postavljanja).ToList();
            }
            else
            {
                return listProizvodi.OrderByDescending(proizvod => proizvod.Datum_Postavljanja).ToList();
            }

        }

        [HttpPost]
        [Route("api/prodavac/DodajProizvod")]
        public bool DodajProizvod(ProdavacProizvod prod)
        {
            string pathProdavac = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Registrovani.json";
            List<Korisnik> sviKorisnici = CitajJson();

            Proizvod noviProizvod = prod.Proizvod;
            noviProizvod.Id = testController.DobaviNoviIdProizvod();
            noviProizvod.Datum_Postavljanja = DateTime.Now;
            noviProizvod.Recenzija = new List<int>();
            noviProizvod.Dostupnost = true;

            

            if (!UpisiJson(noviProizvod))
            {
                return false;
            }

            foreach (var item in sviKorisnici)
            {
                if(item.Objavljeni_Proizvodi == null)
                {
                    item.Objavljeni_Proizvodi = new List<int>();
                }
                if (item.Korisnicko_Ime == prod.Korisnicko_Ime)
                {
                    item.Objavljeni_Proizvodi.Add(noviProizvod.Id);
                }
            }

            var jsonString = JsonConvert.SerializeObject(sviKorisnici, Formatting.Indented);
            File.WriteAllText(pathProdavac, jsonString);

            return true;
        }
        [HttpPost]
        [Route("api/prodavac/DeleteProizvod")]
        public bool DeleteProizvod(Proizvod p)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";

            foreach (var item in sviProizvodi)
            {
                if (item.Id == p.Id && item.Dostupnost == true)
                {
                    item.Obrisan = true;

                    var jsonString = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }
    }
}
