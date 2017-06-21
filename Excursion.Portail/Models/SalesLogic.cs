using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excursion.Data;
using Excursion.Business.Repositories;

namespace Excursion.Portail.Models
{

    public class SalesLogic
    {

        private ReservationRepository reservationRepository;

        public List<Reservation> GetReservationsByFilterTypeC( string typeclient)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.FindMany(x => x.TypeClient == typeclient ).OrderByDescending(x => x.DateReserv).ToList();
            return reservations;       
        }

        public SalesLogic() 
        {
            this.reservationRepository = new ReservationRepository(new ExcursionContext());
        }

        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll().OrderByDescending(x => x.DateReserv).ToList();
            return reservations;
        }

        public List<Reservation> GetReservationsByDateRange(DateTime startDate, DateTime endDate, string typeclient)
        {
            List<Reservation> reservations = new List<Reservation>();
            if (typeclient == null)
            {
                reservations = reservationRepository.FindMany(x => x.DateReserv >= startDate && x.DateReserv <= endDate).OrderByDescending(x => x.DateReserv).ToList();
            }
            else
            {
                reservations = reservationRepository.FindMany(x => x.DateReserv >= startDate && x.DateReserv <= endDate && x.TypeClient == typeclient).OrderByDescending(x => x.DateReserv).ToList();
            }
            
            
            return reservations;
        }


        private const string _productName = "Product ";
        private const string _customerName = "Customer ";

        public List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();

            for (int i = 0; i < 100; i++)
            {
                sales.Add(new Sale()
                {
                    Id = i + 1,
                    Quantity = i * 3,
                    Product = _productName + i.ToString(),
                    Customer = _customerName + i.ToString(),
                    Date = DateTime.Today.AddDays(-i),
                    Amount = (decimal)((i * 3) * 4.56)
                });
            }

            return sales.OrderByDescending(s => s.Date).ToList<Sale>();
        }

        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            var sales = (from s in GetSales()
                         where s.Date >= startDate
                         && s.Date <= endDate
                         orderby s.Date descending
                         select s).ToList<Sale>();

            return sales;
        }


       
    }
}