using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class PorucivanjeProizvoda
    {
        public string Korisnicko_Ime { get; set; }
        public Proizvod Proizvod { get; set; }
        public int Kolicina { get; set; }
    }
}