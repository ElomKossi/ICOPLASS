using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationMVC.Areas.Prestataire.Models
{
    public class FactureNew
    {
        public int IdFacture { get; set; }
        public System.DateTime DateFacture { get; set; }
        public double Montant { get; set; }
        public double RestePlafond { get; set; }
        public int IdPlafond { get; set; }
        public int IdPolice { get; set; }
        public int IdAdherent { get; set; }
        public int IdAffilie { get; set; }
        public int IdPrestataire { get; set; }
    }
}