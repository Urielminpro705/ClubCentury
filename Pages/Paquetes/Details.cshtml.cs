using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Paquetes
{
    public class DetailsModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public DetailsModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        public Paquete Paquete { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquetes
                .Include(s => s.Socios)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PaqueteID == id);
            if (paquete == null)
            {
                return NotFound();
            }
            else
            {
                Paquete = paquete;
            }
            return Page();
        }
    }
}
