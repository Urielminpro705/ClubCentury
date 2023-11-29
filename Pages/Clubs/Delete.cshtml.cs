using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Clubs
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
        public Club Club { get; set; } = default!;
        public string ErrorMessage { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club = await _context.Clubs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ClubID == id);

            if (Club == null)
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

            var club = await _context.Clubs.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            try
            {
                _context.Clubs.Remove(club);
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
