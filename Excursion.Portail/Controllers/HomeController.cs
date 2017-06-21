using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursion.Portail.Models;
using System.Linq.Dynamic;
using Excursion.Data;
using Excursion.Business.Repositories;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;



namespace Excursion.Portail.Controllers
{
    public class HomeController : Controller
    {

        private ReservationRepository reservationRepository;
        private ThemeRepository themeRepository;
        private SortieParSemaineRepository sortieParSemaineRepository;
        private JourRepository jourRepository;
        private TypeExcRepository typeExcRepository;

        private HttpClient httpClient;
        //private const string requestUri = "http://localhost:27017/api/excursions/v1/themes";
        //private const string requestUri = "http://localhost:27017/api/excursions/v1/departureCountries";
        private const string BASE_URI = "http://localhost:27017/api/excursions/";



        public HomeController()
        {
            this.httpClient = new HttpClient();
            this.reservationRepository = new ReservationRepository(new ExcursionContext());
            this.themeRepository = new ThemeRepository(new ExcursionContext());
            this.sortieParSemaineRepository = new SortieParSemaineRepository(new ExcursionContext());
            this.jourRepository = new JourRepository(new ExcursionContext());
            this.typeExcRepository = new TypeExcRepository(new ExcursionContext());
        }

        public async Task<string[]> GetThemesAsync()
        {
            var responseString = await httpClient.GetStringAsync(BASE_URI + "v1/themes");
            return JsonConvert.DeserializeObject<Themes>(responseString).themes;
        }

        public async Task<string[]> GetDepartureCountriesAsync()
        {
            var responseString = await httpClient.GetStringAsync(BASE_URI + "v1/departureCountries");
            return JsonConvert.DeserializeObject<DepartureCountries>(responseString).departureCountries;
        }

        public async Task<string[]> GetRegionsAsync()
        {
            var responseString = await httpClient.GetStringAsync(BASE_URI + "v1/regions");
            return JsonConvert.DeserializeObject<Regions>(responseString).regions;
        }

        public async Task<string[]> GetDestinationsInRegionAsync(string region)
        {
            var responseString = await httpClient.GetStringAsync(BASE_URI + "v1/destinations?region=" + region);
            return JsonConvert.DeserializeObject<DestinationsInRegion>(responseString).destinationsInRegion;
        }

        public async Task<ActionResult> Index()
        {
            var SysLstThemes = new List<string>();
            string[] listThemes = await GetThemesAsync();
            SysLstThemes.AddRange(listThemes.ToList());
            ViewBag.ThemesList = new SelectList(SysLstThemes);

            var SysLstDepCountries = new List<string>();
            string[] listDepartureCountries = await GetDepartureCountriesAsync();
            SysLstDepCountries.AddRange(listDepartureCountries.ToList());
            ViewBag.DepartureCountriesList = new SelectList(SysLstDepCountries);

            var SysLstRegions = new List<string>();
            string[] listRegions = await GetRegionsAsync();
            SysLstRegions.AddRange(listRegions.ToList());
            ViewBag.RegionsList = new SelectList(SysLstRegions);

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Search2(string jsonString)
        {
            //List<string> sortiesList = new List<string>();           
            //sortiesList.Add("ppppp");

            List<SortieParSemaine> sortiesList = new List<SortieParSemaine>();
            sortiesList = sortieParSemaineRepository.GetAll().ToList();


            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //json_serializer.DeserializeObject(jsonString);
            //Dictionary<string, SearchExcursionModel2> routes_list = (Dictionary<string, SearchExcursionModel2>)json_serializer.DeserializeObject(jsonString);
            //SearchExcursionModel2 routes_list = (SearchExcursionModel2)json_serializer.DeserializeObject("{ \"test\":\"some data\" }");


            return Json(new { SortiesList = sortiesList });
            //return RedirectToAction("Index", new RouteValueDictionary(
            //                     new { controller = "Home", action = "Index" }));

        }

        public ActionResult Reserver(int id)
        {
            ViewBag.SortieID = id;
            return View();
        }

        [HttpPost]
        public ActionResult Reserver(Excursion.Portail.Models.ReserverModel model)
        {
            int sortieId = model.SortieID;
            int Nadultes = model.NbrAdultes;
            int Nenfants = model.NbrEnfants;
            int Nbebes = model.NbrBebes;
            // href=\"ShoppingCart/AddToCart?id=" + item.SortieID + "&nbrAdultes=2&nbrEnfants=1&nbrBebes=1\"
            return RedirectToAction("AddToCart", new RouteValueDictionary(new { controller = "ShoppingCart", action = "AddToCart", id = sortieId, nbrAdultes = Nadultes, nbrEnfants = Nenfants, nbrBebes = Nbebes }));
        }

        [HttpPost]
        public async Task<ActionResult> GetDestinationsInRegion(string region)
        {
            var SysLstDestinationsInRegion = new List<string>();
            string[] listDestinationsInRegion = await GetDestinationsInRegionAsync(region);
            //Returning the list using json.
            return Json(new { VilleList = listDestinationsInRegion.ToList() });
        }

        [HttpPost]
        public ActionResult GetDestinationByRegion(string selectedValue)
        {
            List<string> villeList = new List<string>();
            if (selectedValue == "Cap Bon")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Béni Khiar"); villeList.Add("Dougga"); villeList.Add("El Haouaria");
            }
            else if (selectedValue == "Centre")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Sfax");
                villeList.Add("Zaghouane");
            }
            else if (selectedValue == "Djerba & Zarzis")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Djerba");
                villeList.Add("Gabès");
                villeList.Add("Matmata");
                villeList.Add("Zarzis");
            }
            else if (selectedValue == "Djerid")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Ben Gardane");
                villeList.Add("Douz");
                villeList.Add("Gafsa");
            }
            else if (selectedValue == "Sahel")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Kairouan");
            }
            else if (selectedValue == "Tabarka")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Aîn Draham");
                villeList.Add("Béja");
            }
            else if (selectedValue == "Tunis et côtes de Carthage")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Borj Cedria");
            }
            else if (selectedValue == "Zaghouane")
            {
                villeList.Add("Toutes les villes");
            }
            else if (selectedValue == "Toutes Les Régions")
            {
                villeList.Add("Toutes les villes");
                villeList.Add("Béni Khiar"); villeList.Add("Dougga"); villeList.Add("El Haouaria");
                villeList.Add("Ezzahra");
            }
            //Returning the list using jSon.
            return Json(new { VilleList = villeList });
        }


        #region jqGrid

        //        public ActionResult Update(Excursion.Portail.Models.ReservationViewModel viewModel, FormCollection formCollection)
        //        {
        //            var operation = formCollection["oper"];
        //            var Id = formCollection["id"];
        //            if (operation.Equals("add"))
        //            {
        //                Reservation reserv = new Reservation();
        //                reserv.SortieID = 1;
        //                reserv.DateReserv = DateTime.Now.Date;
        //                reserv.NumChamb = "456";

        //                reserv.HotelID = 1;
        //                    reserv.TypeClient = "Cttsesett";

        //                reserv.UserID = 1;

        //                reserv.LangueID = 4;
        //                reserv.Etat = "M";
        //                reserv.NumTicket = viewModel.NumTicket;

        //                reserv.PointDepart = "jkbue";

        //                reservationRepository.Add(reserv);
        //                reservationRepository.Save();
        //                //int ContactId = viewModel.ReservationID;                
        //            }
        //            else if(operation.Equals("edit"))
        //            {
        //              Reservation res = reservationRepository.FindOne(x => x.ReservationID == viewModel.ReservationID);
        //              res.PointDepart = "EditPointdepart";

        //              reservationRepository.Update(res);
        //              reservationRepository.Save();
        //            }
        //            else if (operation.Equals("del"))
        //            {
        //                Reservation res = reservationRepository.FindOne(x => x.ReservationID == viewModel.ReservationID);
        //                reservationRepository.Delete(res);
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
        //                   s.UserID.ToString(),
        //                   s.TypeClient,
        //                   s.PointDepart,
        //                   s.DateReserv.ToShortDateString(), 
        //                   s.NumTicket.ToString(),
        //                   s.SortieID.ToString()
        //                }
        //                       }).ToArray()
        //            };

        //            // Return the result in json
        //            return Json(jsonData, JsonRequestBehavior.AllowGet);
        //        }


        //        public JsonResult GetExcursionsList()
        //        {
        //            List<string> allExcursions =  new List<string>();

        //            allExcursions.Add("Excursion1");
        //            allExcursions.Add("Excursion2");
        //            allExcursions.Add("Excursion3");
        //            allExcursions.Add("Excursion4");
        //            allExcursions.Add("Excursion5");

        //            return Json(allExcursions, JsonRequestBehavior.AllowGet);
        //        }


        //        public JsonResult GetHotelsList()
        //        {
        //            List<string> allHotels = new List<string>();

        //            allHotels.Add("Hotel1");
        //            allHotels.Add("Hotel2");
        //            allHotels.Add("Hotel3");
        //            allHotels.Add("Hotel4");
        //            allHotels.Add("Hotel5");

        //            return Json(allHotels, JsonRequestBehavior.AllowGet);
        //        }


        //        public JsonResult GetLanguesList()
        //        {
        //            List<string> allLangues = new List<string>();

        //            allLangues.Add("Deutsh");
        //            allLangues.Add("English");
        //            allLangues.Add("Français");
        //            allLangues.Add("Arab");
        //            allLangues.Add("Italien");

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
    }
}
