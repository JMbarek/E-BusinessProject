using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursion.Business.Repositories;
using Excursion.Data;
using Excursion.Portail.Models;
using Excursion.Portail.ViewModels;
using System.Web.Routing;
using Excursion.Portail.Utilities;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace Excursion.Portail.Controllers
{
    public class ShoppingCartController : Controller
    {

        private CartRepository cartRepository;
        private OrderRepository orderRepository;
        private OrderDetailRepository orderDetailRepository;
        //private SortieParSemaineRepository sortieParSemaineRepository;
        public static int nbreTickets = 0;

        private HttpClient httpClient;
        private const string requestUri = "http://localhost:27017/api/excursions/";


        public ShoppingCartController()
        {
            this.httpClient = new HttpClient();
            this.cartRepository = new CartRepository(new ExcursionContext());
            this.orderRepository = new OrderRepository(new ExcursionContext());
            this.orderDetailRepository = new OrderDetailRepository(new ExcursionContext());
            //this.sortieParSemaineRepository = new SortieParSemaineRepository(new ExcursionContext());
        }


        public async Task<Excursionn> GetExcursionByIdAsync(string id)
        {
            var responseString = await httpClient.GetStringAsync(requestUri + id);
            return JsonConvert.DeserializeObject<ExcursionPL>(responseString).excursion;
        }


        public Excursionn GetExcursionById(string id)
        {
            using (WebClient webClient = new WebClient())
            {
                var exc = JsonConvert.DeserializeObject<ExcursionPL>(
                   webClient.DownloadString(requestUri + id)
               );
                return exc.excursion;
            }
        }

        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }


        //
        // GET: /ShoppingCart/
        public ActionResult GuideIndex()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }



        public void AddSelectionToCartMethode(string jsonString)
        {

            string con = "{\"jsonString\":" + jsonString + "}";
            var data = JsonConvert.DeserializeObject<jsonObject>(con);
            List<CartParam> CartParams = JsonConvert.DeserializeObject<List<CartParam>>(jsonString);


            foreach (CartParam cartp in CartParams)
            {
                AddToCartMethode(cartp.ExcursionnID, cartp.NbreAdultes, cartp.NbreEnfants, cartp.NbreBebes);
                nbreTickets += 1;
            }
        }


        public async void AddToCartMethode(string id, int nbrAdultes, int nbrEnfants, int nbrBebes)
        {
            // Retrieve the album from the database
            //var addedAlbum = sortieParSemaineRepository
            //    .FindOne(sortie => sortie.SortieID == id);
            var addedExcursionn = await GetExcursionByIdAsync(id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedExcursionn, nbrAdultes, nbrEnfants, nbrBebes);


        }


        //
        // GET: /Store/AddToCart/5
        public async Task<ActionResult> AddToCart(string id, int nbrAdultes, int nbrEnfants, int nbrBebes)
        {
            // Retrieve the album from the database
            //var addedAlbum = sortieParSemaineRepository
            //    .FindOne(sortie => sortie.SortieID == id);

            var addedExcursionn = await GetExcursionByIdAsync(id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedExcursionn, nbrAdultes, nbrEnfants, nbrBebes);

            // Go back to the main store page for more shopping

            //if (SessionData.CurrentUser.TypeUser == "Client Indirect")
            //{
            //    return RedirectToAction("Reservation","Index");
            //}
            //else
            //{
            return RedirectToAction("Index");
            //}

        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int id, string type)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            Excursionn exc = await GetExcursionByIdAsync(cartRepository.FindOne(item => item.RecordId == id).excursionnId);
            string albumName = exc.title;
            // Remove from cart
            //int itemCount = cart.RemoveFromCart(id, type);
            List<int> itemCount = cart.RemoveFromCart(id, type);
            int itemCountAdulte = itemCount[0];
            int itemCountEnfant = itemCount[1];
            int itemCountBebe = itemCount[2];

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCountAdulte = itemCountAdulte,
                ItemCountEnfant = itemCountEnfant,
                ItemCountBebe = itemCountBebe,
                DeleteId = id
            };
            return Json(results);

        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<ActionResult> AddToCart(int id, string type)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation

            Excursionn exc = await GetExcursionByIdAsync(cartRepository.FindOne(item => item.RecordId == id).excursionnId);
            string albumName = exc.title;

            // Remove from cart
            //int itemCount = cart.AddToCart(id, type);
            List<int> itemCount = cart.AddToCart(id, type);
            int itemCountAdulte = itemCount[0];
            int itemCountEnfant = itemCount[1];
            int itemCountBebe = itemCount[2];

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been added to your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                //ItemCount = itemCount,
                ItemCountAdulte = itemCountAdulte,
                ItemCountEnfant = itemCountEnfant,
                ItemCountBebe = itemCountBebe,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        //[ChildActionOnly]
        [HttpGet]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            int count;
            count = cart.GetCount();
            ViewData["CartCount"] = count;
            return PartialView("CartSummary");
        }


        [HttpGet]
        public ActionResult CartGuideSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            int count;
            count = nbreTickets;
            ViewData["CartCount"] = count;
            return PartialView("CartGuideSummary");
        }

    }
}
