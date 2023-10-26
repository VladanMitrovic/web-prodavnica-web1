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
    public class TestController : ApiController
    {
        public static int idProizvod = -1;
        public static int idPorudzbina = -1;
        public static int idRecenzije = -1;


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

        public int DobaviNoviIdProizvod()
        {
            List<Proizvod> sviProizvodi = CitajJsonProd();
            Proizvod maxProd = sviProizvodi.OrderByDescending(obj => obj.Id).FirstOrDefault();

            if (maxProd != null) //nema porudzbina, id krece iz pocetka
            {
                idProizvod = ++maxProd.Id;
            }
            else
            {
                idProizvod = 0;
            }

            return idProizvod;
        }

        public int DobaviNoviIdPorudzbine()
        {
            List<Porudzbina> svePorudzbine = CitajJsonPorudzbine();
            Porudzbina maxPor = svePorudzbine.OrderByDescending(obj => obj.Id).FirstOrDefault();

            if (maxPor != null) //nema porudzbina, id krece iz pocetka
            {
                idPorudzbina = ++maxPor.Id;
            }
            else
            {
                idPorudzbina = 0;
            }

            return idPorudzbina;
        }

        public int DobaviNoviIdRecenzije()
        {
            List<Recenzija> sveRecenzije = CitajJsonRecenzija();
            Recenzija maxRecenzija = sveRecenzije.OrderByDescending(obj => obj.Id).FirstOrDefault();

            if (maxRecenzija != null) //nema porudzbina, id krece iz pocetka
            {
                idRecenzije = ++maxRecenzija.Id;
            }
            else
            {
                idRecenzije = 0;
            }

            return idRecenzije;
        }
    }
}
