namespace Obligatorio_11._2023_DB.Models
{
    public class PeriodoActualizacion
    {
        public int Year { get; set; }
        public int Semestre { get; set; }
        public DateTime Fch_Inicio { get; set; }
        public DateTime Fch_Fin { get; set; }

        public PeriodoActualizacion() { }
    }
}
