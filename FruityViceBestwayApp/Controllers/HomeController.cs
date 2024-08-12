using FruityViceBestwayApp.Models;
using FruityViceBestwayApp.Models.ViewModels;
using FruityViceBestwayApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FruityViceBestwayApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFruitViceService _fruitViceService;

        public HomeController(ILogger<HomeController> logger,
            IFruitViceService fruitViceService)
        {
            _logger = logger;
            _fruitViceService = fruitViceService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> FruitVice(string name)
        {
            var getFruitVice = await _fruitViceService.GetFruitByName(name);
            return Json(getFruitVice);
        }

        [HttpPost]
        public async Task<IActionResult> AddMetadata([FromBody] FruitRequestModel model)
        {
            var addMetadata = await _fruitViceService.AddMetadata(model.Name, model.Key, model.Value);
            return Json(addMetadata);
        }
        [HttpPut]
        public async Task<IActionResult> RemoveMetadata([FromBody] FruitRequestModel model)
        {
            var addMetadata = await _fruitViceService.RemoveMetadata(model.Name, model.Key, model.Value);
            return Json(addMetadata);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMetadata([FromBody] FruitRequestModel model)
        {
            var addMetadata = await _fruitViceService.UpdateMetadata(model.Name, model.Key, model.Value);
            return Json(addMetadata);
        }
    }
}
