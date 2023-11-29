using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Talleres
{
    public class DeleteModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ClubCentury.Data.ClubContext context,
                           ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Taller Taller { get; set; } = default!;
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Taller = await _context.Talleres
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TallerID == id);

            if (Taller == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taller = await _context.Talleres.FindAsync(id);

            if (taller == null)
            {
                return NotFound();
            }

            try
            {
                _context.Talleres.Remove(taller);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);

                return RedirectToAction("./Delete",
                                    new { id, saveChangesError = true });
            }
        }
    }
}
