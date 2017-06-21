using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excursion.Business.Repositories;
using Excursion.Data;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace Excursion.Portail.Models
{
    public partial class ShoppingCart
    {
        //ExcursionEntities storeDB = new ExcursionEntities();
        private HttpClient httpClient;
        private const string requestUri = "http://localhost:27017/api/excursions/";

        private CartRepository cartRepository;
        private ExcursionnRepository excursionnRepository;
        private OrderRepository orderRepository;
        private OrderDetailRepository orderDetailRepository;
        public ShoppingCart()
        {
            this.httpClient = new HttpClient();
            this.cartRepository = new CartRepository(new ExcursionContext());
            this.excursionnRepository = new ExcursionnRepository(new ExcursionContext());
            this.orderRepository = new OrderRepository(new ExcursionContext());
            this.orderDetailRepository = new OrderDetailRepository(new ExcursionContext());
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

        public async Task<Excursionn> GetExcursionByIdAsync(string id)
        {
            var responseString = await httpClient.GetStringAsync(requestUri + id);
            return JsonConvert.DeserializeObject<ExcursionPL>(responseString).excursion;
        }



        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Excursionn excursion, int nbrAdultes, int nbrEnfants, int nbrBebes)
        {
            excursionnRepository.Add(excursion);
            excursionnRepository.Save();
            // Get the matching cart and album instances
            var cartItem = cartRepository.FindOne(
                       c => c.CartId == ShoppingCartId
                       && c.excursionnId == excursion.excursionnId);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                //Excursionn ex = new Excursionn();
                //ex = excursion;
                cartItem = new Cart
                {
                    excursionnId = excursion.excursionnId,
                    //Excursionn = ex,
                    CartId = ShoppingCartId,
                    CountAdulte = nbrAdultes,
                    CountEnfant = nbrEnfants,
                    CountBebe = nbrBebes,
                    DateCreated = DateTime.Now
                };
                cartRepository.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                /*********/
                //cartItem.Count++;
                cartItem.CountAdulte += nbrAdultes;
                cartItem.CountEnfant += nbrEnfants;
                cartItem.CountBebe += nbrBebes;
            }
            // Save changes
            cartRepository.Save();
        }

        public List<int> RemoveFromCart(int id, string type)
        {
            // Get the cart
            var cartItem = cartRepository.FindOne(
                 cart => cart.CartId == ShoppingCartId
                 && cart.RecordId == id);
            int itemCountAdulte = 0;
            int itemCountEnfant = 0;
            int itemCountBebe = 0;
            if (cartItem != null)
            {

                if (type == "ad")
                {
                    if (cartItem.CountAdulte >= 1)
                    {
                        itemCountEnfant = cartItem.CountEnfant;
                        itemCountBebe = cartItem.CountBebe;
                        if ((cartItem.CountAdulte == 1) && (itemCountEnfant == 0) && (itemCountBebe == 0))
                        {
                            cartRepository.Delete(cartItem);
                            cartRepository.Save();
                        }
                        else
                        {
                            cartItem.CountAdulte--;
                            itemCountAdulte = cartItem.CountAdulte;
                            cartRepository.Save();
                        }
                    }
                    else
                    {
                        itemCountAdulte = 0;
                        itemCountBebe = cartItem.CountBebe;
                        itemCountEnfant = cartItem.CountEnfant;
                    }
                }
                else if (type == "en")
                {
                    if (cartItem.CountEnfant >= 1)
                    {
                        itemCountAdulte = cartItem.CountAdulte;
                        itemCountBebe = cartItem.CountBebe;
                        if ((cartItem.CountEnfant == 1) && (itemCountBebe == 0) && (itemCountAdulte == 0))
                        {
                            cartRepository.Delete(cartItem);
                            cartRepository.Save();
                        }
                        else
                        {
                            cartItem.CountEnfant--;
                            itemCountEnfant = cartItem.CountEnfant;
                            cartRepository.Save();
                        }
                    }
                    else
                    {
                        itemCountEnfant = 0;
                        itemCountBebe = cartItem.CountBebe;
                        itemCountAdulte = cartItem.CountAdulte;
                    }
                }
                else if (type == "bb")
                {
                    if (cartItem.CountBebe >= 1)
                    {
                        itemCountEnfant = cartItem.CountEnfant;
                        itemCountAdulte = cartItem.CountAdulte;
                        if ((cartItem.CountBebe == 1) && (itemCountEnfant == 0) && (itemCountAdulte == 0))
                        {
                            cartRepository.Delete(cartItem);
                            cartRepository.Save();
                        }
                        else
                        {
                            cartItem.CountBebe--;
                            itemCountBebe = cartItem.CountBebe;
                            cartRepository.Save();
                        }
                    }
                    else
                    {
                        itemCountBebe = 0;
                        itemCountEnfant = cartItem.CountEnfant;
                        itemCountAdulte = cartItem.CountAdulte;
                    }
                }
            }
            List<int> itemCount = new List<int>();
            itemCount.Add(itemCountAdulte); itemCount.Add(itemCountEnfant); itemCount.Add(itemCountBebe);
            return itemCount;
        }

        public List<int> AddToCart(int id, string type)
        {
            // Get the cart
            var cartItem = cartRepository.FindOne(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCountAdulte = 0;
            int itemCountEnfant = 0;
            int itemCountBebe = 0;

            if (cartItem != null)
            {

                if (type == "ad")
                {
                    if (cartItem.CountAdulte <= 30)
                    {
                        cartItem.CountAdulte++;
                        itemCountAdulte = cartItem.CountAdulte;
                        cartRepository.Save();
                        itemCountEnfant = cartItem.CountEnfant;
                        itemCountBebe = cartItem.CountBebe;
                    }
                }
                else if (type == "en")
                {
                    if (cartItem.CountEnfant <= 30)
                    {
                        cartItem.CountEnfant++;
                        itemCountEnfant = cartItem.CountEnfant;
                        cartRepository.Save();
                        itemCountAdulte = cartItem.CountAdulte;
                        itemCountBebe = cartItem.CountBebe;
                    }
                }
                else if (type == "bb")
                {
                    if (cartItem.CountBebe <= 30)
                    {
                        cartItem.CountBebe++;
                        itemCountBebe = cartItem.CountBebe;
                        cartRepository.Save();
                        itemCountEnfant = cartItem.CountEnfant;
                        itemCountAdulte = cartItem.CountAdulte;
                    }
                }
            }

            List<int> itemCount = new List<int>();
            itemCount.Add(itemCountAdulte); itemCount.Add(itemCountEnfant); itemCount.Add(itemCountBebe);
            return itemCount;
        }


        public void EmptyCart()
        {
            var cartItems = cartRepository.FindMany(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                cartRepository.Delete(cartItem);
            }

            // Save changes
            cartRepository.Save();
        }

        public List<Cart> GetCartItems()
        {
            return cartRepository.FindMany(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            //int? count = (from cartItems in cartRepository.GetAll()
            //              where cartItems.CartId == ShoppingCartId
            //              select (int?)cartItems.Count).Sum();
            //****

            int? countad = (from cartItems in cartRepository.GetAll()
                            where cartItems.CartId == ShoppingCartId
                            select (int?)cartItems.CountAdulte).Sum();
            int? counten = (from cartItems in cartRepository.GetAll()
                            where cartItems.CartId == ShoppingCartId
                            select (int?)cartItems.CountEnfant).Sum();
            int? countbb = (from cartItems in cartRepository.GetAll()
                            where cartItems.CartId == ShoppingCartId
                            select (int?)cartItems.CountBebe).Sum();

            int? count = countad + counten + countbb;

            return count ?? 0;
            //return 0;
        }


        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            decimal? totalad = (from cartItems in cartRepository.GetAll()
                                where cartItems.CartId == ShoppingCartId
                                select (int?)cartItems.CountAdulte * (decimal)(GetExcursionById(cartItems.excursionnId).priceAdult)).Sum();
            decimal? totalen = (from cartItems in cartRepository.GetAll()
                                where cartItems.CartId == ShoppingCartId
                                select (int?)cartItems.CountEnfant * (decimal)(GetExcursionById(cartItems.excursionnId).priceKind)).Sum();
            decimal? totalbb = (from cartItems in cartRepository.GetAll()
                                where cartItems.CartId == ShoppingCartId
                                select (int?)cartItems.CountBebe * (decimal)(GetExcursionById(cartItems.excursionnId).priceBaby)).Sum();
            decimal? total = totalad + totalen + totalbb;

            return total ?? decimal.Zero;
            //  ???????????????????????????????????
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    excursionnId = item.excursionnId,
                    OrderId = order.OrderId,
                    UnitPriceAd = (decimal)(GetExcursionById(item.excursionnId).priceAdult),
                    UnitPriceEn = (decimal)(GetExcursionById(item.excursionnId).priceKind),
                    UnitPriceBb = (decimal)(GetExcursionById(item.excursionnId).priceBaby),
                    QuantityAd = item.CountAdulte,
                    QuantityEn = item.CountEnfant,
                    QuantityBb = item.CountBebe
                    //  ???????????????????????????????????
                };

                // Set the order total of the shopping cart
                orderTotal += ((item.CountAdulte * (decimal)GetExcursionById(item.excursionnId).priceAdult) +
                    (item.CountBebe * (decimal)GetExcursionById(item.excursionnId).priceBaby) +
                    (item.CountEnfant * (decimal)GetExcursionById(item.excursionnId).priceKind));
                //  ???????????????????????????????????
                orderDetailRepository.Add(orderDetail);

            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            orderDetailRepository.Save();

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = cartRepository.FindMany(c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            cartRepository.Save();
        }
    }

}