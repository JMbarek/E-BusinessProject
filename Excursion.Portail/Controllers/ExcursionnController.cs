using Excursion.Data;
using Excursion.Portail.Utilities;
using Excursion.Portail.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Excursion.Portail.Controllers
{
    public class ExcursionnController : Controller
    {
        private HttpClient httpClient;
        private const string BaseUri = "http://localhost:27017/";
        private const string requestUri = "http://localhost:27017/api/excursions";


        //public Excursions GetExcursions()
        //{
        //    using (WebClient webClient = new WebClient())
        //    {
        //        var excs = JsonConvert.DeserializeObject<Excursions>(
        //           webClient.DownloadString(requestUri)
        //       );
        //        return excs;
        //    }
        //}

        public async Task<List<Excursionn>> GetExcursionsAsync()
        {
            httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(requestUri);
            return JsonConvert.DeserializeObject<Excursions>(responseString).excursions;
        }

        //
        // GET: /Excursionn/

        public async Task<ActionResult> Index()
        {
            List<Excursionn> listExc = await GetExcursionsAsync();
            return Json(new { ExcursionsList = listExc });
        }


        //public ActionResult Index()
        //{
        //    return View();
        //}

        //
        // GET: /Excursionn/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Excursionn/Create

        public ActionResult Create()
        {
            return View();
        }


        public async Task<HttpResponseMessage> PostJsonEncodedContent<T>(T content) where T : Excursionn
        {
            //string requestUri = "api/excursions";
            //httpClient.BaseAddress = new Uri(BaseUri);
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            content.organizerId = SessionData.CurrentUser.UserID;
            string jsonString = "{\"excursion\":" + JsonConvert.SerializeObject(content) + "}";
            ExcursionPL pl = new ExcursionPL();
            pl.excursion = content;
            var response = await httpClient.PostAsJsonAsync(requestUri, pl);
            //var response = await httpClient.PostAsJsonAsync(requestUri, jsonString);
            //var contents = await response.Content.ReadAsStringAsync();
            return response;
        }

        public async Task<HttpResponseMessage> PutJsonEncodedContent<T>(T content) where T : Excursionn
        {
            //string requestUri = "api/excursions";
            //httpClient.BaseAddress = new Uri(BaseUri);
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            content.organizerId = SessionData.CurrentUser.UserID;
            string jsonString = "{\"excursion\":" + JsonConvert.SerializeObject(content) + "}";
            ExcursionPL pl = new ExcursionPL();
            pl.excursion = content;
            var response = await httpClient.PutAsJsonAsync(requestUri + "/" + content.excursionnId, pl);
            //var response = await httpClient.PostAsJsonAsync(requestUri, jsonString);
            //var contents = await response.Content.ReadAsStringAsync();
            return response;
        }


        public async Task<HttpResponseMessage> DeleteExcursionAsync<T>(T content) where T : Excursionn
        {
            httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync(requestUri + "/" + content.excursionnId);
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UploadImageToExcursiono(string url, byte[] ImageData)
        {
            httpClient = new HttpClient();
            var requestContent = new MultipartFormDataContent();
            //    here you can specify boundary if you need---^
            var imageContent = new ByteArrayContent(ImageData);
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "image", "image.jpg");
            return await httpClient.PostAsync(url, requestContent);
        }

        public async Task<HttpResponseMessage> UploadImageToExcursion(string filePath)
        {
            string id = "58f8ef9fb8cb32286824a7cc";
            string url = "http://localhost:27017/api/excursions/" + id + "/images/upload";
            MultipartFormDataContent requestContent = new MultipartFormDataContent();
            var fileContent = new StreamContent(System.IO.File.OpenRead(filePath));
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            fileContent.Headers.ContentDisposition.FileName = filePath.Split('\\').Last();
            fileContent.Headers.ContentDisposition.Name = "uploadfile";
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            requestContent.Add(fileContent);
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            HttpResponseMessage response = await httpClient.PostAsync(url, requestContent);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage(FileUploadViewModel fileUploadModel)
        {
            HttpPostedFileBase file;

            try
            {
                file = Request.Files[0];
                byte[] imageSize = new byte[file.ContentLength];
                file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
                string filePath = System.IO.Path.Combine(Server.MapPath("~/Images/trips"), System.IO.Path.GetFileName(file.FileName));
                //string fileName = file.FileName.Split('\\').Last();  // not used, it is included filepath
                file.SaveAs(filePath);
                HttpResponseMessage response = await UploadImageToExcursion(filePath);
                if ((response.StatusCode == System.Net.HttpStatusCode.OK) && !string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Index", "Home");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("uploadError", e);
                return View();
            }
        }

        //
        // POST: /Excursionn/Create

        [HttpPost]
        public async Task<ActionResult> Create(CreateTripViewModel tripmodel)
        {
            try
            {
                HttpResponseMessage response = await PostJsonEncodedContent(tripmodel.Excursionn);
                if ((response.StatusCode == System.Net.HttpStatusCode.OK) && !string.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Excursionn/Edit/5

        public async Task<ActionResult> Edit(string id)
        {
            List<Excursionn> listExc = await GetExcursionsAsync();
            var exc = listExc.Where(e => e.excursionnId == id).FirstOrDefault();
            return View(exc);
        }

        //
        // POST: /Excursionn/Edit/5

        [HttpPost]
        public async Task<ActionResult> Edit(string id, Excursionn excursion)
        {
            try
            {
                HttpResponseMessage response = await PutJsonEncodedContent(excursion);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent) //204

                    return RedirectToAction("Index", "Home");

                else

                    return View();

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Excursionn/Delete/5

        public async Task<ActionResult> Delete(string id)
        {
            List<Excursionn> listExc = await GetExcursionsAsync();
            var exc = listExc.Where(e => e.excursionnId == id).FirstOrDefault();
            var response = await DeleteExcursionAsync(exc);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        //
        // POST: /Excursionn/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
