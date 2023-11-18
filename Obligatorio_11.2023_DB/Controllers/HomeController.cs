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

        string connectionString = "Server=localhost;Database=obligatorio;User ID=root;Password=bernardo";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            SqlConnectionHelper sqlConnectionHelper = new SqlConnectionHelper(connectionString);

            // Abre la conexión
            sqlConnectionHelper.OpenConnection();
        }

        public IActionResult Index()
        {
            //funcion();
            return View();
        }

        /*        public IEnumerable<Funcionario> funcion()
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
                }*/


        //Listar Funcionarios
        public IActionResult listarFuncionarios()
        {
            return View(GetFuncionarios());
        }

        //Buscar Funcionario por ID
    /*    public IActionResult buscarFuncionarioPorId()
        {
            return View();
        }

        [HttpPost]
        public IActionResult buscarFuncionarioPorId(int id)
        {
            Funcionario funcionarioEnc = GetFuncionariosById(id);

            return RedirectToAction("DetalleFuncionarioId", funcionarioEnc);
        }

        public IActionResult DetalleFuncionarioId(Funcionario a)
        {
            return View(a);
        }

        public IActionResult CreateFuncionario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFuncionario(Funcionario nuevo)
        {
            try
            {
                CrearFuncionario(nuevo);
                return RedirectToAction("DetalleFuncionarioId", nuevo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult Updatefuncionario(int id)
        {
            Funcionario fun = GetFuncionariosById(id);
            return View(fun);
        }

        [HttpPost]
        public IActionResult Updatefuncionario(Funcionario nuevo)
        {
            try
            {
                ActualizarFuncionario(nuevo);
                return RedirectToAction("DetalleFuncionarioId", nuevo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult DeleteFuncionario(int id)
        {
            Funcionario fun = GetFuncionariosById(id);
            return View(fun);
        }

        [HttpPost]
        public IActionResult DeleteFuncionario(Funcionario fun)
        {
            try
            {
                BorrarFuncionario(fun.Ci);
                return RedirectToAction("listarFuncionarios");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }*/






        //METODOS GENERALES

        public IActionResult GetFuncionarios()
        {
            string consultaSql = "SELECT * FROM Funcionarios";
        
            MySqlConnectionHelper<Funcionario> mySqlConnectionHelper = new MySqlConnectionHelper<Funcionario>(connectionString);
            List<Funcionario> funcionarios = mySqlConnectionHelper.EjecutarConsulta(consultaSql);

            // Puedes hacer algo con la lista de funcionarios, como pasarla a la vista
            return View(funcionarios);
        }

        /*
        public IEnumerable<Funcionario> GetFuncionarios()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            string query = "SELECT * FROM Funcionarios";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))

                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Mapea los resultados a objetos Funcionario
                            Funcionario funcionario = new Funcionario
                            {
                                Ci = Convert.ToInt32(reader["id"]),
                                nombre = Convert.ToString(reader["nombre"]),
                                apellido = Convert.ToString(reader["apellido"]),
                                fecha_nacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                                direccion = Convert.ToString(reader["direccion"]),
                                Telefono = Convert.ToInt32(reader["Telefono"]),
                                email = Convert.ToString(reader["email"]),
                                LogId = Convert.ToString(reader["LogId"])
                            };

                            // Agrega el funcionario a la lista
                            funcionarios.Add(funcionario);
                        }
                    }
                }
            }
            return funcionarios;
        }*/

    }
}