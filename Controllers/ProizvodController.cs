using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class ProizvodController : ApiController
    {
        public static List<Proizvod> proizvodi = new List<Proizvod>();
        public List<Proizvod> CitajJson()
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

        [HttpGet]
        public List<Proizvod> Get()
        {
            proizvodi = CitajJson();
            List<Proizvod> proizvod = new List<Proizvod>();
            for(int i = 0; i < proizvodi.Count(); i++)
            {
                proizvod.Add(proizvodi[i]);
            }
            proizvodi = proizvod;
            return proizvodi;
        }
        
        [HttpPost]
        [Route("api/proizvod/sortiraj")]
        public List<Proizvod> Sortiraj(SortiranjePomoc sort)
        {
           
            if (sort.Sortiranje == "RastuceNaziv")
            {
                return proizvodi.OrderBy(proizvod => proizvod.Naziv).ToList();
            }
            else if (sort.Sortiranje == "OpadajuceNaziv")
            {
                return proizvodi.OrderByDescending(proizvod => proizvod.Naziv).ToList();
            }
            else if (sort.Sortiranje == "RastuceCena")
            {
                return proizvodi.OrderBy(proizvod => proizvod.Cena).ToList();
            }
            else if(sort.Sortiranje == "OpadajuceCena")
            {
                return proizvodi.OrderByDescending(proizvod => proizvod.Cena).ToList();
            }
            else if (sort.Sortiranje == "RastuceDatum")
            {
                return proizvodi.OrderBy(proizvod => proizvod.Datum_Postavljanja).ToList();
            }
            else
            {
                return proizvodi.OrderByDescending(proizvod => proizvod.Datum_Postavljanja).ToList();
            }

        }
        [HttpPost]
        [Route("api/proizvod/PretraziProizvod")]
        public List<Proizvod> PretraziProizvod(PretragaKriterijum kriterijum)
        {
            List<Proizvod> filtriraniProizvodi = new List<Proizvod>();

            foreach (var item in proizvodi)
            {
                bool isNazivMatched = string.IsNullOrEmpty(kriterijum.Naziv) || item.Naziv.Equals(kriterijum.Naziv);
                //bool isCenaMatched = string.IsNullOrEmpty(kriterijum.MinCena) || string.IsNullOrEmpty(kriterijum.MaxCena) || (Int32.Parse(item.Cena) >= Int32.Parse(kriterijum.MinCena) && Int32.Parse(item.Cena) <= Int32.Parse(kriterijum.MaxCena));
                bool isCenaMatched = string.IsNullOrEmpty(kriterijum.MinCena) || string.IsNullOrEmpty(kriterijum.MaxCena) ||
           (decimal.TryParse(item.Cena, out decimal cena) && cena >= decimal.Parse(kriterijum.MinCena) && cena <= decimal.Parse(kriterijum.MaxCena));
                bool isGradMatched = string.IsNullOrEmpty(kriterijum.Grad) || item.Grad.Equals(kriterijum.Grad);

                if (isNazivMatched && isCenaMatched && isGradMatched)
                {
                    filtriraniProizvodi.Add(item);
                }
            }

            return filtriraniProizvodi;
        }
    }

    public class PretragaKriterijum
    {
        public string Naziv { get; set; }
        public string MinCena { get; set; }
        public string MaxCena { get; set; }
        public string Grad { get; set; }
    }

}
