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
    public class AdminController : ApiController
    {
        public bool UpisiJson(Korisnik k)
        {
            List<Korisnik> registrovaniKorisnik = CitajJson();
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Registrovani.json";
            bool found = true;
            if (registrovaniKorisnik == null)
            {
                registrovaniKorisnik = new List<Korisnik>();
                registrovaniKorisnik.Add(k);
            }
            else
            {
                if (registrovaniKorisnik != null)
                {
                    foreach (var item in registrovaniKorisnik)
                    {
                        if (item.Korisnicko_Ime.Equals(k.Korisnicko_Ime))
                        {
                            found = false;
                            break;
                        }

                    }
                    //return true;

                }
                else
                {
                    registrovaniKorisnik.Add(k);
                }
            }
            if (found)
            {
                registrovaniKorisnik.Add(k);
                var path = JsonConvert.SerializeObject(registrovaniKorisnik, Formatting.Indented);
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

        [HttpGet]
        [Route("api/admin/GetKorisnici")]
        // GET: Admin
        public List<Korisnik> GetKorisnici()
        {
            List<Korisnik> listKorisnici = CitajJson();
            List<Korisnik> listKupciProdavci = new List<Korisnik>();

            foreach (var item in listKorisnici)
            {
                if (item.Uloga.ToString() != "Administrator")
                {
                    listKupciProdavci.Add(item);
                }

            }
            return listKupciProdavci;
        }

        [HttpGet]
        // GET: Admin
        [Route("api/admin/GetProizvodi")]
        public List<Proizvod> GetProizvodi()
        {
            List<Proizvod> listProizvodi = CitajJsonProd();
            return listProizvodi;
        }

        [HttpGet]
        // GET: Admin
        [Route("api/admin/GetRecenzije")]
        public List<Recenzija> GetRecenzije()
        {
            List<Recenzija> listRecenzije = CitajJsonRecenzija();
            return listRecenzije;
        }

        [HttpPost]
        // GET: Admin
        [Route("api/admin/GetAdmin")]
        public Korisnik GetAdmin(Korisnik k)
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

        [HttpGet]
        // GET: Admin

        [Route("api/admin/GetPorudzbine")]
        public List<Porudzbina> GetPorudzbine()
        {
            List<Porudzbina> listPorudzbine = CitajJsonPorudzbine();
            return listPorudzbine;
        }

        [HttpPost]
        [Route("api/admin/IzmenaKorisnika")]
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
        [Route("api/admin/DeleteKorisnik")]
        public bool DeleteKorisnik(Korisnik k)
        {
            List<Korisnik> sviKorisnici = CitajJson();
            List<Porudzbina> svePorudzbine = CitajJsonPorudzbine();
            List<Proizvod> sviProizvodi = CitajJsonProd();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Registrovani.json";
            string pathPorudzbine = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Porudzbine.json";
            string pathProd = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";

            foreach (var item in sviKorisnici)
            {
                if (item.Korisnicko_Ime == k.Korisnicko_Ime)
                {
                    item.Obrisan = true;
                    //za korisnika proveriti i aktivne porudzbine, ako obrisemo korisnika sa aktivnim
                    //porudzbinama, brisu se i porudzbine i proizvodi se vracaju na stanje
                    foreach (var i in svePorudzbine)
                    {
                        if (i.Kupac == item.Korisnicko_Ime && i.Status == 0)
                        {
                            if (i.ObrisanaPor == false)
                            {
                                i.ObrisanaPor = true;
                            }

                            foreach (var prod in sviProizvodi)
                            {
                                if (prod.Id == i.Proizvod.Id)
                                {
                                    prod.Kolicina += i.Kolicina;
                                    if (prod.Dostupnost == false)
                                    {
                                        prod.Dostupnost = true;
                                    }
                                }
                            }
                        }
                    }

                    var jsonString = JsonConvert.SerializeObject(sviKorisnici, Formatting.Indented);
                    var jsonString2 = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
                    var jsonString3 = JsonConvert.SerializeObject(svePorudzbine, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    File.WriteAllText(pathProd, jsonString2);
                    File.WriteAllText(pathPorudzbine, jsonString3);
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("api/admin/DeleteProizvod")]
        public bool DeleteProizvod(Proizvod p)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";

            foreach (var item in sviProizvodi)
            {
                if (item.Id == p.Id)
                {
                    item.Obrisan = true;

                    var jsonString = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("api/admin/IzmeniRecenzijuOdobravanje")]
        public bool IzmeniRecenzijuOdobravanje(Recenzija r)
        {
            List<Recenzija> sveRecenzije = CitajJsonRecenzija();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Recenzije.json";

            foreach (var item in sveRecenzije)
            {
                if (item.Id == r.Id)
                {
                    item.Odobreno = true;

                    var jsonString = JsonConvert.SerializeObject(sveRecenzije, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("api/admin/IzmenaIzvrsenaPorudzbina")]
        public bool IzmenaIzvrsenaPorudzbina(Porudzbina p)
        {
            List<Porudzbina> svePorudzbine = CitajJsonPorudzbine();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Porudzbine.json";

            foreach (var item in svePorudzbine)
            {
                if (item.Id == p.Id)
                {
                    if (item.Status == status.AKTIVNA)
                    {
                        item.Status = status.IZVRSENA;
                    }

                    var jsonString = JsonConvert.SerializeObject(svePorudzbine, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }

        public Proizvod DobaviProizvodPrekoId(int id)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            foreach (var item in sviProizvodi)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        [HttpPost]
        [Route("api/admin/IzmenaOtkazanaPorudzbina")]
        public bool IzmenaOtkazanaPorudzbina(Porudzbina p)
        {
            List<Porudzbina> svePorudzbine = CitajJsonPorudzbine();
            List<Proizvod> sviProizvodi = CitajJsonProd();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Porudzbine.json";
            string pathProd = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";

            foreach (var item in svePorudzbine)
            {
                if (item.Id == p.Id)
                {
                    //nasli smo trazenu porudzbinu, vracamo proizvodu kolicinu
                    //ako je proizvod nedostupan znaci da ga nema (kolicina 0), a ako je
                    //proizvod obrisan onda ne mozemo kreirati porudzbinu nad njim  
                    if (item.Status == status.AKTIVNA)
                    {
                        item.Status = status.OTKAZANA;
                        foreach (var i in sviProizvodi)
                        {
                            if (i.Id == item.Proizvod.Id)
                            {
                                i.Kolicina += item.Kolicina;
                                if (i.Dostupnost == false)
                                {
                                    i.Dostupnost = true;
                                }
                            }
                        }
                    }

                    var jsonString = JsonConvert.SerializeObject(svePorudzbine, Formatting.Indented);
                    var jsonString2 = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    File.WriteAllText(pathProd, jsonString2);
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("api/admin/DodavanjeProdavca")]
        public bool DodavanjeProdavca(Korisnik k)
        {
            if (UpisiJson(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        [Route("api/admin/pretraziKorisnika")]
        public List<Korisnik> PretraziKorisnika(PretragaKorisnika pretraga)
        {
            List<Korisnik> korisnici = CitajJson();
            List<Korisnik> filtriraniKorisnici = new List<Korisnik>();

            foreach (var korisnik in korisnici)
            {
                bool isImeMatched = string.IsNullOrEmpty(pretraga.Ime) || korisnik.Ime.Contains(pretraga.Ime);
                bool isPrezimeMatched = string.IsNullOrEmpty(pretraga.Prezime) || korisnik.Prezime.Contains(pretraga.Prezime);
                bool isDatumMatched = (string.IsNullOrEmpty(pretraga.MinDatum) && string.IsNullOrEmpty(pretraga.MaxDatum)) ||
                                       Int32.Parse(pretraga.MinDatum) <= Int32.Parse(korisnik.Datum_Rodjenja) && Int32.Parse(korisnik.Datum_Rodjenja) <= Int32.Parse(pretraga.MaxDatum);
                bool isUlogaMatched = pretraga.Uloga == 4 || pretraga.Uloga == (int)korisnik.Uloga;

                if (isImeMatched && isPrezimeMatched && isDatumMatched && isUlogaMatched)
                {
                    filtriraniKorisnici.Add(korisnik);
                }
            }

            return filtriraniKorisnici;
        }
        [HttpPost]
        [Route("api/admin/sortiraj")]
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
        [Route("api/admin/sortirajKorisnika")]
        public List<Korisnik> SortirajKorisnika(SortiranjeKorisnik sort)
        {
            List<Korisnik> listKorisnisk = CitajJson();
            if (sort.Sortiranje == "RastuceIme")
            {
                return listKorisnisk.OrderBy(k => k.Ime).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceIme")
            {
                return listKorisnisk.OrderByDescending(k => k.Ime).ToList();
            }
            else if (sort.Sortiranje == "RastuceDatumi")
            {
                return listKorisnisk.OrderBy(k => k.Datum_Rodjenja).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceDatumi")
            {
                return listKorisnisk.OrderByDescending(k => k.Datum_Rodjenja).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceKupac")
            {
                return listKorisnisk.OrderByDescending(k => k.Uloga).ToList();
            }
            else if (sort.Sortiranje == "RastuceKupac")
            {
                return listKorisnisk.OrderBy(k => k.Uloga).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceProdavac")
            {
                return listKorisnisk.OrderByDescending(k => k.Uloga).ToList();
            }
            else if (sort.Sortiranje == "RastuceProdavac")
            {
                return listKorisnisk.OrderBy(k => k.Uloga).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceAdmin")
            {
                return listKorisnisk.OrderByDescending(k => k.Uloga).ToList();
            }
            else
            {
                return listKorisnisk.OrderBy(k => k.Uloga).ToList();
            }
        }
    }
    public class PretragaKorisnika
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string MinDatum { get; set; }
        public string MaxDatum { get; set; }
        public int Uloga { get; set; }
    }
    public class SortiranjeKorisnik
    {
        public string Sortiranje { get; set; }
    }
}
