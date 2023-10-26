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
    public class KupacController : ApiController
    {
        public static TestController testController = new TestController();

        public bool UpisiJson(Porudzbina por)
        {
            List<Porudzbina> svePorudzbine = CitajJsonPorudzbine();
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Porudzbine.json";
            bool found = true;
            if (svePorudzbine == null)
            {
                svePorudzbine = new List<Porudzbina>();
                svePorudzbine.Add(por);
            }
            else
            {
                if (svePorudzbine != null)
                {
                    foreach (var item in svePorudzbine)
                    {
                        if (item.Id.Equals(por.Id))
                        {
                            found = false;
                            break;
                        }

                    }
                    //return true;

                }
                else
                {
                    svePorudzbine.Add(por);
                }
            }
            if (found)
            {
                svePorudzbine.Add(por);
                var path = JsonConvert.SerializeObject(svePorudzbine, Formatting.Indented);
                File.WriteAllText(jsonPath, path);
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool UpisiJsonRecenzija(Recenzija rec)
        {
            List<Recenzija> sveRecenzije = CitajJsonRecenzija();
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Recenzije.json";
            bool found = true;
            if (sveRecenzije == null)
            {
                sveRecenzije = new List<Recenzija>();
                sveRecenzije.Add(rec);
            }
            else
            {
                if (sveRecenzije != null)
                {
                    foreach (var item in sveRecenzije)
                    {
                        if (item.Id.Equals(rec.Id))
                        {
                            found = false;
                            break;
                        }

                    }
                    //return true;

                }
                else
                {
                    sveRecenzije.Add(rec);
                }
            }
            if (found)
            {
                sveRecenzije.Add(rec);
                var path = JsonConvert.SerializeObject(sveRecenzije, Formatting.Indented);
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

        [HttpPost]
        // GET: Admin
        [Route("api/kupac/GetKupac")]
        public Korisnik GetKupac(Korisnik k)
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
        [Route("api/kupac/IzmenaKupac")]
        public bool IzmenaKupac(Korisnik k)
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
        [Route("api/kupac/DodajOmiljeni")]
        public bool DodajOmiljeni(ProdavacProizvod pp)
        {
            List<Korisnik> sviKorisnici = CitajJson();
            Proizvod prod = new Proizvod();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Registrovani.json";

            foreach (var item in sviKorisnici)
            {
                if (item.Korisnicko_Ime == pp.Korisnicko_Ime)
                {
                    prod = PronadjiProizvod(pp.Proizvod.Id);
                    if (prod != null)
                    {
                        if (item.Omiljeni_Proizvodi == null)
                        {
                            item.Omiljeni_Proizvodi = new List<Proizvod>();
                        }

                        item.Omiljeni_Proizvodi.Add(prod);

                        var jsonString = JsonConvert.SerializeObject(sviKorisnici, Formatting.Indented);
                        File.WriteAllText(path, jsonString);
                        return true;
                    }
                }
            }

            return false;
        }
       
        public bool ProveraDaLiPostoji(List<Proizvod> omiljeniProizvodi, int id)
        {
            foreach (var item in omiljeniProizvodi)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }

            return false; //ako ne postoji id u omiljenima znaci da ga nema
        }
        public Porudzbina PronadjiPorudzbinu(int id)
        {
            List<Porudzbina> svePorudzbine = CitajJsonPorudzbine();
            foreach (var item in svePorudzbine)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        [HttpPost]
        [Route("api/kupac/DodajPorudzbinu")]
        public bool DodajPorudzbinu(PorucivanjeProizvoda testPor)
        {
            string proizvodPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";
            Proizvod prod = PronadjiProizvod(testPor.Proizvod.Id);
            if (prod != null)
            {
                prod.Kolicina -= testPor.Kolicina;
                if (prod.Kolicina < 0)
                {
                    return false;
                }
                else if (prod.Kolicina == 0)
                {
                    prod.Dostupnost = false;
                    //proizvoda vise nema na stanju posle narucivanja, dodati porudzbinu jos
                }

                //azuriranje stanja u proizvod.json da se promeni kolicina
                List<Proizvod> sviProizvodi = IzmeniProizvod(prod);
                var path = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
                File.WriteAllText(proizvodPath, path);

                Porudzbina novaPorudzbina = new Porudzbina();
                novaPorudzbina.Proizvod = new Proizvod();

                novaPorudzbina.Kupac = testPor.Korisnicko_Ime;
                novaPorudzbina.Kolicina = testPor.Kolicina;
                novaPorudzbina.Id = testController.DobaviNoviIdPorudzbine();
                novaPorudzbina.Datum_Porudzbine = DateTime.Now;
                novaPorudzbina.ObrisanaPor = false;
                novaPorudzbina.Proizvod.Id = testPor.Proizvod.Id;
                novaPorudzbina.Proizvod.Naziv = prod.Naziv;
                novaPorudzbina.Status = status.AKTIVNA;

                UpisiJson(novaPorudzbina);
                return true;

            }

            return false;
        }
        [HttpPost]
        [Route("api/kupac/DodajRecenziju")]
        public bool DodajRecenziju(Recenzija rec)
        {
            string pathProizvod = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Proizvod.json";
            List<Proizvod> sviProizvodi = CitajJsonProd();

            Recenzija novaRecenzija = rec;
            novaRecenzija.Odobreno = false;
            novaRecenzija.Id = testController.DobaviNoviIdRecenzije();

            Proizvod proizvodRecenziran = PronadjiProizvod(rec.Proizvod.Id);
            novaRecenzija.Proizvod.Naziv = proizvodRecenziran.Naziv;

            if (!UpisiJsonRecenzija(novaRecenzija))
            {
                return false;
            }

            foreach (var item in sviProizvodi)
            {
                if (item.Id == rec.Proizvod.Id)
                {
                    if (item.Recenzija == null)
                    {
                        item.Recenzija = new List<int>();

                    }
                    item.Recenzija.Add(novaRecenzija.Id);
                }
            }

            var jsonString = JsonConvert.SerializeObject(sviProizvodi, Formatting.Indented);
            File.WriteAllText(pathProizvod, jsonString);

            return true;
        }

        public Proizvod PronadjiProizvod(int id)
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
       
        public List<Proizvod> IzmeniProizvod(Proizvod prod)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            for (int i = 0; i < sviProizvodi.Count; i++)
            {
                if (sviProizvodi[i].Id == prod.Id)
                {
                    sviProizvodi[i] = prod;
                }
            }



            return sviProizvodi;
        }

        [HttpPost]
        [Route("api/kupac/GetPorudzbine")]
        public List<Porudzbina> GetPorudzbine(Porudzbina por)
        {
            List<Porudzbina> listPorudzbine = CitajJsonPorudzbine();
            List<Porudzbina> listaKupacPorudzbine = new List<Porudzbina>();
            foreach (var item in listPorudzbine)
            {
                if (item.Kupac == por.Kupac)
                {
                    listaKupacPorudzbine.Add(item);
                }
            }
            return listaKupacPorudzbine;
        }

        [HttpPost]
        [Route("api/kupac/IzmenaIzvrsenaPorudzbina")]
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

        [HttpPost]
        // GET: Admin
        [Route("api/kupac/GetRecenzije")]
        public List<Recenzija> GetRecenzije(Recenzija r)
        {
            List<Recenzija> listRecenzije = CitajJsonRecenzija();
            List<Recenzija> listRecenzijaKupca = new List<Recenzija>();
            foreach (var item in listRecenzije)
            {
                if (item.Recezent == r.Recezent)
                {
                    listRecenzijaKupca.Add(item);
                }
            }
            return listRecenzijaKupca;
        }

        [HttpPost]
        [Route("api/kupac/getUloga")]
        // GET: Admin
        public uloga GetUloga(Korisnik k)
        {
            List<Korisnik> listKorisnici = CitajJson();
            List<Korisnik> listKupciProdavci = new List<Korisnik>();

            foreach (var item in listKorisnici)
            {
                if (item.Korisnicko_Ime == k.Korisnicko_Ime)
                {
                    return item.Uloga;
                }

            }
            return uloga.Nepoznato;
        }
        [HttpPost]
        [Route("api/kupac/GetOmiljeniProizvodi")]
        public List<Proizvod> GetOmiljeniProizvodi(Korisnik k)
        {
            Korisnik trenutniKupac = GetKupac(k);
            List<int> idProd = new List<int>();
            if(trenutniKupac.Omiljeni_Proizvodi == null)
            {
                trenutniKupac.Omiljeni_Proizvodi = new List<Proizvod>();
            }
            foreach (var item in trenutniKupac.Omiljeni_Proizvodi)
            {
                idProd.Add(item.Id);
            }

            return DobaviProizvode(idProd);
        }

        public List<Proizvod> DobaviProizvode(List<int> idProds)
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            List<Proizvod> omiljeniProizvodi = new List<Proizvod>();
            foreach (var id in idProds)
            {
                foreach (var item in sviProizvodi)
                {
                    if (item.Id == id)
                    {
                        omiljeniProizvodi.Add(item);
                    }
                }
            }

            return omiljeniProizvodi;
        }

        [HttpPost]
        [Route("api/kupac/DeleteRecenzija")]
        public bool DeleteRecenzija(Proizvod p)
        {
            List<Recenzija> sveRecenzije = CitajJsonRecenzija();
            string path = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Recenzije.json";

            foreach (var item in sveRecenzije)
            {
                if (item.Id == p.Id)
                {
                    item.Brisanje = BrisanjeRecenzije.Obrisana;

                    var jsonString = JsonConvert.SerializeObject(sveRecenzije, Formatting.Indented);
                    File.WriteAllText(path, jsonString);
                    return true;
                }
            }
            return false;
        }

    }
}
