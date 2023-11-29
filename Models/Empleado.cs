using System.ComponentModel.DataAnnotations.Schema;
namespace ClubCentury.Models
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Curp { get; set; }
        public string Domicilio { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        [ForeignKey("Club")]
        public int ClubID { get; set; }
        public Club Club { get; set; }
    }
}