namespace Obligatorio_11._2023_DB.Models
{
    public class Logins
    {
        public int LogId { get; set; }
        public string Password { get; set; }


        public Logins() { }
        public Logins(int logId, string password)
        {
            this.LogId = logId;
            this.Password = password;
        }
        public void EsValido() { }
    }
}

