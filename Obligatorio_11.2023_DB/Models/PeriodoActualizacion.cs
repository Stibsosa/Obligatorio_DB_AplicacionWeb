namespace Obligatorio_11._2023_DB.Models
{
    public class PeriodoActualizacion
    {
        public int Year { get; set; }
        public int Semestre { get; set; }
        public DateOnly Fch_Inicio { get; set; }
        public DateOnly Fch_Fin { get; set; }

        public PeriodoActualizacion() { }
    }
}
