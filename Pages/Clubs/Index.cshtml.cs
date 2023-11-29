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
    public class IndexModel : PageModel
    {
        private readonly ClubCentury.Data.ClubContext _context;

        private readonly IConfiguration Configuration;
        public IndexModel(ClubContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string DireccionSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Club> Clubs { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            DireccionSort = String.IsNullOrEmpty(sortOrder) ? "dic_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Club> clubsIQ = from s in _context.Clubs
                                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                clubsIQ = clubsIQ.Where(s => s.Direccion.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "dic_desc":
                    clubsIQ = clubsIQ.OrderByDescending(s => s.Direccion);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Clubs = await PaginatedList<Club>.CreateAsync(
                clubsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
