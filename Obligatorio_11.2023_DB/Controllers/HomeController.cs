using Microsoft.AspNetCore.Mvc;
using Obligatorio_11._2023_DB.Models;
using System.Diagnostics;

namespace Obligatorio_11._2023_DB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ConexionDB conect;

        public HomeController(ILogger<HomeController> logger, ConexionDB databaseContext)
        {
            _logger = logger;
            conect = databaseContext;
        }

        public IActionResult Index()
        {
            funcion();
            return View();
        }

        public IEnumerable<Funcionario> funcion()
        {
            string sql = "Select * From Funcionarios";
            return conect.ExecuteQuery<Funcionario>(sql);
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
    }
}