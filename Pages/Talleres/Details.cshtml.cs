using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Talleres
{
    public class DetailsModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public DetailsModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        public Taller Taller { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taller = await _context.Talleres.FirstOrDefaultAsync(m => m.TallerID == id);
            if (taller == null)
            {
                return NotFound();
            }
            else
            {
                Taller = taller;
            }
            return Page();
        }
    }
}
