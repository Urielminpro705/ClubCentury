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

namespace ClubCentury.Pages.Paquetes
{
    public class EditModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public EditModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Paquete Paquete { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Paquete = await _context.Paquetes.FindAsync(id);

            if (Paquete == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var paqueteToUpdate = await _context.Paquetes.FindAsync(id);

            if (paqueteToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Paquete>(
                paqueteToUpdate,
                "paquete",
                s => s.Nombre, s => s.Costo, s => s.Duracion, s => s.Privilegios))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool PaqueteExists(int id)
        {
            return _context.Paquetes.Any(e => e.PaqueteID == id);
        }
    }
}
