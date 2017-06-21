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
    public class ExcursionController : Controller
    {

        private ExcursionRepository excursionRepository;
        private UserRepository userRepository;
        private PeriodeRepository periodeRepository;
        private PrixRepository prixRepository;
        private ExcursionThemeRepository excursionThemeRepository;
        private ThemeRepository themeRepository;
        private ZoneRepository zoneRepository;
        public ExcursionController()
        {
            this.zoneRepository = new ZoneRepository(new ExcursionContext());
            this.excursionRepository = new ExcursionRepository(new ExcursionContext());
            this.userRepository = new UserRepository(new ExcursionContext());
            this.periodeRepository = new PeriodeRepository(new ExcursionContext());
            this.prixRepository = new PrixRepository(new ExcursionContext());
            this.excursionThemeRepository = new ExcursionThemeRepository(new ExcursionContext());
            this.themeRepository = new ThemeRepository(new ExcursionContext());
        }


        //[HttpGet]
        public ActionResult Ajouter()
        {
            #region dropdownlist
            var SysLst = new List<string>();
            var SysQry = periodeRepository.GetAll().ToList();
            foreach (Periode p in SysQry)
            {
                string pr = "from " + p.DateDebut.ToString().Substring(0, 10) + "  to " + p.DateFin.Date.ToString().Substring(0, 10);
                SysLst.Add(pr);
            }
            ViewBag.PeriodeList = new SelectList(SysLst);

            var SysLst3 = new List<string>();
            SysLst3.Add("2 jours"); SysLst3.Add("1 jour"); SysLst3.Add("< 1 jour");
            ViewBag.DureeList = new SelectList(SysLst3);

            var SysLst4 = new List<int>();
            for (int i = 1; i < 24; i++)
            {
                SysLst4.Add(i);
            }
            ViewBag.HeureList = new SelectList(SysLst4);

            var SysLst5 = new List<int>();
            for (int i = 0; i < 60; i += 5)
            {
                SysLst5.Add(i);
            }
            ViewBag.MinuteList = new SelectList(SysLst5);


            var SysLst6 = new List<string>();
            if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
            {
                var SysQry6 = from b in themeRepository.GetAll()
                             select b.Nom_fr;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
            {
                var SysQry6 = from b in themeRepository.GetAll() select b.Nom_en;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
            {
                var SysQry6 = from b in themeRepository.GetAll() select b.Nom_de;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
            {
                var SysQry6 = from b in themeRepository.GetAll() select b.Nom_it;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            ViewBag.ThemeList = new SelectList(SysLst6);


            var SysLst1 = new List<float>();
            var SysQry1 = from b in prixRepository.GetAll()
                          select b.PrixAdulte;
            SysLst1.AddRange(SysQry1.Distinct());
            ViewBag.PrixList = new SelectList(SysLst1);

            var SysLst2 = new List<string>();
            SysLst2.Add("Valide");
            SysLst2.Add("Non Valide");
            ViewBag.EtatList = new SelectList(SysLst2);

            var SysLst7 = new List<string>();
            var SysQry7 = from b in zoneRepository.FindMany(x => x.CentreID == SessionData.CurrentUser.CentreID)
                          select b.Nom;
            SysLst7.AddRange(SysQry7.Distinct());
            ViewBag.ZoneList = new SelectList(SysLst7);
            #endregion

            return View();
        }

        // ajouter excursion
        [HttpPost]
        public ActionResult Ajouter(Excursion.Data.Excursion excur, string Periode, string Etat, float Prix, int? Heures, int? Minutes, string Duree, List<string> Themes)
        //string Affaires, string AllInclusive, string Archeologie, string Balneaire, string Combinees, string Gratuite,
        //string Decouverte, string Desert, string Early, string Booking, string Ecologique, string fetedesmeres, string Golf,
        //string Enfant, string Meilleure, string Note, string Montagne, string Petit, string Budget, string Promo, string Randonnee,
        //string Famille, string Reveillon, string Saharien, string Special, string AidElKebir, string Thalassotherapie,
        //string Thermalisme, string Voyagesdenoces, string Weekend
        {

            Excursion.Data.Excursion exc = new Data.Excursion();
            exc.CodeExcursion = excur.CodeExcursion;
            exc.Description_fr = excur.Description_fr;
            exc.Description_en = excur.Description_en;
            exc.Description_de = excur.Description_de;
            exc.Description_it = excur.Description_it;
            //exc.Duree = excur.Duree;
            exc.Etat = excur.Etat;
            exc.Nom_de = excur.Nom_de;
            exc.Nom_en = excur.Nom_en;
            exc.Nom_fr = excur.Nom_fr;
            exc.Nom_it = excur.Nom_it;
 
            exc.Etat = Etat;
            exc.EstLimite = excur.EstLimite;
            //
            DateTime datedebut = Convert.ToDateTime(Periode.Substring(5, 10));
            DateTime datefin = Convert.ToDateTime(Periode.Substring(19, 10));

            Periode prd = periodeRepository.FindOne(x => x.DateDebut == datedebut && x.DateFin == datefin);
            exc.PeriodeID = prd.PeriodeID; // excur.PeriodeID;

            Excursion.Data.Prix pri = prixRepository.FindOne(x => x.PrixAdulte == Prix);
            // ???????????????????????????
            exc.PrixID = pri.PrixID; // excur.PrixID;

            exc.CentreID = (int)SessionData.CurrentUser.CentreID;//excur.RegionID;
            exc.Duree = Duree;
            exc.DureeHeure = Heures.ToString() + Minutes.ToString();
            //
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                byte[] imageSize = new byte[file.ContentLength];
                file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
                //
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/Img/Excursion"), System.IO.Path.GetFileName(file.FileName));
                file.SaveAs(path);
                //
                exc.Photo = file.FileName.Split('\\').Last();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("uploadError", e);
            }
            //           
            excursionRepository.Add(exc);
            excursionRepository.Save();

            foreach (string theme in Themes)
            {
                ExcursionTheme exth = new ExcursionTheme();
                exth.ExcursionID = exc.ExcursionID;
                if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
                {
                    exth.ThemeID = themeRepository.FindOne(x => x.Nom_fr == theme).ThemeID;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
                {
                    exth.ThemeID = themeRepository.FindOne(x => x.Nom_de == theme).ThemeID;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
                {
                    exth.ThemeID = themeRepository.FindOne(x => x.Nom_en == theme).ThemeID;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
                {
                    exth.ThemeID = themeRepository.FindOne(x => x.Nom_it == theme).ThemeID;
                } 
                excursionThemeRepository.Add(exth);
                excursionThemeRepository.Save();
            }

            #region Themes
            //if (Convert.ToBoolean(Affaires))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Affaires").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(AllInclusive))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "AllInclusive").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Archeologie))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Archeologie").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Balneaire))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Balneaire").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Combinees))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Combinees").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Gratuite))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Gratuite").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Decouverte))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Decouverte").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Desert))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Desert").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Early))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Early").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Booking))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Booking").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Ecologique))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Ecologique").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(fetedesmeres))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "fetedesmeres").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Golf))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Golf").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Enfant))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Enfant").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Meilleure))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Meilleure").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Note))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Note").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Montagne))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Montagne").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Petit))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Petit").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Budget))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Budget").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Promo))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Promo").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Randonnee))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Randonnee").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Famille))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Famille").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Reveillon))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Reveillon").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Saharien))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Saharien").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Special))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Special").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(AidElKebir))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "AidElKebir").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Thalassotherapie))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Thalassotherapie").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Thermalisme))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Thermalisme").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Voyagesdenoces))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Voyagesdenoces").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            //if (Convert.ToBoolean(Weekend))
            //{
            //    ExcursionTheme exth = new ExcursionTheme();
            //    exth.ExcursionID = exc.ExcursionID;
            //    exth.ExcursionThemeID = themeRepository.FindOne(x => x.Nom == "Weekend").ThemeID;
            //    excursionThemeRepository.Add(exth);
            //    excursionThemeRepository.Save();
            //}
            #endregion

            #region dropdownlist
            var SysLst = new List<string>();
            var SysQry = periodeRepository.GetAll().ToList();
            foreach (Periode p in SysQry)
            {
                string pr = "from " + p.DateDebut.ToString().Substring(0, 10) + "  to " + p.DateFin.Date.ToString().Substring(0, 10);
                SysLst.Add(pr);
            }
            ViewBag.PeriodeList = new SelectList(SysLst);

            var SysLst1 = new List<float>();
            var SysQry1 = from b in prixRepository.GetAll()
                          select b.PrixAdulte;
            // ???????????????????????????
            SysLst1.AddRange(SysQry1.Distinct());
            ViewBag.PrixList = new SelectList(SysLst1);

            var SysLst2 = new List<string>();
            SysLst2.Add("Valide");
            SysLst2.Add("Non Valide");
            ViewBag.EtatList = new SelectList(SysLst2);

            var SysLst3 = new List<string>();
            SysLst3.Add("2 jours"); SysLst3.Add("1 jour"); SysLst3.Add("< 1 jour");
            ViewBag.DureeList = new SelectList(SysLst3);


            var SysLst4 = new List<int>();
            for (int i = 1; i <= 24; i++)
            {
                SysLst4.Add(i);
            }
            ViewBag.HeureList = new SelectList(SysLst4);

            var SysLst5 = new List<int>();
            for (int i = 0; i <= 60; i += 5)
            {
                SysLst5.Add(i);
            }
            ViewBag.MinuteList = new SelectList(SysLst5);

            var SysLst6 = new List<string>();
            if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
            {
                var SysQry6 = from b in themeRepository.GetAll()
                              select b.Nom_fr;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
            {
                var SysQry6 = from b in themeRepository.GetAll() select b.Nom_en;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
            {
                var SysQry6 = from b in themeRepository.GetAll() select b.Nom_de;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
            {
                var SysQry6 = from b in themeRepository.GetAll() select b.Nom_it;
                SysLst6.AddRange(SysQry6.Distinct());
            }
            ViewBag.ThemeList = new SelectList(SysLst6);

            var SysLst7 = new List<string>();
            var SysQry7 = from b in zoneRepository.FindMany(x => x.CentreID == SessionData.CurrentUser.CentreID)
                          select b.Nom;
            SysLst7.AddRange(SysQry7.Distinct());
            ViewBag.ZoneList = new SelectList(SysLst7);

            #endregion

            return View();
        }



        //
        // GET: /Excursion/
        int centreID;
        public ActionResult Index(int UserID)
        {
            Excursion.Data.User user = userRepository.FindOne(x => x.UserID == UserID);

            if (user.TypeUser == "Responsable")
            {

                if (user.RoleResp == "RespExcSSE")
                {
                    centreID = 1;
                }
                else if (user.RoleResp == "RespExcHMT")
                {
                    centreID = 2;
                }
                else if (user.RoleResp == "RespExcDJE")
                {
                    centreID = 3;
                }
            }
            else if (user.TypeUser == "Client Indirect" && user.CentreID != null)
            {
                centreID = (int)user.CentreID;
            }

            List<Excursion.Data.Excursion> ListExc = excursionRepository.FindMany(x => x.CentreID == centreID).ToList();

            if (ListExc.Count != 0)
            {
                return View(ListExc);
            }
            else
            {
                ViewBag.Message = "There's no outings";
                return View();
            }
        }

    }
}
