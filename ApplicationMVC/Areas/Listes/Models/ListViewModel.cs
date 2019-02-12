using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace ApplicationMVC.Areas.Listes.Models
{
    public class ListViewModel
    {
        public List<DAL.Prestataire> prest { get; set; }
        public List<DAL.Adherent> adher { get; set; }
        public List<DAL.Affilie> affil { get; set; }
        public List<DAL.Police> polic { get; set; }
        public List<DAL.Facture> factu { get; set; }
        public List<DAL.Ligne_PolicePlafond> PP { get; set; }
    }
}