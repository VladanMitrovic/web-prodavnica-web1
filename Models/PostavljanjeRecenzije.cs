using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class PostavljanjeRecenzije
    {
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public Porudzbina Porudzbina { get; set; }
        public string Slika { get; set; }
    }
}