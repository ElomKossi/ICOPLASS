using System.Web.Mvc;

namespace ApplicationMVC.Areas.Prestataire
{
    public class PrestataireAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Prestataire";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Prestataire_default",
                "Prestataire/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}