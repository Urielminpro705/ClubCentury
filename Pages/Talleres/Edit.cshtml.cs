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

namespace ClubCentury.Pages.Talleres
{
    public class EditModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        public EditModel(ClubCentury.Data.ClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Taller Taller { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taller =  await _context.Talleres.FirstOrDefaultAsync(m => m.TallerID == id);
            if (taller == null)
            {
                return NotFound();
            }
            Taller = taller;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Taller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TallerExists(Taller.TallerID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TallerExists(int id)
        {
            return _context.Talleres.Any(e => e.TallerID == id);
        }
    }
}
