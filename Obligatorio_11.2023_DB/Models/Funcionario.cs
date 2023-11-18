namespace Obligatorio_11._2023_DB.Models
{
    public class Funcionario
    {
        public int Ci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string direccion { get; set; }
        public int Telefono { get; set; }
        public string email { get; set; }
        public string LogId { get; set; }

        public Funcionario() { }
        public Funcionario(int ci, string nombre, string apellido, DateTime fecha_Nac, string direccion, int telefono, string email, string logId)
        {
            this.Ci = ci;
            this.nombre = nombre;
            this.apellido = apellido;
            this.fecha_nacimiento = fecha_Nac;
            this.direccion = direccion;
            this.Telefono = telefono;
            this.email = email;
            this.LogId = logId;
        }
        public void EsValido() { }
    }
}
