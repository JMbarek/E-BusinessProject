using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursion.Business.Repositories;
using Excursion.Data;
using Excursion.Portail.Utilities;
using System.Web.Routing;

namespace Excursion.Portail.Controllers
{
    public class SortieController : Controller
    {

        private ExcursionRepository excursionRepository;
        private PeriodeRepository periodeRepository;
        private SortieParSemaineRepository sortieParSemaineRepository;
        private PrixRepository prixRepository;
        private TypeExcRepository typeExcRepository;
        private JourRepository jourRepository;
        public SortieController()
        {
            this.sortieParSemaineRepository = new SortieParSemaineRepository(new ExcursionContext());
            this.periodeRepository = new PeriodeRepository(new ExcursionContext());
            this.prixRepository = new PrixRepository(new ExcursionContext());
            this.excursionRepository = new ExcursionRepository(new ExcursionContext());
            this.typeExcRepository = new TypeExcRepository(new ExcursionContext());
            this.jourRepository = new JourRepository(new ExcursionContext());
        }




        //[HttpGet]
        public ActionResult Ajouter()
        {
            #region dropdownlist
            var SysLst = new List<string>();
            if ( MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
            {
               var SysQry = from b in excursionRepository.GetAll()
                         select b.Nom_fr;
               SysLst.AddRange(SysQry.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
            {
                var SysQry = from b in excursionRepository.GetAll() select b.Nom_en;
                SysLst.AddRange(SysQry.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
            {
                var SysQry = from b in excursionRepository.GetAll() select b.Nom_de;
                SysLst.AddRange(SysQry.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
            {
                var SysQry = from b in excursionRepository.GetAll() select b.Nom_it;
                SysLst.AddRange(SysQry.Distinct());
            }
            ViewBag.ExcursionList = new SelectList(SysLst);

            var SysLst1 = new List<string>();
            var SysQry1 = from b in typeExcRepository.GetAll()
                          select b.Type;
            SysLst1.AddRange(SysQry1.Distinct());
            ViewBag.TypeList = new SelectList(SysLst1);

            var SysLst2 = new List<string>();
            SysLst2.Add("Valide");
            SysLst2.Add("Non Valide");
            ViewBag.EtatList = new SelectList(SysLst2);

            #endregion
            return View();
        }

        // ajouter excursion
        [HttpPost]
        public ActionResult Ajouter(Excursion.Portail.Models.Sortie sortie, string Type, string Excursion, string Etat)
        {
            Excursion.Data.SortieParSemaine st = new SortieParSemaine();
            st.HeureDepart = sortie.HeureDepart;
            st.TypeExcID = typeExcRepository.FindOne(x => x.Type == Type).TypeExcID;
            if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
            {
                st.ExcursionID = excursionRepository.FindOne(x => x.Nom_fr == Excursion).ExcursionID;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
            {
                st.ExcursionID = excursionRepository.FindOne(x => x.Nom_de == Excursion).ExcursionID;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
            {
                st.ExcursionID = excursionRepository.FindOne(x => x.Nom_en == Excursion).ExcursionID;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
            {
                st.ExcursionID = excursionRepository.FindOne(x => x.Nom_it == Excursion).ExcursionID;
            }

            Excursion.Data.Jour jour = new Jour();
            if (Etat == "Valide") { jour.Etat = "V"; } else { jour.Etat = "N"; }
            jour.Date = sortie.DateSortie;
            int NumJour = 1 + (int)sortie.DateSortie.DayOfWeek;
            jour.NumeroJ = NumJour;
            jourRepository.Add(jour);
            jourRepository.Save();

            st.JourID = jour.JourID;

            sortieParSemaineRepository.Add(st);
            sortieParSemaineRepository.Save();


            #region dropdownlist
            var SysLst = new List<string>();
            if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
            {
                var SysQry = from b in excursionRepository.GetAll()
                             select b.Nom_fr;
                SysLst.AddRange(SysQry.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
            {
                var SysQry = from b in excursionRepository.GetAll() select b.Nom_en;
                SysLst.AddRange(SysQry.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
            {
                var SysQry = from b in excursionRepository.GetAll() select b.Nom_de;
                SysLst.AddRange(SysQry.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
            {
                var SysQry = from b in excursionRepository.GetAll() select b.Nom_it;
                SysLst.AddRange(SysQry.Distinct());
            }
            ViewBag.ExcursionList = new SelectList(SysLst);

            var SysLst1 = new List<string>();
            var SysQry1 = from b in typeExcRepository.GetAll()
                          select b.Type;
            SysLst1.AddRange(SysQry1.Distinct());
            ViewBag.TypeList = new SelectList(SysLst1);

            var SysLst2 = new List<string>();
            SysLst2.Add("Valide");
            SysLst2.Add("Non Valide");
            ViewBag.EtatList = new SelectList(SysLst2);

            #endregion
            return View();
        }



        public ActionResult Search(Excursion.Portail.Models.SearchExcursionModel searchModel, string Region, List<string> Themes, string VilleArrive)
        {


            return RedirectToAction("Index", new RouteValueDictionary(
                                 new { controller = "Home", action = "Index" }));

        }

        [HttpPost]
        public ActionResult Search2(Excursion.Portail.Models.SearchExcursionModel2 jsonString)
        {
            List<string> sortiesList = new List<string>();
            sortiesList.Add("ppppp");
            return Json(new { SortiesList = sortiesList });
            //return RedirectToAction("Index", new RouteValueDictionary(
            //                     new { controller = "Home", action = "Index" }));

        }

        public ActionResult SearchByTheme(string Theme)
        {

            return RedirectToAction("Index", new RouteValueDictionary(
                                 new { controller = "Home", action = "Index" }));
        }


        //
        // GET: /Sortie/

        public ActionResult IndexAll()
        {
            List<SortieParSemaine> Listsorties = new List<SortieParSemaine>();
            Listsorties = sortieParSemaineRepository.GetAll().ToList();
            if (Listsorties.Count != 0)
                return View(Listsorties);
            else
                ViewBag.Message = "No trips exist";
            return View();

        }


        public ActionResult Index(int ExcursionID)
        {
            List<SortieParSemaine> Listsorties = new List<SortieParSemaine>();
            Listsorties = sortieParSemaineRepository.FindMany(x => x.ExcursionID == ExcursionID).ToList();
            if (Listsorties.Count != 0)
                return View(Listsorties);
            else
                ViewBag.Message = "No trips exist for this excursion";
            return View();

        }

    }
}
