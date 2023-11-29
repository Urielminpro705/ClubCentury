using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Socios
{
    public class IndexModel : PageModel
    {
        private readonly ClubContext _context;
        private readonly IConfiguration Configuration;
        public IndexModel(ClubContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Socio> Socios { get; set; }

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

            IQueryable<Socio> sociosIQ = from s in _context.Socios
                                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                sociosIQ = sociosIQ.Where(s => s.Nombre.Contains(searchString)
                                       || s.Apellido_Paterno.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sociosIQ = sociosIQ.OrderByDescending(s => s.Nombre);
                    break;
                default:
                    sociosIQ = sociosIQ.OrderBy(s => s.Apellido_Paterno);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Socios = await PaginatedList<Socio>.CreateAsync(
                sociosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}