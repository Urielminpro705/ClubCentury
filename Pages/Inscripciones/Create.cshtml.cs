using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClubCentury.Data;
using ClubCentury.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubCentury.Pages.Inscripciones
{
    public class CreateModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public CreateModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["SocioID"] = new SelectList(_context.Socios, "SocioID", "SocioID");
            ViewData["TallerID"] = new SelectList(_context.Talleres, "TallerID", "TallerID");
            return Page();
        }

        [BindProperty]
        public Inscripcion Inscripcion { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyInscripcion = new Inscripcion();

            if (await TryUpdateModelAsync<Inscripcion>(
                emptyInscripcion,
                "inscripcion",   // Prefix for form value.
                s => s.SocioID, s => s.TallerID))
            {
                try
                {
                    _context.Inscripciones.Add(emptyInscripcion);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateException ex)
                {
                    // Aquí puedes imprimir información detallada sobre la excepción.
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException?.Message);
                    throw; // Re-levanta la excepción para que se maneje en niveles superiores si es necesario.
                }
            }

            return Page();
        }
    }
}
