using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public enum pol
    {
        Musko = 1, Zensko = 2
    }
    public enum uloga
    {
        Kupac, Prodavac, Administrator, Nepoznato
    }
    public class Korisnik
    {
        public string Korisnicko_Ime { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public pol Pol { get; set; }
        public string Email { get; set; }
        public string Datum_Rodjenja { get; set; }
        public uloga Uloga { get; set; }
        public List<int> Prudzbina { get; set; } 
        public List<Proizvod> Omiljeni_Proizvodi { get; set; }
        public List<int> Objavljeni_Proizvodi { get; set; }
        public bool Obrisan { get; set; }
    }
}