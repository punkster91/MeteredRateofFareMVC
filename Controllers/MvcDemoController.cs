using MeteredRateofFare.Managers;
using MeteredRateofFare.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeteredRateofFare.Controllers
{
    public class MvcDemoController : Controller
    {
        public MvcDemoController()
        {
           
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: MvcDemo/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,StartTime,MilesTraveled,MinutesTraveled")]MeteredFareModel fare)
        {
            if (ModelState.IsValid)
            {
                new FareManager().ComputeTotalCost(fare);
                // save record to database

                // redirect view to a table of past fares
            }

            return View(fare);
        }
    }
}
