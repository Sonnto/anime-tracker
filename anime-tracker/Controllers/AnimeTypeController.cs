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

namespace anime_tracker.Controllers
{
    public class AnimeTypeController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();


        static AnimeTypeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44383/api/");
        }
        // GET: AnimeType/List
        public ActionResult List()
        {
            //Objective: communicate with our animeTypes data API to retrieve a list of anime types
            //curl https://localhost:44383/api/animetypedata/listanimetypes
           
            string url = "animetypedata/listanimetypes";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<AnimeTypeDto> animeTypes = response.Content.ReadAsAsync<IEnumerable<AnimeTypeDto>>().Result;
            Debug.WriteLine("Number of anime types received: ");
            Debug.WriteLine(animeTypes.Count());


            return View(animeTypes);
        }

        // GET: AnimeType/Details/5
        public ActionResult Details(int id)
        {
            //Objective: communicate with our animeType data API to retrieve a specific animeType
            //curl https://localhost:44383/api/animetypedata/findanimetype/{id}

            DetailsAnimeTypes ViewModel = new DetailsAnimeTypes();

            string url = "animetypedata/findanimetype/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            AnimeTypeDto selectedAnimeType = response.Content.ReadAsAsync<AnimeTypeDto>().Result;
            Debug.WriteLine("Anime Type received: ");
            Debug.WriteLine(selectedAnimeType.anime_type_name);

            //showcase info about anime related to specific anime types

            url = "animedata/listanimeforanimetype/" + id;
            response = client.GetAsync(url).Result;



            ViewModel.RelatedAnimes = RelatedAnimes;

            //=============WORKING ON THIS============

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
