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

namespace ClubCentury.Pages.Clubs
{
    public class EditModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public EditModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Club Club { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club = await _context.Clubs.FindAsync(id);

            if (Club == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var clubToUpdate = await _context.Clubs.FindAsync(id);

            if (clubToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Club>(
                clubToUpdate,
                "club",
                s => s.Direccion, s => s.Cp, s => s.Telefono, s => s.Correo))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool ClubExists(int id)
        {
            return _context.Clubs.Any(e => e.ClubID == id);
        }
    }
}
