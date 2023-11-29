namespace ClubCentury.Models
{
    public class Club
    {
        public int ClubID { get; set; }
        public string Direccion { get; set; }
        public string Cp { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public ICollection<Empleado> Empleados { get; set; }
        public ICollection<Socio> Socios { get; set; }
    }
}