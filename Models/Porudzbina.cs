using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public enum status
    {
        AKTIVNA, IZVRSENA, OTKAZANA
    }
    public class Porudzbina
    {
        public int Id { get; set; }
        public Proizvod Proizvod { get; set; }
        public int Kolicina { get; set; }
        public string Kupac { get; set; }
        public DateTime Datum_Porudzbine { get; set; }
        public status Status { get; set; }
        public bool ObrisanaPor { get; set; }
    }
}