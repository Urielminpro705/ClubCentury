using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Empleados
{
    public class DetailsModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public DetailsModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        public Empleado Empleado { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(s => s.Club)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EmpleadoID == id);
            if (empleado == null)
            {
                return NotFound();
            }
            else
            {
                Empleado = empleado;
            }
            return Page();
        }
    }
}
