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
    public class RegistracijaController : ApiController
    {
        [HttpGet]
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

       public bool UpisiJson(Korisnik k)
        {
            List<Korisnik> registrovaniKorisnik = CitajJson();
            string jsonPath = "/Users/pc/Desktop/ProjekatWeb1/WebShop/App_Data/Registrovani.json";
            bool found = true;
            if(registrovaniKorisnik == null)
            {
                registrovaniKorisnik = new List<Korisnik>();
                registrovaniKorisnik.Add(k);
            }
            else
            {
                if(registrovaniKorisnik != null)
                {
                    foreach(var item in registrovaniKorisnik)
                    {
                        if (item.Korisnicko_Ime.Equals(k.Korisnicko_Ime))
                        {
                            found = false ;
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
            if(found)
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

        [HttpPost]
        [Route("api/registracija/Registrovanje")]
        public bool Registrovanje(Korisnik k)
        {
            if(UpisiJson(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
