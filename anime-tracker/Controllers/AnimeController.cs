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

            AnimeDto SelectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;
            Debug.WriteLine("AnimeController.cs: Anime received: " + SelectedAnime.anime_title);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s  anime_type_id is: " + SelectedAnime.anime_type_id);
            Debug.WriteLine("AnimeController.cs: However, the anime_type_name is: " + SelectedAnime.anime_type_name);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s Anime ID value: " + SelectedAnime.anime_id);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s Start Date value: " + SelectedAnime.start_date);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s End Date value: " + SelectedAnime.end_date);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s Activity value: " + SelectedAnime.activity);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s Completed Eps value: " + SelectedAnime.completed_episodes);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s Total Eps value: " + SelectedAnime.total_episodes);
            Debug.WriteLine("AnimeController.cs: " + SelectedAnime.anime_title + "'s Rating value: " + SelectedAnime.rating+"/10");
            Debug.WriteLine("AnimeController.cs: "+SelectedAnime.anime_title + "'s Favourite value: " + SelectedAnime.favorite);

            ViewModel.SelectedAnime = SelectedAnime;

            //show associated genres with this anime between here

            url = "genredata/listgenresforanime/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<GenreDto> TaggedGenres = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;

            ViewModel.TaggedGenres = TaggedGenres;

            url = "genredata/listgenresnotforanime/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<GenreDto> AvailableGenres = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;

            ViewModel.AvailableGenres = AvailableGenres;

            return View(ViewModel);
        }

        //POST: Anime/Associate/{animeid}
        [HttpPost]

        public ActionResult Associate(int id, int genre_id)
        {
            Debug.WriteLine("Attempting to associate anime: " + id + " with genre: " + genre_id);
            //call API to associate anime with genre
            string url = "animedata/associateanimewithgenre/" + id + "/" + genre_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Anime/UnAssociate/{id}?genre_id={genre_id}
        [HttpGet]

        public ActionResult UnAssociate(int id, int genre_id)
        {
            Debug.WriteLine("Attempting to unassociate anime: " + id + " with genre: " + genre_id);
            //call API to unassociate anime with genre
            string url = "animedata/unassociateanimewithgenre/" + id + "/" + genre_id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UpdateAnime ViewModel = new UpdateAnime();

            //the existing anime information
            
            string url = "animedata/findanime/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AnimeDto SelectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;

            Debug.WriteLine("AnimeController.cs: selectedAnime's anime_type_id: " + SelectedAnime.anime_type_id);
            Debug.WriteLine("AnimeController.cs: Anime id for edit is: " + id);

            ViewModel.SelectedAnime = SelectedAnime;



            //also include all animeTypes to choose from when updating this anime
            url = "animetypedata/listanimetypes/";
            response = client.GetAsync(url).Result;
            IEnumerable<AnimeTypeDto> AnimeTypesOptions = response.Content.ReadAsAsync<IEnumerable<AnimeTypeDto>>().Result;

            Debug.WriteLine("AnimeController.cs: AnimeTypesOptions: " + AnimeTypesOptions);

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
            AnimeDto SelectedAnime = response.Content.ReadAsAsync<AnimeDto>().Result;
            return View(SelectedAnime);
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
