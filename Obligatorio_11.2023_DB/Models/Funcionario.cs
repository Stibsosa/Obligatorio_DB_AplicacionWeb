namespace Obligatorio_11._2023_DB.Models
{
    public class Funcionario
    {
        public int Ci { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nac { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public string LogId { get; set; }

        public Funcionario() { }
        public Funcionario(int ci, string nombre, string apellido, DateTime fecha_Nac, string direccion, int telefono, string email, string logId)
        {
            Ci = ci;
            Nombre = nombre;
            Apellido = apellido;
            Fecha_Nac = fecha_Nac;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            LogId = logId;
        }
    }
}
