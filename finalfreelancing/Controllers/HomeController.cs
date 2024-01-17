using finalfreelancing.Data;
using finalfreelancing.Models;
using finalfreelancing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace finalfreelancing.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _db;
        public HomeController(ILogger<HomeController> logger,AppDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            FreelanceViewModel model = new FreelanceViewModel()
            {
                Freelancers = _db.freelancers.ToList(),
                Topfreelancers = _db.topfreelancers.ToList()
            };

            return View(model);
        }
       // _db.freelancers.OrderByDescending(x=>x.Rating)
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}