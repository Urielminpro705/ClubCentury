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

namespace ClubCentury.Pages.Inscripciones
{
    public class EditModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public EditModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inscripcion Inscripcion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inscripcion =  await _context.Inscripciones.FindAsync(id);
            ViewData["SocioID"] = new SelectList(_context.Socios, "SocioID", "SocioID");
            ViewData["TallerID"] = new SelectList(_context.Talleres, "TallerID", "TallerID");
            if (Inscripcion == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var inscripcionToUpdate = await _context.Inscripciones.FindAsync(id);

            if (inscripcionToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Inscripcion>(
                inscripcionToUpdate,
                "inscripcion",
                s => s.SocioID, s => s.TallerID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool InscripcionExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionID == id);
        }
    }
}
