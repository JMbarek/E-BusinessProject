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
    public class HotelController : Controller
    {

        private RegionRepository regionRepository ;
        private HotelRepository hotelRepository;

        public HotelController()
        {
            this.regionRepository = new Excursion.Business.Repositories.RegionRepository(new ExcursionContext());
            this.hotelRepository = new HotelRepository(new ExcursionContext());
        }
        
        //
        // GET: /Hotel/

        public ActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        public ActionResult Ajouter()
        {
            #region dropdownlist
            var SysLst = new List<string>();
            var SysQry = from b in regionRepository.FindMany( x=> x.Zone.CentreID == SessionData.CurrentUser.CentreID)
                         select b.Nom;
            SysLst.AddRange(SysQry.Distinct());
            ViewBag.regionList = new SelectList(SysLst);

            var SysLst1 = new List<string>();
            SysLst1.Add("1 étoile"); SysLst1.Add("2 étoiles"); SysLst1.Add("3 étoiles"); SysLst1.Add("4 étoiles"); SysLst1.Add("5 étoiles");
            ViewBag.NbreEtoileList = new SelectList(SysLst1);

            #endregion
            return View();
        }


        //Ajouter Hotel
        [HttpPost]
        public ActionResult Ajouter(Excursion.Data.Hotel hotel, string NomRegion, string NbreEtoiles)
        {
            Excursion.Data.Hotel htl = new Data.Hotel();
            htl.CodeHotel = hotel.CodeHotel;
            htl.Nom = hotel.Nom;
            htl.NbrEtoiles = NbreEtoiles;
           
            //
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                byte[] imageSize = new byte[file.ContentLength];
                file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
                //
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/Img/Hotel"), System.IO.Path.GetFileName(file.FileName));
                file.SaveAs(path);
                //
                htl.Photo = file.FileName.Split('\\').Last();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("uploadError", e);
            }
            //

            htl.RegionID = regionRepository.FindOne(x => x.Nom == NomRegion).RegionID;  
            hotelRepository.Add(htl);
            hotelRepository.Save();

            #region dropdownlist
            var SysLst = new List<string>();
            var SysQry = from b in regionRepository.FindMany(x => x.Zone.CentreID == SessionData.CurrentUser.CentreID)
                         select b.Nom;
            SysLst.AddRange(SysQry.Distinct());
            ViewBag.regionList = new SelectList(SysLst);

            var SysLst1 = new List<string>();
            SysLst1.Add("1 étoile"); SysLst1.Add("2 étoiles"); SysLst1.Add("3 étoiles"); SysLst1.Add("4 étoiles"); SysLst1.Add("5 étoiles");
            ViewBag.NbreEtoileList = new SelectList(SysLst1);

            #endregion

            return RedirectToAction("Index", new RouteValueDictionary(
                                  new { controller = "Reservation", action = "Index" }));
       
        }



    }
}
