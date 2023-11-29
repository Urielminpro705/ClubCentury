using System.ComponentModel.DataAnnotations.Schema;

namespace ClubCentury.Models
{
    public class Taller
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TallerID { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string Ubicacion { get; set; }
        public TimeOnly horario { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; }
    }
}