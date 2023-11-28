namespace Obligatorio_11._2023_DB.Models
{
    public class Reservas_Disponibles
    {
        public int Id { get; set; }
        public DateTime Fch_Disponible { get; set; }

        public bool Ocupada { get; set; }

        public Reservas_Disponibles() { }
    }
}
