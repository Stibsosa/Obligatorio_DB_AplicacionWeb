namespace Obligatorio_11._2023_DB.Models
{
    public class CarneSalud
    {
        public int Ci { get; set; }
        public DateTime Fch_Emision { get; set; }
        public DateTime Fch_Vencimiento { get; set; }
        public byte[] Comprobante { get; set; }

        public CarneSalud() { }
    }
}
