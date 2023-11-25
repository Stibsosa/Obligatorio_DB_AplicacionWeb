using Microsoft.AspNetCore.Mvc;
using Obligatorio_11._2023_DB.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Obligatorio_11._2023_DB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string connectionString = "Server=localhost;Port=3306;Database=obligatorio;User ID=root;Password=bernardo";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;        
        }

        public IActionResult Index()
        {
            //MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
            //Funcionario funcionario1 = sqlConnectionHelper.GetFuncionarioporCi(12345678);
            //funcionario1.Ci = 33147038;
            //funcionario1.LogId = 33147038;

            //Logins logins = sqlConnectionHelper.GetLogId(45753575, "juan.perez");


            return RedirectToAction("Login");
        }

        

        //Listar Funcionarios
        public IActionResult listarFuncionarios()
        {
            MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
            List<Funcionario> funcionarios = sqlConnectionHelper.GetFuncionarios();
            return View(funcionarios);
        }

        public IActionResult GetFuncionarios()
        {

            MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
            List<Funcionario> funcionarios = sqlConnectionHelper.GetFuncionarios();

            // Puedes hacer algo con la lista de funcionarios, como pasarla a la vista
            return View(funcionarios);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Logins logs)
        {
            try
            {
                MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
                Logins logins = sqlConnectionHelper.GetLogId(logs.LogId, logs.Password);
                if (logins != null)
                {
                    return RedirectToAction("listarFuncionarios"); //Cambiar pantalla a redirigir
                }
                else
                {
                    string mensaje = "El usuario no fue encontrado, por favor registrese.";
                    ViewBag.Mensaje = mensaje;
                    // Redirigir a la vista
                    return View();
                }
            }
            catch (Exception e)
            {
                string mensaje = "El usuario no fue encontrado, por favor registrese.";
                ViewBag.Mensaje = mensaje;

                // Redirigir a la vista
                return View();
            }
        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(Funcionario funcionario)
        {
            try
            {
                MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
                int nuevoIngreso = sqlConnectionHelper.InsertFuncionario(funcionario);
                return View();
            }
            catch (Exception e)
            {
                return View();
            }

        }
    }
}