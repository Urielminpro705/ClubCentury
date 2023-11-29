using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Clubs
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
        public Club Club { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyClub = new Club();

            if (await TryUpdateModelAsync<Club>(
                emptyClub,
                "club",   // Prefix for form value.
                s => s.Direccion, s => s.Cp, s => s.Telefono, s => s.Correo))
            {
                _context.Clubs.Add(emptyClub);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
