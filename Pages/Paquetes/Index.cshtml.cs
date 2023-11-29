using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClubCentury.Data;
using ClubCentury.Models;

namespace ClubCentury.Pages.Paquetes
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

        public PaginatedList<Paquete> Paquetes { get; set; }

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

            IQueryable<Paquete> paquetesIQ = from s in _context.Paquetes
                                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                paquetesIQ = paquetesIQ.Where(s => s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    paquetesIQ = paquetesIQ.OrderByDescending(s => s.Nombre);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Paquetes = await PaginatedList<Paquete>.CreateAsync(
                paquetesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
