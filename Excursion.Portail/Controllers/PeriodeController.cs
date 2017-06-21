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
    public class PeriodeController : Controller
    {

        private PeriodeRepository periodeRepository;

        public PeriodeController()
        {
            this.periodeRepository = new PeriodeRepository(new ExcursionContext());
        }



        //
        // GET: /Periode/
        public ActionResult Index()
        {
            return View();
        }





        //Ajouter periode
        [HttpPost]
        public ActionResult Ajouter(Excursion.Data.Periode periode)
        {
            Excursion.Data.Periode pr = new Data.Periode();
            pr.DateDebut = periode.DateDebut;
            pr.DateFin = periode.DateFin;
            periodeRepository.Add(pr);
            periodeRepository.Save();
            return RedirectToAction("Ajouter", new RouteValueDictionary(
                                  new { controller = "Excursion", action = "Ajouter"}));
        }





    }
}
