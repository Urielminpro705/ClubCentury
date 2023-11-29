using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Socios
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
            ViewData["ClubID"] = new SelectList(_context.Clubs, "ClubID", "ClubID");
            ViewData["PaqueteID"] = new SelectList(_context.Paquetes, "PaqueteID", "PaqueteID");
            return Page();
        }

        [BindProperty]
        public Socio Socio { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptySocio = new Socio();

            if (await TryUpdateModelAsync<Socio>(
                emptySocio,
                "socio",   // Prefix for form value.
                s => s.Nombre, s => s.Apellido_Paterno, s => s.Apellido_Materno, s => s.Curp, s => s.Domicilio, s => s.Telefono,s => s.Correo, s => s.ClubID, s => s.PaqueteID))
            {
                _context.Socios.Add(emptySocio);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
