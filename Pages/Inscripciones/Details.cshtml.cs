using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Inscripciones
{
    public class DetailsModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public DetailsModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        public Inscripcion Inscripcion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripciones
                .Include(s => s.Socio)
                .Include(s => s.Taller)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InscripcionID == id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            else
            {
                Inscripcion = inscripcion;
            }
            return Page();
        }
    }
}
