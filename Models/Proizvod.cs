using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Proizvod
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Cena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public string Slika { get; set; }
        public DateTime Datum_Postavljanja { get; set; }
        public string Grad { get; set; }
        public List<int> Recenzija { get; set; } 
        public bool Dostupnost { get; set; }
        public bool Obrisan { get; set; }
    }
}