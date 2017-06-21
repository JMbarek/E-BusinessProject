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
    public class RegionController : Controller
    {

        private RegionRepository regionRepository;
        private ZoneRepository zoneRepository;

        public RegionController()
        {
            this.regionRepository = new RegionRepository(new ExcursionContext());
            this.zoneRepository = new ZoneRepository(new ExcursionContext());
        }

        //
        // GET: /Region/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Ajouter()
        {
            var SysLst = new List<string>();
            var SysQry = from b in zoneRepository.FindMany( x => x.CentreID == SessionData.CurrentUser.CentreID)
                          select b.Nom;
            SysLst.AddRange(SysQry.Distinct());
            ViewBag.ZoneList = new SelectList(SysLst);

            return View();
        }

        //Ajouter Theme
        [HttpPost]
        public ActionResult Ajouter(Excursion.Data.Region region, string Zone)
        {
            Excursion.Data.Region rg = new Data.Region();
            rg.Nom = region.Nom;
            rg.CodeRegion = region.CodeRegion;
            rg.ZoneID = zoneRepository.FindOne(x => x.Nom == Zone).ZoneID;
            regionRepository.Add(rg);
            regionRepository.Save();
            return RedirectToAction("Ajouter", new RouteValueDictionary(
                                  new { controller = "Excursion", action = "Ajouter" }));

        }






    }
}
