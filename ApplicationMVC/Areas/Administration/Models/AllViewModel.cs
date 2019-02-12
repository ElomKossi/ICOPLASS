using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace ApplicationMVC.Areas.Administration.Models
{
    public class AllViewModel
    {
        public List<DAL.Prestataire> prestataire { get; set; }
        public List<Plafond> plafond { get; set; }
        public List<DAL.CompteUtilisateur> user { get; set; }
        public List<Profil> profil { get; set; }
        public List<Connexion> connexion { get; set; }
    }
}