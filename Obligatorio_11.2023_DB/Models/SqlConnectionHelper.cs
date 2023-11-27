namespace Obligatorio_11._2023_DB.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;

public class MySqlConnectionHelper
{
    private readonly string connectionString;

    public MySqlConnectionHelper(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Funcionario> GetFuncionarios()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "select * from Funcionarios";

            // Utilizar Dapper para mapear los resultados a objetos
            List<Funcionario> resultados = connection.Query<Funcionario>(sql).AsList();
            return resultados;
        }
    }

    //public int EjecutarComando(string sql, object parametros = null)
    //{
    //    using (MySqlConnection connection = new MySqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        // Utilizar Dapper para ejecutar un comando
    //        return connection.Execute(sql, parametros);
    //    }
    //}


    //Insertar funcionario
    public int InsertFuncionario(Funcionario funcionario)
    {
        string pw = funcionario.nombre + "." + funcionario.apellido;

        try
        {
            InsertLogIn(funcionario);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //abre conexion
                connection.Open();

               
                string sql = "insert into Funcionarios values (@Ci, @nombre, @apellido, @fecha_nacimiento, @direccion, @Telefono, @email, @LogId)";

                using (MySqlCommand command1 = new MySqlCommand(sql, connection))
                {
                    string fechaFormateada = (funcionario.fecha_nacimiento).ToString("yyyy-MM-dd");

                    command1.Parameters.AddWithValue("@Ci", funcionario.Ci);
                    command1.Parameters.AddWithValue("@nombre", funcionario.nombre);
                    command1.Parameters.AddWithValue("@apellido", funcionario.apellido);
                    command1.Parameters.AddWithValue("@fecha_nacimiento", fechaFormateada);
                    command1.Parameters.AddWithValue("@direccion", funcionario.direccion);
                    command1.Parameters.AddWithValue("@Telefono", funcionario.Telefono);
                    command1.Parameters.AddWithValue("@email", funcionario.email);
                    command1.Parameters.AddWithValue("@LogId", funcionario.LogId);


                    // Devuelve filas afectadas
                    int filasAfectadas1 = command1.ExecuteNonQuery();

                    return filasAfectadas1;
                }

            }

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    //Busca funcionario por CI
    public Funcionario GetFuncionarioporCi(int Ci)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "select * from Funcionarios where Ci = "+Ci;

            // Trae funcionario por CI
            Funcionario funcionario = connection.Query<Funcionario>(sql).FirstOrDefault();
            return funcionario;
        }
    }

    //Setea atributos de un funcionario
    public int ActualizarFuncionario(Funcionario funcionario)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "update Funcionarios SET Ci = @Ci, nombre = @nombre, apellido = @apellido, fecha_nacimiento = @fecha_nacimiento, direccion = @direccion, Telefono = @Telefono, email = @email, LogId = @LogId where Ci = @Ci";


                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    string fechaFormateada = (funcionario.fecha_nacimiento).ToString("yyyy-MM-dd");

                    command.Parameters.AddWithValue("@Ci", funcionario.Ci);
                    command.Parameters.AddWithValue("@nombre", funcionario.nombre);
                    command.Parameters.AddWithValue("@apellido", funcionario.apellido);
                    command.Parameters.AddWithValue("@fecha_nacimiento", fechaFormateada);
                    command.Parameters.AddWithValue("@direccion", funcionario.direccion);
                    command.Parameters.AddWithValue("@Telefono", funcionario.Telefono);
                    command.Parameters.AddWithValue("@email", funcionario.email);
                    command.Parameters.AddWithValue("@LogId", funcionario.LogId);

                    // Devuelve filas afectadas
                    int filasAfectadas = command.ExecuteNonQuery();

                    return filasAfectadas;
                }               


            }

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    //Inserta registro en tabla Logins (se pretende una insersión inicial a la de funcionarios para ya generar un LogId - Foreing Key en tabla Funcionarios)
    public int InsertLogIn(Funcionario funcionario)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //connection.Open();

                string sql = "insert into Logins (LogId, Password) VALUES (@LogId1, @Password1)";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    int logId = funcionario.Ci;
                    string password = funcionario.nombre + "." + funcionario.apellido;

                    command.Parameters.AddWithValue("@LogId1", logId);
                    command.Parameters.AddWithValue("@Password1", password);

                    connection.Open();
                    // Devuelve filas afectadas
                    int filasAfectadas = command.ExecuteNonQuery();

                    return filasAfectadas;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    //Devuelve registro de Logins (tabla) si la persona tiene uno
    public Logins GetLogId(int LogId, string Pwd)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Logins where LogId = " + LogId + " and Password = '" + Pwd + "'";

                // Trae Logins por LogId
                Logins log = connection.Query<Logins>(sql).FirstOrDefault();
                return log;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public bool esAdministrador(int LogId)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Administradores where LogId = " + LogId;

                // Trae si es administrador
                Administradores administrador = connection.Query<Administradores>(sql).FirstOrDefault();
                if (administrador != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public PeriodoActualizacion periodoActualizacion()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Periodos_Actualizacion";

                // Trae si es administrador
                PeriodoActualizacion periodo = connection.Query<PeriodoActualizacion>(sql).FirstOrDefault();

                return periodo;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public int ActualizarPeriodo(PeriodoActualizacion periodo_a_actualizar)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "update Periodos_Actualizacion SET Year = @Year, Semestre = @Semestre, Fch_Inicio = @fecha_inicio, Fch_Fin = @fecha_fin";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    string fechaIniFormateada = (periodo_a_actualizar.Fch_Inicio).ToString("yyyy-MM-dd");
                    string fechaFinFormateada = (periodo_a_actualizar.Fch_Fin).ToString("yyyy-MM-dd");

                    command.Parameters.AddWithValue("@Year", periodo_a_actualizar.Year);
                    command.Parameters.AddWithValue("@Semestre", periodo_a_actualizar.Semestre);
                    command.Parameters.AddWithValue("@fecha_inicio", fechaIniFormateada);
                    command.Parameters.AddWithValue("@fecha_fin", fechaFinFormateada);

                    // Devuelve filas afectadas
                    int filasAfectadas = command.ExecuteNonQuery();

                    return filasAfectadas;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public bool estaEnFecha()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Periodos_Actualizacion";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    // Trae funcionario por CI
                    PeriodoActualizacion periodoActual = connection.Query<PeriodoActualizacion>(sql).FirstOrDefault();
                    DateTime fechaInicio = periodoActual.Fch_Inicio;
                    DateTime fechaFin = periodoActual.Fch_Fin;

                    // Fecha específica que quieres validar
                    DateTime fechaHoy = DateTime.Today;

                    // Verificar si la fecha está dentro del rango
                    if (fechaHoy >= fechaInicio && fechaHoy <= fechaFin)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public CarneSalud obtenerCarneSalud(int cedula)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Carnet_Salud where Ci = " + cedula;


                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    // Trae funcionario por CI
                    CarneSalud carne = connection.Query<CarneSalud>(sql).FirstOrDefault();
                    return carne;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public int ActualizarCarne(CarneSalud carne)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "update Carnet_Salud SET Fch_Emision = @Fch_Emision, Fch_Vencimiento = @Fch_Vencimiento, Comprobante = @Comprobante where Ci = @Ci";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    string fechaEmision = (carne.Fch_Emision).ToString("yyyy-MM-dd");
                    string fechaVencimiento = (carne.Fch_Vencimiento).ToString("yyyy-MM-dd");

                    command.Parameters.AddWithValue("@Ci", carne.Ci);
                    command.Parameters.AddWithValue("@Fch_Emision", fechaEmision);
                    command.Parameters.AddWithValue("@Fch_Vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@Comprobante", carne.Comprobante);

                    // Devuelve filas afectadas
                    int filasAfectadas = command.ExecuteNonQuery();

                    return filasAfectadas;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public int cargarCarneSalud(CarneSalud carne)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "insert into Carnet_Salud (Ci,Fch_Emision,Fch_Vencimiento,Comprobante) VALUES (@Ci,@Fch_Emision,@Fch_Vencimiento,@Comprobante)";


                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    string fechaEmision = (carne.Fch_Emision).ToString("yyyy-MM-dd");
                    string fechaVencimiento = (carne.Fch_Vencimiento).ToString("yyyy-MM-dd");

                    command.Parameters.AddWithValue("@Ci", carne.Ci);
                    command.Parameters.AddWithValue("@Fch_Emision", fechaEmision);
                    command.Parameters.AddWithValue("@Fch_Vencimiento", fechaVencimiento);
                    command.Parameters.AddWithValue("@Comprobante", carne.Comprobante);

                    // Devuelve filas afectadas
                    int filasAfectadas = command.ExecuteNonQuery();

                    return filasAfectadas;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public List<Funcionario> DetalleFuncionario(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "select * from Funcionarios where Ci = "+ id;

            List<Funcionario> resultados = connection.Query<Funcionario>(sql).AsList();
            return resultados;

        }
    }
    public int EliminarFuncionario(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "delete from Logins where LogId = " + id;

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Ci", id);
                int filasafectadas = command.ExecuteNonQuery();
                return filasafectadas;
            }
        }
    }

    public List<CarneVencido> CarneVencido()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "select cs.Fch_Vencimiento, f.Ci,f.nombre,f.apellido, f.Telefono, f.email, f.direccion from Funcionarios f join Carnet_Salud cs on f.Ci = cs.Ci where cs.Fch_Vencimiento < NOW()";

            List<CarneVencido> resultados = connection.Query<CarneVencido>(sql).AsList();
            return resultados;

        }
    }

    public List<Reservas_Disponibles> FechasClinica()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Reservas_Disponibles";

                // Trae si es administrador
                List<Reservas_Disponibles> fchDisponible = connection.Query<Reservas_Disponibles>(sql).AsList();

                return fchDisponible;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public int ActualizarFechasClinica(Reservas_Disponibles fechaDisponible)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "insert into Reservas_Disponibles (Fch_Disponible) values (@Fch_Disponible)";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    string fechaFormateada = (fechaDisponible.Fch_Disponible).ToString("yyyy-MM-dd HH:mm:ss");

                    command.Parameters.AddWithValue("@Fch_Disponible", fechaFormateada);

                    // Devuelve filas afectadas
                    int filasAfectadas = command.ExecuteNonQuery();

                    return filasAfectadas;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public int ReservarFecha(Reservas_Disponibles reserva, int ci)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql1 = "insert into Reservas_Clinica (Ci,Id,Fch_Reserva) values (@Ci,@Id,@Fch_Disponible)";
            string sql2 = "delete from Reservas_Disponibles where Id = @Id";

            using (MySqlCommand command = new MySqlCommand(sql1, connection))
            {
                string fechaFormateada = (reserva.Fch_Disponible).ToString("yyyy-MM-dd HH:mm:ss");

                command.Parameters.AddWithValue("@Ci", ci);
                command.Parameters.AddWithValue("@Id", reserva.Id);
                command.Parameters.AddWithValue("@Fch_Disponible", fechaFormateada);

                int filasafectadas1 = command.ExecuteNonQuery();

                if (filasafectadas1 > 0)
                {
                    using (MySqlCommand command2 = new MySqlCommand(sql2, connection))
                    {
                        command2.Parameters.AddWithValue("@Id", reserva.Id);

                        int filasAfectadas2 = command2.ExecuteNonQuery();
                        return filasAfectadas2;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
    }

}

