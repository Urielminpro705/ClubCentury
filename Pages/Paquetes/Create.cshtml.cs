using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Paquetes
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
            return Page();
        }

        [BindProperty]
        public Paquete Paquete { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyPaquete = new Paquete();

            if (await TryUpdateModelAsync<Paquete>(
                emptyPaquete,
                "paquete",   // Prefix for form value.
                s => s.Nombre, s => s.Costo, s => s.Duracion, s => s.Privilegios))
            {
                _context.Paquetes.Add(emptyPaquete);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
