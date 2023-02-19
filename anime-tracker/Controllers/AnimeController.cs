using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using anime_tracker.Models;
using System.Web.Script.Serialization;

namespace anime_tracker.Controllers
{
    public class AnimeController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static AnimeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44383/api/animedata/");
        }
        // GET: Anime/List
        public ActionResult List()
        {
            //Objective: communicate with our anime data API to retrieve a list of anime
            //curl https://localhost:44383/api/animedata/listanimes
           
            string url = "listanimes";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<AnimeDto> animes = response.Content.ReadAsAsync<IEnumerable<AnimeDto>>().Result;
            Debug.WriteLine("Number of anime received: ");
            Debug.WriteLine(animes.Count());


            return View(animes);
        }

        // GET: Anime/Details/5
        public ActionResult Details(int id)
        {
            //Objective: communicate with our anime data API to retrieve a specific anime
            //curl https://localhost:44383/api/animedata/findanime/{id}

            string url = "findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            AnimeDto selectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;
            Debug.WriteLine("Anime received: ");
            Debug.WriteLine(selectedAnime.anime_title);

            return View(selectedAnime);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Anime/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Anime/Create
        [HttpPost]
        public ActionResult Create(Anime anime)
        {
            Debug.WriteLine("The inputted anime name is: ");
            Debug.WriteLine(anime.anime_title);
            //Objective: add a new anime into our system using the API
            //curl -H "Content-Type:application/json" -d anime.json https://localhost:44383/api/animedata/addanime
            string url = "addanime";

            string jsonpayload = jss.Serialize(anime);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");

            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Anime/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Anime/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Anime/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Anime/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
