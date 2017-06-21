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
    public class ZoneController : Controller
    {

        private ZoneRepository zoneRepository;
        private CentreRepository centreRepository;
        public ZoneController()
        {
            this.zoneRepository = new ZoneRepository(new ExcursionContext());
            this.centreRepository = new CentreRepository(new ExcursionContext());
        }



        //
        // GET: /Zone/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Ajouter()
        {
            var SysLst = new List<string>();
            var SysQry = from b in centreRepository.FindMany(x => x.CentreID == SessionData.CurrentUser.CentreID)
                         select b.Nom;
            SysLst.AddRange(SysQry.Distinct());
            ViewBag.CentreList = new SelectList(SysLst);
            return View();
        }

        //Ajouter Theme
        [HttpPost]
        public ActionResult Ajouter(Excursion.Data.Zone zone, string Centre)
        {
            Excursion.Data.Zone zn = new Data.Zone();
            zn.Nom = zone.Nom;
            zn.CodeZone = zone.CodeZone;
            zn.CentreID = centreRepository.FindOne(x => x.Nom == Centre).CentreID;
            zoneRepository.Add(zn);
            zoneRepository.Save();
            return RedirectToAction("Ajouter", new RouteValueDictionary(
                                  new { controller = "Excursion", action = "Ajouter" }));

        }







    }
}
