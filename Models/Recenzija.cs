using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public enum BrisanjeRecenzije
    {
        NijeObrisana,Obrisana
    }
    public class Recenzija
    {
        public int Id { get; set; }
        public Proizvod Proizvod { get; set; }
        public string Recezent { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj_recenzije { get; set; }
        public string Slika { get; set; }
        public int ProizvodId { get; set; }
        public BrisanjeRecenzije Brisanje { get; set; }
        public bool Odobreno { get; set; }
    }
}