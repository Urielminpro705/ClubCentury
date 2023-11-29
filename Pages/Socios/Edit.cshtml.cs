using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Socios
{
    public class EditModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public EditModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Socio Socio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Socio = await _context.Socios.FindAsync(id);
            ViewData["ClubID"] = new SelectList(_context.Clubs, "ClubID", "ClubID");
            ViewData["PaqueteID"] = new SelectList(_context.Paquetes, "PaqueteID", "PaqueteID");

            if (Socio == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var socioToUpdate = await _context.Socios.FindAsync(id);

            if (socioToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Socio>(
                socioToUpdate,
                "socio",
                s => s.Nombre, s => s.Apellido_Paterno, s => s.Apellido_Materno, s => s.Curp, s => s.Domicilio, s => s.Telefono,s => s.Correo, s => s.ClubID, s => s.PaqueteID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool SocioExists(int id)
        {
            return _context.Socios.Any(e => e.SocioID == id);
        }
    }
}
