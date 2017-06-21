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

namespace Excursion.Portail.Controllers
{
    public class ReservationValideController : Controller
    {

         private ReservationRepository reservationRepository;
         private ReservationValideRepository reservationValideRepository;

        public ReservationValideController()
        {
            this.reservationRepository = new ReservationRepository( new ExcursionContext());
            this.reservationValideRepository = new ReservationValideRepository(new ExcursionContext());
        }




        //
        // GET: /ReservationValide/

        public ActionResult Index()
        {
            return View();
        }

    }
}
