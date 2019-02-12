using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace ApplicationMVC.Areas.Prestataire.Models
{
    public class ViewModelAllClass
    {
        public List<Facture> facture { get; set; }
        public List<Affilie> affilie { get; set; }
        public DAL.Prestataire prest { get; set; }
    }
}