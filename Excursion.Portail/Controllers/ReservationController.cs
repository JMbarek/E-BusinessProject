using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursion.Business.Repositories;
using Excursion.Data;
using Excursion.Portail.Utilities;
using System.Data.Entity.Validation;
using System.Text;
using Excursion.Portail.Models;
using System.Linq.Dynamic;
using System.Web.Routing;


namespace Excursion.Portail.Controllers
{
    public class ReservationController : Controller
    {
        private SortieParSemaineRepository sortieParSemaineRepository;
        private ReservationRepository reservationRepository;
        private HotelRepository hotelRepository;
        private LangueRepository langueRepository;
        private UserRepository userRepository;

        public ReservationController() 
        {
            this.sortieParSemaineRepository = new Excursion.Business.Repositories.SortieParSemaineRepository(new ExcursionContext());
            this.reservationRepository = new ReservationRepository(new ExcursionContext());
            this.hotelRepository = new HotelRepository( new ExcursionContext());
            this.langueRepository = new LangueRepository(new ExcursionContext());
            this.userRepository = new UserRepository(new ExcursionContext());
        }



        //
        // GET: /Reservation/

        public ActionResult Index( )
        {
            return View();

            #region Ancienblabla
            //List<Reservation> Listreserv = new List<Reservation>();
            //List<Excursion.Portail.Models.ReservationModel> Liste2 = new List<ReservationModel>();
            //if (IsClient == null)
            //{
            //    Listreserv = reservationRepository.GetAll().ToList();
            //}
            //else 
            //{
            //    Listreserv = reservationRepository.FindMany( x => x.UserID == SessionData.CurrentUser.UserID).ToList();
               
            //    foreach (Reservation Res in Listreserv)
            //    {
            //        ReservationModel res2 = new ReservationModel();
            //        res2.AddRow = null;
            //        res2.UpdateRow = null;
            //        res2.DeleteRow = null;
            //        res2.DateAnnulation = Res.DateAnnulation;
            //        res2.DateModification = Res.DateModification;
            //        res2.DatePayement = Res.DatePayement;
            //        res2.DateReserv = Res.DateReserv;
            //        res2.Etat = Res.Etat;

            //        res2.Hotel = Res.Hotel.Nom;
            //        res2.Langue = Res.Langue.NomLangue;
            //        res2.Sortie = Res.SortieParSemaine.Excursion.Nom;

            //        res2.NbreAdultes = Res.NbreAdultes;
            //            res2.NbreBebes = Res.NbreBebes;
            //            res2.NbreEnfants = Res.NbreEnfants;
            //            res2.NumChamb = Res.NumChamb;
            //            res2.NumLigneAs400 = Res.NumLigneAs400;
            //            res2.NumTicket = Res.NumTicket;
            //            res2.Observation = Res.Observation;
            //            res2.Paye =(bool)Res.Paye;
            //            res2.PointDepart = Res.PointDepart;
            //            res2.Reduction = Res.Reduction;
            //            res2.ReservationID = Res.ReservationID;
            //            res2.TypeClient = Res.TypeClient;
            //            res2.UserID = Res.UserID;
                   
            //        Liste2.Add(res2);
                    
            //    }
            //}

            //if( Listreserv.Count !=0 )
            //{
            //    return View( Liste2);
            //}
            //else
            //{
            //    ViewBag.Message = "There's no reservation";
            //    return View();
            //}
            #endregion

        }


        //
        // GET: /Reservation/Create

        public ActionResult Create( int SortieID )
        {
            //ViewBag.IDSortie = SortieID;
            #region SelectList
            var SysLst = new List<string>();
            var SysQry = from d in hotelRepository.FindMany( x =>x.Region.Zone.CentreID == SessionData.CurrentUser.CentreID )
                          select d.Nom;
            SysLst.AddRange(SysQry.Distinct());
            ViewBag.Hotel = new SelectList(SysLst);


            var SysLst1 = new List<string>();
            var SysQry1 = from b in langueRepository.GetAll()
                         select b.NomLangue;
            SysLst1.AddRange(SysQry1.Distinct());
            ViewBag.Lang = new SelectList(SysLst1);

            var SysLst2 = new List<string>();
            SysLst2.Add("Canceled");
            SysLst2.Add("No Modification No Cancellation");
            SysLst2.Add("Modified");
            ViewBag.Etatres = new SelectList(SysLst2);

            var SysLst3 = new List<int>();
            SysLst3.Add(SortieID);
            ViewBag.Sortie = new SelectList(SysLst3);
            #endregion
            return View();
        }


        [HttpPost]
        public ActionResult Create( int SortieID ,string HotelName, string LanguageName, string Etat, Reservation model)
        {
            //Reserv.Data.Excursion excr = ExcursionRepository
            try
            {
                Excursion.Data.Reservation reserv = new Reservation();

                reserv.SortieID = SortieID;
                reserv.DateReserv = DateTime.Now.Date;
                reserv.NumChamb = model.NumChamb;

                Excursion.Data.Hotel hotel = hotelRepository.FindOne(x => x.Nom == HotelName);
                reserv.HotelID = hotel.HotelID;

                if (SessionData.CurrentUser.TypeUser == "Client")
                    reserv.TypeClient = SessionData.CurrentUser.TypeUser;
                else if (SessionData.CurrentUser.TypeUser == "Client Indirect")
                    reserv.TypeClient = SessionData.CurrentUser.TypeCIndirect;

                reserv.UserID = SessionData.CurrentUser.UserID;

                if (LanguageName == "Eng") reserv.LangueID = 1;
                else if (LanguageName == "Fr") reserv.LangueID = 2;
                else if (LanguageName == "Deu") reserv.LangueID = 3;
                else if (LanguageName == "Ara") reserv.LangueID = 4;

                if (reserv.Etat == "No Modification No Cancellation") reserv.Etat = "N";
                else if (reserv.Etat == "Canceled") reserv.Etat = "C";
                else if (reserv.Etat == "Modified") reserv.Etat = "M";

                reserv.Observation = model.Observation;
                reserv.DateAnnulation = model.DateAnnulation;

                reserv.NumTicket = model.NumTicket;

                reserv.PointDepart = model.PointDepart;
                reserv.DateModification = model.DateModification;
                reserv.NbreAdultes = model.NbreAdultes;
                reserv.NbreBebes = model.NbreBebes;
                reserv.NbreEnfants = model.NbreEnfants;

                reserv.Reduction = model.Reduction;
                reserv.Paye = model.Paye;
                reserv.DatePayement = model.DatePayement;

                reservationRepository.Add(reserv);
                reservationRepository.Save();
                ViewBag.AjoutSuccess = "Reservation added successfully";
            }
            catch (DbEntityValidationException ex)
            {

            }

            #region SelectList
            var SysLst = new List<string>();
            var SysQry = from d in hotelRepository.FindMany(x => x.Region.Zone.CentreID == SessionData.CurrentUser.CentreID)
                         select d.Nom;
            SysLst.AddRange(SysQry.Distinct());
            ViewBag.Hotel = new SelectList(SysLst);


            var SysLst1 = new List<string>();
            var SysQry1 = from b in langueRepository.GetAll()
                          select b.NomLangue;
            SysLst1.AddRange(SysQry1.Distinct());
            ViewBag.Lang = new SelectList(SysLst1);

            var SysLst2 = new List<string>();
            SysLst2.Add("Canceled");
            SysLst2.Add("No Modification No Cancellation");
            SysLst2.Add("Modified");
            ViewBag.Etatres = new SelectList(SysLst2);

            var SysLst3 = new List<int>();
            SysLst3.Add(SortieID);
            ViewBag.Sortie = new SelectList(SysLst3);
            #endregion
            return View();
        }

#region Jqgrid

//        public ActionResult Update(Excursion.Portail.Models.ReservationViewModel viewModel, FormCollection formCollection)
//        {
//            var operation = formCollection["oper"];
//            var Id = formCollection["id"];
//            if (operation.Equals("add"))
//            {
//                Reservation reserv = new Reservation();
//                reserv.SortieID = 1;
//                reserv.DateReserv = DateTime.Now.Date;
//               // reserv.NumChamb = viewModel.;
//                reserv.HotelID = 1;
//                reserv.TypeClient = SessionData.CurrentUser.TypeUser;
//                reserv.UserID = SessionData.CurrentUser.UserID;
//                reserv.LangueID = 4;
//                reserv.Etat = "N";
//                reserv.NumTicket = viewModel.NumTicket;
//                reserv.PointDepart = viewModel.PointDepart;
//                reserv.NbreAdultes = viewModel.NbreAdultes;
//                reserv.NbreEnfants = viewModel.NbreEnfants;
//                reserv.NbreBebes = viewModel.NbreBebes;
//                reserv.Observation = viewModel.Observation;
//                reserv.NumChamb = viewModel.NumChamb.ToString();
//                reserv.Paye = viewModel.Paye;
//                reserv.DatePayement = viewModel.DatePayement;

//                reservationRepository.Add(reserv);
//                reservationRepository.Save();
//                //int ContactId = viewModel.ReservationID;                
//            }
//            else if (operation.Equals("edit"))
//            {
//                Reservation reserv = reservationRepository.FindOne(x => x.ReservationID == viewModel.ReservationID);
                
            
//                reserv.DateModification = DateTime.Now.Date;
//                // reserv.NumChamb = viewModel.;
//                reserv.HotelID = 1;
                
//                reserv.LangueID = 4;
//                reserv.Etat = "M";
                
//                reserv.LangueID = langueRepository.FindOne(x => x.NomLangue == viewModel.LangueID).LangueID;
//                reserv.NumTicket = viewModel.NumTicket;
//                reserv.PointDepart = viewModel.PointDepart;
//                reserv.NbreAdultes = viewModel.NbreAdultes;
//                reserv.NbreEnfants = viewModel.NbreEnfants;
//                reserv.NbreBebes = viewModel.NbreBebes;
//                reserv.Observation = viewModel.Observation;
//                reserv.NumChamb = viewModel.NumChamb.ToString();
//                reserv.Paye = viewModel.Paye;
//                reserv.DatePayement = viewModel.DatePayement;


//                reservationRepository.Update(reserv);
//                reservationRepository.Save();
//            }
//            else if (operation.Equals("del"))
//            {
//                Reservation reserv = reservationRepository.FindOne(x => x.ReservationID == viewModel.ReservationID);
//                reserv.Etat = "C";
//                reserv.DateAnnulation = DateTime.Now.Date; 
//                reservationRepository.Update(reserv);
//                reservationRepository.Save();
//            }

//            return Content("false");
//            //return RedirectToAction("JsonSalesCollection", new RouteValueDictionary(
//            //                      new { controller = "Home", action = "JsonSalesCollection" }));
//        }




//        public virtual ActionResult JsonSalesCollection(DateTime? startDate, DateTime? endDate, string typeclient,
//string sidx, string sord, int page, int rows)
//        {
//            SalesLogic logicLayer = new SalesLogic();
//            List<Reservation> context;

//            ////Filter using  client type
//            //if (typeclient != null)
//            //{
//            //    context = logicLayer.GetReservationsByFilterTypeC(typeclient);
//            //}

//            // If we aren't filtering by date, return this month's contributions
//            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
//            {
//                context = logicLayer.GetReservations();
//            }
//            else // Filter by specified date range
//            {
//                context = logicLayer.GetReservationsByDateRange((System.DateTime)startDate, (System.DateTime)endDate, typeclient);
//            }

//            // Calculate page index, total pages, etc. for jqGrid to us for paging
//            int pageIndex = Convert.ToInt32(page) - 1;
//            int pageSize = rows;
//            int totalRecords = context.Count();
//            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

//            // Order the results based on the order passed into the method
//            string orderBy = string.Format("{0} {1}", sidx, sord);
//            var Reservations = context.AsQueryable()
//                         .OrderBy(orderBy) // Uses System.Linq.Dynamic library for sorting
//                         .Skip(pageIndex * pageSize)
//                         .Take(pageSize);

//            // Format the data for the jqGrid
//            var jsonData = new
//            {
//                total = totalPages,
//                page = page,
//                records = totalRecords,
//                rows = (
//                       from s in Reservations
//                       select new
//                       {
//                           i = s.ReservationID,
//                           cell = new string[] {
//                   s.ReservationID.ToString(),
//                   s.PointDepart.ToString(),
//                   s.DateReserv.ToShortDateString(),
//                   s.NumTicket.ToString(),
//                   //s.SortieID.ToString(),
//                   //s.HotelID.ToString(),
//                   //s.LangueID.ToString(),
//                   s.SortieParSemaine.Excursion.Nom.ToString(),
//                   s.Hotel.Nom.ToString(),
//                   s.NumChamb.ToString(),
//                   s.Langue.NomLangue.ToString(),
//                   s.Observation.ToString(),
//                   s.NbreAdultes.ToString(),
//                   s.NbreEnfants.ToString(),
//                   s.NbreBebes.ToString(),
//                   s.Paye.ToString(),
//                   s.DatePayement.ToString()
                   
//                }
//                       }).ToArray()
//            };

//            // Return the result in json
//            return Json(jsonData, JsonRequestBehavior.AllowGet);
//        }


//        public JsonResult GetExcursionsList()
//        {

//            var allExcursions = new List<string>();
//            var SysQry1 = from b in sortieParSemaineRepository.GetAll()
//                          select b.Excursion.Nom;
//            allExcursions.AddRange(SysQry1.Distinct());

//            //allExcursions.Add("Excursion1");
//            //allExcursions.Add("Excursion2");
//            //allExcursions.Add("Excursion3");
//            //allExcursions.Add("Excursion4");
//            //allExcursions.Add("Excursion5");

//            return Json(allExcursions, JsonRequestBehavior.AllowGet);
//        }


//        public JsonResult GetHotelsList()
//        {
//            //List<string> allHotels = new List<string>();

//            var allHotels = new List<string>();
//            var SysQry = from d in hotelRepository.FindMany(x => x.RegionID == SessionData.CurrentUser.RegionID)
//                         select d.Nom;
//            allHotels.AddRange(SysQry.Distinct());


//            //allHotels.Add("Hotel1");
//            //allHotels.Add("Hotel2");
//            //allHotels.Add("Hotel3");
//            //allHotels.Add("Hotel4");
//            //allHotels.Add("Hotel5");

//            return Json(allHotels, JsonRequestBehavior.AllowGet);
//        }


//        public JsonResult GetLanguesList()
//        {
//            //List<string> allLangues = new List<string>();

//            var allLangues = new List<string>();
//            var SysQry1 = from b in langueRepository.GetAll()
//                          select b.NomLangue;
//            allLangues.AddRange(SysQry1.Distinct());


//            //allLangues.Add("Deutsh");
//            //allLangues.Add("English");
//            //allLangues.Add("Français");
//            //allLangues.Add("Arab");
//            //allLangues.Add("Italien");

//            return Json(allLangues, JsonRequestBehavior.AllowGet);
//        }

//        public JsonResult GetEtatsList()
//        {
//            List<string> allEtats = new List<string>();

//            allEtats.Add("No Modif No Cancel");
//            allEtats.Add("Cancel");
//            allEtats.Add("Modif");

//            return Json(allEtats, JsonRequestBehavior.AllowGet);
//        }

#endregion



#region IgGrid


        public virtual ActionResult GetAllRespExc()
        {
            List<Excursion.Portail.Models.ReservationModel> Liste2 = new List<ReservationModel>();
            var reservations = reservationRepository.GetAll().ToList();

            foreach (Reservation Res in reservations)
            {
                ReservationModel res2 = new ReservationModel();
                res2.AddRow = null;
                res2.UpdateRow = null;
                res2.DeleteRow = null;
                res2.DateAnnulation = Res.DateAnnulation;
                res2.DateModification = Res.DateModification;
                res2.DatePayement = Res.DatePayement;
                res2.DateReserv = (DateTime)Res.DateReserv;
                res2.Etat = Res.Etat;


                res2.Hotel = Res.Hotel.Nom;
                res2.Langue = Res.Langue.NomLangue;
                if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_fr;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_de;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_en;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_it;
                }
                

                res2.NbreAdultes = Res.NbreAdultes;
                res2.NbreBebes = Res.NbreBebes;
                res2.NbreEnfants = Res.NbreEnfants;
                res2.NumChamb = Res.NumChamb;
                res2.NumLigneAs400 = Res.NumLigneAs400;
                res2.NumTicket = Res.NumTicket;
                res2.Observation = Res.Observation;
                res2.Paye = (bool)Res.Paye;
                res2.PointDepart = Res.PointDepart;
                res2.Reduction = Res.Reduction;
                res2.ReservationID = Res.ReservationID;
                res2.TypeClient = Res.TypeClient;
                res2.UserID = Res.UserID;

                User us = userRepository.FindOne(x => x.UserID == Res.UserID);
                if (us.TypeUser == "Client")
                { 
                    res2.ClientD = us.Nom.ToString() + " " + us.Prenom.ToString(); 
                }
                else
                {
                    if (us.TypeCIndirect == "Guide")
                    {
                        res2.Guide = us.Nom.ToString() + " " + us.Prenom.ToString();
                    }
                    else if (us.TypeCIndirect == "Fournisseur")
                    {
                        res2.Fournisseur = us.Nom.ToString() + " " + us.Prenom.ToString();
                    }
                }

                Liste2.Add(res2);

            }
            return Json(Liste2, JsonRequestBehavior.AllowGet);
        }


        
        public virtual ActionResult GetAll()
        {
            List<Excursion.Portail.Models.ReservationModel> Liste2 = new List<ReservationModel>();
            var reservations = reservationRepository.GetAll().ToList();

            foreach (Reservation Res in reservations)
            {
                ReservationModel res2 = new ReservationModel();
                res2.AddRow = null;
                res2.UpdateRow = null;
                res2.DeleteRow = null;
                res2.DateAnnulation = Res.DateAnnulation;
                res2.DateModification = Res.DateModification;
                res2.DatePayement = Res.DatePayement;
                res2.DateReserv = (DateTime)Res.DateReserv;
                res2.Etat = Res.Etat;


                res2.Hotel = Res.Hotel.Nom;
                res2.Langue = Res.Langue.NomLangue;
                if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_fr;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_de;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_en;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
                {
                    res2.Sortie = Res.SortieParSemaine.Excursion.Nom_it;
                }
                //res2.Sortie = Res.SortieParSemaine.Excursion.Nom;


                res2.NbreAdultes = Res.NbreAdultes;
                res2.NbreBebes = Res.NbreBebes;
                res2.NbreEnfants = Res.NbreEnfants;
                res2.NumChamb = Res.NumChamb;
                res2.NumLigneAs400 = Res.NumLigneAs400;
                res2.NumTicket = Res.NumTicket;
                res2.Observation = Res.Observation;
                if (Res.Paye != null) { res2.Paye = (bool)Res.Paye; }
                res2.PointDepart = Res.PointDepart;
                res2.Reduction = Res.Reduction;
                res2.ReservationID = Res.ReservationID;
                res2.TypeClient = Res.TypeClient;
                res2.UserID = Res.UserID;
                res2.SortieID = Res.SortieID;
                Liste2.Add(res2);

            }
            return Json(Liste2, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateReservation2( int ReservationID )
        {
            Excursion.Data.Reservation Res = reservationRepository.GetById(ReservationID);
            Excursion.Portail.Models.ReservationModel2 res2 = new ReservationModel2();

            res2.AddRow = null;
            res2.UpdateRow = null;
            res2.DeleteRow = null;
            res2.DateAnnulation = Res.DateAnnulation;
            res2.DateModification = Res.DateModification;
            res2.DatePayement = Res.DatePayement;
            res2.DateReserv = Res.DateReserv;
            res2.Etat = Res.Etat;


            res2.Hotel = Res.Hotel.Nom;
            res2.Langue = Res.Langue.NomLangue;
            if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
            {
                res2.Sortie = Res.SortieParSemaine.Excursion.Nom_fr;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
            {
                res2.Sortie = Res.SortieParSemaine.Excursion.Nom_de;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
            {
                res2.Sortie = Res.SortieParSemaine.Excursion.Nom_en;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
            {
                res2.Sortie = Res.SortieParSemaine.Excursion.Nom_it;
            }
            //res2.Sortie = Res.SortieParSemaine.Excursion.Nom;


            res2.NbreAdultes = Res.NbreAdultes;
            res2.NbreBebes = Res.NbreBebes;
            res2.NbreEnfants = Res.NbreEnfants;
            res2.NumChamb = Res.NumChamb;
            res2.NumLigneAs400 = Res.NumLigneAs400;
            res2.NumTicket = Res.NumTicket;
            res2.Observation = Res.Observation;

            if (Res.Paye != null) res2.Paye = (bool)Res.Paye;
            res2.PointDepart = Res.PointDepart;
            res2.Reduction = Res.Reduction;
            res2.ReservationID = Res.ReservationID;            
            res2.TypeClient = Res.TypeClient;
            res2.UserID = Res.UserID;

            var allExcursions = new List<string>();
            var SysQry1 = sortieParSemaineRepository.FindMany(x => x.Excursion.CentreID == SessionData.CurrentUser.CentreID).ToList();
            foreach (Excursion.Data.SortieParSemaine srt in SysQry1)
            {
                if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
                {
                    string sortie = srt.Excursion.Nom_fr + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie);
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
                {
                    string sortie = srt.Excursion.Nom_de + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie);
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
                {
                    string sortie = srt.Excursion.Nom_en + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie);
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
                {
                    string sortie = srt.Excursion.Nom_it + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie);
                }

                
            }
            ViewBag.ExcursionsList = new SelectList(allExcursions);

            var allHotels = new List<string>();
            var SysQry = from d in hotelRepository.FindMany(x => x.Region.Zone.CentreID == SessionData.CurrentUser.CentreID)
                         select d.Nom;
            allHotels.AddRange(SysQry.Distinct());
            ViewBag.HotelsList = new SelectList(allHotels);

            var allLangues = new List<string>();
            var SysQry2 = from b in langueRepository.GetAll()
                          select b.NomLangue;
            allLangues.AddRange(SysQry2.Distinct());
            ViewBag.LanguesList = new SelectList(allLangues);

            //return View("_EditReservation", res2);
            return View(res2);
        }

        [HttpPost]
        public ActionResult UpdateReservation2( Excursion.Portail.Models.ReservationModel2 reservation )
        {
            //if (ModelState.IsValid)
            //{
                Reservation reserv = reservationRepository.FindOne(x => x.ReservationID == reservation.ReservationID);

                // recherche SortieID
                string[] tab = reservation.Sortie.Split("-".ToCharArray());
                float hdp = Convert.ToSingle(tab[3]);
                string nom = tab[0].ToString();
                string typeexc = tab[1].ToString();
                DateTime datesortie = Convert.ToDateTime(tab[5]);

                if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
                {
                    SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_fr == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                    reserv.SortieID = sortie.SortieID;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
                {
                    SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_de == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                    reserv.SortieID = sortie.SortieID;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
                {
                    SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_en == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                    reserv.SortieID = sortie.SortieID;
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
                {
                    SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_it == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                    reserv.SortieID = sortie.SortieID;
                }
                

                reserv.DateModification = DateTime.Now.Date;
                reserv.HotelID = hotelRepository.FindOne(x => x.Nom == reservation.Hotel && x.Region.Zone.CentreID == SessionData.CurrentUser.CentreID).HotelID;
                
                reserv.Etat = "M";

                reserv.LangueID = langueRepository.FindOne(x => x.NomLangue == reservation.Langue).LangueID;
                reserv.NumTicket = reservation.NumTicket;
                reserv.PointDepart = reservation.PointDepart;
                reserv.NbreAdultes = reservation.NbreAdultes;
                reserv.NbreEnfants = reservation.NbreEnfants;
                reserv.NbreBebes = reservation.NbreBebes;
                reserv.Observation = reservation.Observation;
                reserv.NumChamb = reservation.NumChamb.ToString();
                if ((bool)reservation.Paye)
                {
                    reserv.Paye = reservation.Paye;
                    reserv.DatePayement = DateTime.Now.Date;
                }
                reservationRepository.Update(reserv);
                reservationRepository.Save();
            //}
            return RedirectToAction("Index");
        }



        public ActionResult AddReservation2()
        {
            var allExcursions = new List<string>();
            var SysQry1 = sortieParSemaineRepository.FindMany(x => x.Excursion.CentreID == SessionData.CurrentUser.CentreID).ToList();
            foreach (Excursion.Data.SortieParSemaine srt in SysQry1)
            {
                if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
                {
                    string sortie = srt.Excursion.Nom_fr + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie);
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
                {
                    string sortie = srt.Excursion.Nom_en + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie);
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
                {
                    string sortie = srt.Excursion.Nom_de + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie);
                }
                else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
                {
                    string sortie = srt.Excursion.Nom_it + "-" + srt.TypeExc.Type + "- Départ à: -" + srt.HeureDepart + "- Le: -" + srt.Jour.Date.ToString().Substring(0, 10);
                    allExcursions.Add(sortie); 
                }
                
            }
            ViewBag.ExcursionsList = new SelectList(allExcursions);

            var allHotels = new List<string>();
            var SysQry = from d in hotelRepository.FindMany(x => x.Region.Zone.CentreID == SessionData.CurrentUser.CentreID)
                         select d.Nom;
            allHotels.AddRange(SysQry.Distinct());
            ViewBag.HotelsList = new SelectList(allHotels);

            var allLangues = new List<string>();
            var SysQry2 = from b in langueRepository.GetAll()
                          select b.NomLangue;
            allLangues.AddRange(SysQry2.Distinct());
            ViewBag.LanguesList = new SelectList(allLangues);

            ViewBag.IsUpdate = false;
            //return PartialView("_ManageReservationPartial");
            //return View("_AddReservation");
            return View();
        }


        [HttpPost]
        public ActionResult AddReservation2(Excursion.Portail.Models.ReservationModel2 reservation, string ExcursionName, string LangueName, string HotelName)
        {
            //if (ModelState.IsValid)
            //{

            Reservation reserv = new Reservation();

            // recherche SortieID
            string[] tab = ExcursionName.Split("-".ToCharArray());
            float hdp = Convert.ToSingle(tab[3]);
            string nom = tab[0].ToString();
            string typeexc = tab[1].ToString();
            DateTime datesortie = Convert.ToDateTime(tab[5]);
            if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "fr")
            {
                SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_fr == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                reserv.SortieID = sortie.SortieID;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "en")
            {
                SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_en == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                reserv.SortieID = sortie.SortieID;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "de")
            {
                SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_de == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                reserv.SortieID = sortie.SortieID;
            }
            else if (MvcGlobalisationSupport.CultureManager.GetLanguage() == "it")
            {
                SortieParSemaine sortie = sortieParSemaineRepository.FindOne(x => x.HeureDepart == hdp && x.Excursion.Nom_it == nom && x.Jour.Date == datesortie && x.TypeExc.Type == typeexc && x.Excursion.CentreID == SessionData.CurrentUser.CentreID);
                reserv.SortieID = sortie.SortieID;
            }


            reserv.DateReserv = DateTime.Now.Date;
            reserv.HotelID = hotelRepository.FindOne(x => x.Nom == HotelName && x.Region.Zone.CentreID == SessionData.CurrentUser.CentreID).HotelID;
            reserv.TypeClient = SessionData.CurrentUser.TypeUser;
            reserv.UserID = SessionData.CurrentUser.UserID;
            reserv.LangueID = langueRepository.FindOne(x => x.NomLangue == LangueName).LangueID;
            //
            reserv.Etat = "N";
            reserv.NumTicket = reservation.NumTicket;
            reserv.PointDepart = reservation.PointDepart;
            reserv.NbreAdultes = reservation.NbreAdultes;
            reserv.NbreEnfants = reservation.NbreEnfants;
            reserv.NbreBebes = reservation.NbreBebes;
            reserv.Observation = reservation.Observation;
            reserv.NumChamb = reservation.NumChamb.ToString();
            if ((bool)reservation.Paye)
            {
                reserv.Paye = reservation.Paye;
                reserv.DatePayement = DateTime.Now.Date;
            }

            reservationRepository.Add(reserv);
            reservationRepository.Save();

            //}
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Annuler(int ReservationID)
        {
            Reservation reserv = reservationRepository.FindOne(x => x.ReservationID == ReservationID);
            reserv.Etat = "C";
            reserv.DateAnnulation = DateTime.Now.Date;
            reservationRepository.Update(reserv);
            reservationRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Paye(int ReservationID)
        {
            Reservation reserv = reservationRepository.FindOne(x => x.ReservationID == ReservationID);
            reserv.Paye = true;
            reserv.DatePayement = DateTime.Now.Date;
            reservationRepository.Update(reserv);
            reservationRepository.Save();
            return RedirectToAction("Index");
        }



#endregion








    }
}
