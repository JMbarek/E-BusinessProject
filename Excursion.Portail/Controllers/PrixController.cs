using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Excursion.Portail.Filters;
using Excursion.Portail.Models;
using Excursion.Business.Repositories;
using Excursion.Data;
using System.Web.Routing;
using Excursion.Portail.Utilities;

namespace Excursion.Portail.Controllers
{
    public class PrixController : Controller
    {


        private PrixRepository prixRepository;
        private ZonePrixRepository zonePrixRepository;
        private ZoneRepository zoneRepository;
        public PrixController()
        {
            this.prixRepository = new PrixRepository(new ExcursionContext());
            this.zonePrixRepository = new ZonePrixRepository(new ExcursionContext());
            this.zoneRepository = new ZoneRepository(new ExcursionContext());
        }



        //
        // GET: /Prix/

        public ActionResult Index()
        {
            return View();
        }


        //Ajouter prix
        [HttpPost]
        public ActionResult Ajouter(Excursion.Portail.Models.Prix prix, string Device, List<string> Zones)
        {
            Excursion.Data.Prix pr = new Data.Prix();
            pr.DateDebut = prix.DateDebutP;
            pr.DateFin = prix.DateFinP;
            pr.PrixAdulte = prix.PrixAdulte;
            pr.PrixEnfant = prix.PrixEnfant;
            pr.PrixBebe = prix.PrixBebe;
            pr.Monnaie = Device;
            prixRepository.Add(pr);
            prixRepository.Save();

            foreach (string zone in Zones)
            {
                ZonePrix zp = new ZonePrix();
                zp.PrixID = pr.PrixID;
                zp.ZoneID = zoneRepository.FindOne(x => x.Nom == zone).ZoneID;
                zonePrixRepository.Add(zp);
                zonePrixRepository.Save();
            }

            return RedirectToAction("Ajouter", new RouteValueDictionary(
                                  new { controller = "Excursion", action = "Ajouter" }));

        }



    }
}
