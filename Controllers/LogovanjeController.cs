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
    public class LogovanjeController : ApiController
    {
        //public static Korisnik TrenutniKorisnik = new Korisnik();
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

        [HttpPost]
        [Route("api/logovanje/LogIn")]
        public bool LogIn(Korisnik korisnik)
        {
            List<Korisnik> k = CitajJson(); //Lista registrovanih korisnika
            if(k != null)
            {
                foreach(var item in k)
                {
                    if(item.Korisnicko_Ime.Equals(korisnik.Korisnicko_Ime) && item.Lozinka.Equals(korisnik.Lozinka))
                    {
                        UpravljanjeKorisnikom.TrenutniKorisnik = korisnik;
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public class UpravljanjeKorisnikom
    {
        public static Korisnik TrenutniKorisnik = new Korisnik();
    }
}
