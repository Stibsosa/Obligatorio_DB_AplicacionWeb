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

    public int InsertFuncionario(Funcionario funcionario)
    {
        string pw = funcionario.nombre + "." + funcionario.apellido;

        try
        {
            InsertLogIn(funcionario);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

               
                string sql = "insert into Funcionarios values (@Ci, @nombre, @apellido, @fecha_nacimiento, @direccion, @Telefono, @email, @LogId)";

                using (MySqlCommand command1 = new MySqlCommand(sql, connection))
                {
                    string fechaFormateada = (funcionario.fecha_nacimiento).ToString("dd-MM-yyyy");

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
                    string fechaFormateada = (funcionario.fecha_nacimiento).ToString("dd-MM-yyyy");

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
}

