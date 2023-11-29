using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubCentury.Models
{

    public class Inscripcion
    {
        public int InscripcionID { get; set; }
        [ForeignKey("Socio")]
        public int SocioID { get; set; }
        [ForeignKey("Taller")]
        public int TallerID { get; set; }

        public Taller Taller { get; set; }
        public Socio Socio { get; set; }
    }
}