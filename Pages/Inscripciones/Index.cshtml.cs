using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Inscripciones
{
    public class IndexModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;
        private readonly IConfiguration Configuration;
        public IndexModel(ClubContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Inscripcion> Inscripciones { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Inscripcion> inscripcionesIQ = from s in _context.Inscripciones.Include(i => i.Socio)
                                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                inscripcionesIQ = inscripcionesIQ.Where(s => s.Socio.Nombre.Contains(searchString)
                                       || s.Socio.Apellido_Paterno.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    inscripcionesIQ = inscripcionesIQ.OrderByDescending(s => s.Socio.Nombre);
                    break;
                default:
                    inscripcionesIQ = inscripcionesIQ.OrderBy(s => s.InscripcionID);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Inscripciones = await PaginatedList<Inscripcion>.CreateAsync(
                inscripcionesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
