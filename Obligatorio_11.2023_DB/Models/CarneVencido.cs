namespace Obligatorio_11._2023_DB.Models
{
    public class CarneVencido
    {
        public DateTime Fch_Vencimiento { get; set; }
        public int Ci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int Telefono { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }

        public CarneVencido() { }
        public CarneVencido(DateTime fch_Vencimiento, int ci, string nombre, string apellido, int telefono, string direccion, string email)
        {
            this.Fch_Vencimiento = fch_Vencimiento;
            this.Ci = ci;
            this.nombre = nombre;
            this.apellido = apellido;
            this.Telefono = telefono;
            this.direccion = direccion;
            this.email = email;
        }
        public void EsValido() { }
    }
}
