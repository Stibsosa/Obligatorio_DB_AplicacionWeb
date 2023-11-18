namespace Obligatorio_11._2023_DB.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class ConexionDB
{
    private readonly string _connectionString;

    public ConexionDB(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new MySqlConnection(_connectionString);

    public IEnumerable<T> ExecuteQuery<T>(string sql, object parameters = null)
    {
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            return dbConnection.Query<T>(sql, parameters);
        }
    }
}
