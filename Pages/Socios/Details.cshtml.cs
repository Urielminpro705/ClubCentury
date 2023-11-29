using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Socios
{
    public class DetailsModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public DetailsModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        public Socio Socio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .Include(s => s.Inscripciones)
                .ThenInclude(e => e.Taller)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SocioID == id);
            if (socio == null)
            {
                return NotFound();
            }
            else
            {
                Socio = socio;
            }
            return Page();
        }
    }
}
