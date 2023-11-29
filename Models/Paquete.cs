namespace ClubCentury.Models
{
    public class Paquete
    {
        public int PaqueteID { get; set; }
        public string Nombre { get; set; }
        public double Costo { get; set; }
        public int Duracion { get; set; }
        public string Privilegios { get; set; }
        public ICollection<Socio> Socios { get; set; }
    }
}