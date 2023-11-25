using Microsoft.AspNetCore.Mvc;
using Obligatorio_11._2023_DB.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Microsoft.AspNetCore.Http;

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

        public IActionResult Bienvenido()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            return View();
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

        //Login de usuarios
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
                    HttpContext.Session.SetInt32("Usuario", logs.LogId); //declara ingreso de usuario valido y setea la ci como variable de entorno para el manejo de sesion

                    bool esAdmin = sqlConnectionHelper.esAdministrador(logs.LogId);
                    if (esAdmin == true)
                    {
                        HttpContext.Session.SetInt32("esAdministrador", logs.LogId); //declara ingreso de usuario valido y setea la ci como variable de entorno para el manejo de sesion
                    }


                    return RedirectToAction("Bienvenido"); //Cambiar pantalla a redirigir
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
        //Solapa de registro de usuarios cuando no existe en la tabla de funcionarios/logins
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(Funcionario funcionario)
        {
            try
            {
                funcionario.LogId = funcionario.Ci;
                MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
                int nuevoIngreso = sqlConnectionHelper.InsertFuncionario(funcionario);
                string mensaje = "El usuario fue ingresado con éxito, su contraseña por defecto es 'nombre.apellido'";
                ViewBag.Mensaje = mensaje;
                return View();
            }
            catch (Exception e)
            {
                string mensaje = e.Message;
                ViewBag.Mensaje = mensaje;
                return View();
            }
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }

        public IActionResult PeriodoActualizacion()
        {
            MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
            PeriodoActualizacion periodo = sqlConnectionHelper.periodoActualizacion();
            return View(periodo);
        }
        [HttpPost]
        public IActionResult PeriodoActualizacion(PeriodoActualizacion periodo)
        {
            try
            {
                MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
                int filasafectadas = sqlConnectionHelper.ActualizarPeriodo(periodo);
                if (filasafectadas == 1)
                {
                    string mensaje = "Se abrió un nuevo período de actualización de formularios";
                    ViewBag.Mensaje = mensaje;
                }
                else
                {
                    string mensaje = "No hubo modificación para el período autorizado";
                    ViewBag.Mensaje = mensaje;
                }
                return View();
            }
            catch (Exception e)
            {
                string mensaje = e.Message;
                ViewBag.Mensaje = mensaje;
                return View();
            }
        }
        public IActionResult Formulario() //Formulario de aquellos que no cargaron el carne de salud
        {
            int usuario = HttpContext.Session.GetInt32("Usuario").GetValueOrDefault();

            try
            {
                MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
                CarneSalud carne = sqlConnectionHelper.obtenerCarneSalud(usuario);
                bool estaEnFecha = sqlConnectionHelper.estaEnFecha();
                if (carne != null && estaEnFecha)
                {
                    return RedirectToAction("ActualizaCarne");//la persona ya ha cargado carné por lo tanto solo lo va a actualizar
                }
                else if (carne == null && estaEnFecha)
                {
                    return RedirectToAction("CrearCarne");
                }
                else
                {
                    TempData["Mensaje"] = "No se encuentra en período de actualización, intente luego";
                    return RedirectToAction("Bienvenido");
                }
            }
            catch (Exception e)
            {
                TempData["Mensaje"] = e.Message;
                return RedirectToAction("Bienvenido");
            }
        }

        public IActionResult ActualizaCarne()
        {
            MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
            CarneSalud carne = sqlConnectionHelper.obtenerCarneSalud(HttpContext.Session.GetInt32("Usuario").GetValueOrDefault());
            return View(carne);
        }
        [HttpPost]
        public IActionResult ActualizaCarne(CarneSalud carne, IFormFile foto)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    foto.CopyTo(ms);
                    carne.Comprobante = ms.ToArray();
                }
                MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
                int filasafectadas = sqlConnectionHelper.ActualizarCarne(carne);
                if (filasafectadas == 1)
                {
                    string mensaje = "Gracias por actualizar su carné";
                    ViewBag.Mensaje = mensaje;
                }
                else
                {
                    string mensaje = "No hubo modificaciones en su registro";
                    ViewBag.Mensaje = mensaje;
                }
                return View();
            }
            catch (Exception e)
            {
                string mensaje = e.Message;
                ViewBag.Mensaje = mensaje;
                return View();
            }
        }
        public IActionResult CrearCarne()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CrearCarne(CarneSalud carne, IFormFile foto)
        {

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    foto.CopyTo(ms);
                    carne.Comprobante = ms.ToArray();
                } 

                MySqlConnectionHelper sqlConnectionHelper = new MySqlConnectionHelper(connectionString);
                int filasafectadas = sqlConnectionHelper.cargarCarneSalud(carne);
                if (filasafectadas == 1)
                {
                    string mensaje = "Gracias por registrar su carné";
                    ViewBag.Mensaje = mensaje;
                }
                else
                {
                    string mensaje = "Por favor registre su carné";
                    ViewBag.Mensaje = mensaje;
                }
                return View();
            }
            catch (Exception e)
            {
                string mensaje = e.Message;
                ViewBag.Mensaje = mensaje;
                return View();
            }
        }
    }
}