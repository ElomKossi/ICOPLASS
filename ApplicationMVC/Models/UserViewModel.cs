using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace ApplicationMVC.Models
{
    public class UserViewModel
    {
        public List<CompteUtilisateur> Users { get; set; }
        public CompteUtilisateur User { get; set; }
        public List<Connexion> Connexions { get; set; }
        public bool DejaConnecte(CompteUtilisateur x)
        {

            return Connexions.Any(p => p.CompteUtilisateur.Equals(x) && p.Fin == null);

        }
    }
}