using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using anime_tracker.Models;
using System.Web.Script.Serialization;
using anime_tracker.Models.ViewModels;
using System.Globalization;

namespace anime_tracker.Controllers
{
    public class AnimeController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static AnimeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44383/api/");
        }
        // GET: Anime/List
        public ActionResult List()
        {
            //Objective: communicate with our anime data API to retrieve a list of anime
            //curl https://localhost:44383/api/animedata/listanimes
           
            string url = "animedata/listanimes";
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
            DetailsAnime ViewModel = new DetailsAnime();

            //Objective: communicate with our anime data API to retrieve a specific anime
            //curl https://localhost:44383/api/animedata/findanime/{id}

            string url = "animedata/findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            AnimeDto selectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;
            Debug.WriteLine("Anime received: ");
            Debug.WriteLine(selectedAnime.anime_title);

            ViewModel.SelectedAnime = selectedAnime;

            //show associated genres with this anime between here

            //show associated genres with this anime between here

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Anime/New
        // =============================================WORKING ON THIS PART==========================================================================================================================
        public ActionResult New()
        {
            //Information about Anime Types and Genres

            string url = "animetypedata/listanimetypes";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<AnimeTypeDto> animeTypesOptions = response.Content.ReadAsAsync<IEnumerable<AnimeTypeDto>>().Result;

            return View(animeTypesOptions);
        }

        // POST: Anime/Create
        [HttpPost]
        public ActionResult Create(Anime anime)
        {
            // Parses dates from input string

            Debug.WriteLine("The inputted anime name is: ");
            Debug.WriteLine(anime.anime_title);
            //Objective: add a new anime into our system using the API
            //curl -H "Content-Type:application/json" -d anime.json https://localhost:44383/api/animedata/addanime
            string url = "animedata/addanime";

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
            UpdateAnime ViewModel = new UpdateAnime();

            //the existing anime information
            
            string url = "animedata/findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AnimeDto selectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;
            ViewModel.SelectedAnime = selectedAnime;



            //also include all animeTypes to choose from when updating this anime
            url = "animetypedata/listanimetypes/";
            response = client.GetAsync(url).Result;
            IEnumerable<AnimeTypeDto> AnimeTypesOptions = response.Content.ReadAsAsync<IEnumerable<AnimeTypeDto>>().Result;

            ViewModel.AnimeTypesOptions = AnimeTypesOptions;

            return View(ViewModel);
        }

        // POST: Anime/Update/5
        [HttpPost]
        public ActionResult Update(int id, Anime anime)
        {
            string url = "animedata/updateanime/" + id;
            string jsonpayload = jss.Serialize(anime);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if(response.IsSuccessStatusCode)
            {
                Debug.WriteLine(content);
                return RedirectToAction("List");
            }
            else
            {
                Debug.WriteLine(content);
                return RedirectToAction("Error");
            }
        }

        // GET: Anime/Delete/5
        
        public ActionResult DeleteConfirm(int id)
        {
            string url = "animedata/findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AnimeDto selectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;
            return View(selectedAnime);
        }

        // POST: Anime/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "animedata/deleteanime/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
