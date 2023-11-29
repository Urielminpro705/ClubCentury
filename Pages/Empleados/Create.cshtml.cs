using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Empleados
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
            return Page();
        }

        [BindProperty]
        public Empleado Empleado { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyEmpleado = new Empleado();

            if (await TryUpdateModelAsync<Empleado>(
                emptyEmpleado,
                "empleado",   // Prefix for form value.
                s => s.Nombre, s => s.Apellido_Paterno, s => s.Apellido_Materno, s => s.Curp, s => s.Domicilio, s => s.Telefono,s => s.Correo, s => s.ClubID))
            {
                _context.Empleados.Add(emptyEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
