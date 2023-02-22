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
    public class GenreController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static GenreController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44383/api/");
        }
        // GET: Genre/List
        public ActionResult List()
        {
            //Objective: communicate with our genre data API to retrieve a list of genres
            //curl https://localhost:44383/api/genredata/listgenres
           
            string url = "genredata/listgenres";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<GenreDto> genres = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;
            Debug.WriteLine("Number of anime types received: ");
            Debug.WriteLine(genres.Count());


            return View(genres);
        }

        // GET: AnimeType/Details/5
        public ActionResult Details(int id)
        {
            //Objective: communicate with our genre data API to retrieve a specific genre
            //curl https://localhost:44383/api/genredata/findgenre/{id}

            string url = "genredata/findgenre/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            GenreDto selectedGenre = response.Content.ReadAsAsync<GenreDto>().Result;
            Debug.WriteLine("Anime Type received: ");
            Debug.WriteLine(selectedGenre.genre_name);

            return View(selectedGenre);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
