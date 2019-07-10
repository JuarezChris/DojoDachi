using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoDachi.Models;



namespace DojoDachi.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            Pet newPet = new Pet()
            {
                Fullness = 20,
                Happiness = 20,
                Meal = 3,
                Energy = 50,

            };
            if (HttpContext.Session.GetInt32("Fullness") == null)
            {
                HttpContext.Session.SetString("Message", "Lets Play!");
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Meal", 3);
                HttpContext.Session.SetInt32("Energy", 50);
                ViewBag.Message = HttpContext.Session.GetString("Message");
                ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
                ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
                ViewBag.Meal = HttpContext.Session.GetInt32("Meal");
                ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
                return View(newPet);
            }
            else
            {
                // if(HttpContext.Session.GetInt32("Fullness") == 50 && HttpContext.Session.GetInt32("Happiness") == 50)
                // {
                //     return RedirectToAction("Win");
                // }
                ViewBag.Message = HttpContext.Session.GetString("Message");
                ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
                ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
                ViewBag.Meal = HttpContext.Session.GetInt32("Meal");
                ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
                return View(newPet);
            }
        }
        [Route("feed")]
        [HttpGet]
        public IActionResult Feed()
        {
            if (HttpContext.Session.GetInt32("Meal") >= 1)
            {
                Random Chancerand = new Random();
                int chance = Chancerand.Next(0, 5);
                if (chance == 0)
                {
                    int? negFood = HttpContext.Session.GetInt32("Meal") - 1;
                    HttpContext.Session.SetInt32("Meal", (int)negFood);
                    HttpContext.Session.SetString("Message", "Your Little Dude didn't like it!");
                    return RedirectToAction("index");
                }
                Random rand = new Random();
                int yum = rand.Next(5, 11);
                int? food = HttpContext.Session.GetInt32("Meal") - 1;
                int? full = HttpContext.Session.GetInt32("Fullness") + yum;
                HttpContext.Session.SetInt32("Meal", (int)food);
                HttpContext.Session.SetInt32("Fullness", (int)full);
                HttpContext.Session.SetString("Message", "Your Little Dude Ate!");
                return RedirectToAction("index");
            }
            else
            {
                HttpContext.Session.SetString("Message", "YOU NEED MEALS!");
                return RedirectToAction("index");
            }
        }
        [Route("play")]
        [HttpGet]
        public IActionResult Play()
        {
            Random Chancerand = new Random();
            int chance = Chancerand.Next(0, 5);
            if (chance == 0)
            {
                int? negEnergy = HttpContext.Session.GetInt32("Energy") - 5;
                HttpContext.Session.SetInt32("Energy", (int)negEnergy);
                HttpContext.Session.SetString("Message", "Your Little Dude didn't like it!");
                return RedirectToAction("index");
            }
            Random rand = new Random();
            int fun = rand.Next(5, 11);
            int? currHappy = HttpContext.Session.GetInt32("Happiness");
            int? play = HttpContext.Session.GetInt32("Energy") - 5;
            int? happy = currHappy + (int)fun;
            HttpContext.Session.SetInt32("Energy", (int)play);
            HttpContext.Session.SetInt32("Happiness", (int)happy);
            HttpContext.Session.SetString("Message", "Your Little Dude Played!");
            return RedirectToAction("index");
        }
        [Route("work")]
        [HttpGet]
        public IActionResult Work()
        {
            Random rand = new Random();
            int newMeal = rand.Next(1, 4);
            int? work = HttpContext.Session.GetInt32("Energy") - 5;
            int? food = HttpContext.Session.GetInt32("Meal") + newMeal;
            HttpContext.Session.SetInt32("Energy", (int)work);
            HttpContext.Session.SetInt32("Meal", (int)food);
            HttpContext.Session.SetString("Message", "Your Little Dude Worked!");
            return RedirectToAction("index");
        }
        [Route("sleep")]
        [HttpGet]
        public IActionResult Slept()
        {
            int? sleeping = HttpContext.Session.GetInt32("Energy") + 15;
            int? full = HttpContext.Session.GetInt32("Fullness") - 5;
            int? happy = HttpContext.Session.GetInt32("Happiness") - 5;
            HttpContext.Session.SetInt32("Energy", (int)sleeping);
            HttpContext.Session.SetInt32("Fullness", (int)full);
            HttpContext.Session.SetInt32("Happiness", (int)happy);
            HttpContext.Session.SetString("Message", "Your Little Dude Slept!");
            return RedirectToAction("index");
        }
        [Route("reset")]
        [HttpGet]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
        [Route("Win")]
        [HttpGet]
        public IActionResult Win()
        {
            return View();
        }
    }
}
