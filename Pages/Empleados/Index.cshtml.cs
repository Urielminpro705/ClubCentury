using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Empleados
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

        public PaginatedList<Empleado> Empleados { get; set; }

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

            IQueryable<Empleado> empleadosIQ = from s in _context.Empleados
                                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                empleadosIQ = empleadosIQ.Where(s => s.Nombre.Contains(searchString)
                                       || s.Apellido_Paterno.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    empleadosIQ = empleadosIQ.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    empleadosIQ = empleadosIQ.OrderBy(s => s.Apellido_Paterno);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Empleados = await PaginatedList<Empleado>.CreateAsync(
                empleadosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
