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
    public class ThemeController : Controller
    {


        private ThemeRepository themeRepository;

        public ThemeController()
        {
            this.themeRepository = new ThemeRepository(new ExcursionContext());
        }



        //centre
        // GET: /Theme/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Ajouter()
        {
            return View();
        }

        //Ajouter Theme
        [HttpPost]
        public ActionResult Ajouter(Excursion.Data.Theme theme)
        {
            Excursion.Data.Theme th = new Data.Theme();
            th.Nom_de = theme.Nom_de;
            th.Nom_en = theme.Nom_en;
            th.Nom_fr = theme.Nom_fr;
            th.Nom_it = theme.Nom_it;

            th.CodeTheme = theme.CodeTheme;
            th.Description_de = theme.Description_de;
            th.Description_en = theme.Description_en;
            th.Description_fr = theme.Description_fr;
            th.Description_it = theme.Description_it; 
            
            try
            {

                HttpPostedFileBase file = Request.Files[0];
                byte[] imageSize = new byte[file.ContentLength];
                file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
                //
                string path = System.IO.Path.Combine(Server.MapPath("~/Images/Img/Theme"), System.IO.Path.GetFileName(file.FileName));
                file.SaveAs(path);
                //
                th.Photo = file.FileName.Split('\\').Last();

            }
            catch (Exception e)
            {
                ModelState.AddModelError("uploadError", e);
            }
            

            themeRepository.Add(th);
            themeRepository.Save();
            return RedirectToAction("Ajouter", new RouteValueDictionary(
                                  new { controller = "Excursion", action = "Ajouter" }));

        }


    }
}
