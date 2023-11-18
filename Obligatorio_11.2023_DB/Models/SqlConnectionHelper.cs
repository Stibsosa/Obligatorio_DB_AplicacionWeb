namespace Obligatorio_11._2023_DB.Models;

using Microsoft.Data.SqlClient;
using System;

/*
public class SqlConnectionHelper
{
    private readonly string connectionString;

    public SqlConnectionHelper(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void OpenConnection()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexión abierta con éxito");

                // Puedes realizar operaciones con la conexión aquí

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
        }
    }
}*/

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;

public class MySqlConnectionHelper<T>
{
    private readonly string connectionString;

    public MySqlConnectionHelper(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<T> EjecutarConsulta(string sql)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            // Utilizar Dapper para mapear los resultados a objetos
            List<T> resultados = connection.Query<T>(sql).AsList();
            return resultados;
        }
    }

    public int EjecutarComando(string sql, object parametros = null)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            // Utilizar Dapper para ejecutar un comando
            return connection.Execute(sql, parametros);
        }
    }
}

