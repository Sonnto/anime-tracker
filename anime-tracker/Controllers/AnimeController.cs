using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using anime_tracker.Models;

namespace anime_tracker.Controllers
{
    public class AnimeController : Controller
    {
        // GET: Anime/List
        public ActionResult List()
        {
            //Objective: communicate with our anime data API to retrieve a list of anime
            //curl https://localhost:44383/api/animedata/listanimes
           
            HttpClient client = new HttpClient () { };
            string url = "https://localhost:44383/api/animedata/listanimes";
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

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44383/api/animedata/findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            AnimeDto selectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;
            Debug.WriteLine("Anime received: ");
            Debug.WriteLine(selectedAnime.anime_title);

            return View(selectedAnime);
        }

        // GET: Anime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anime/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
