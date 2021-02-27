using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazor.Data;
using MegaDeskRazor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace MegaDeskRazor.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskRazor.Data.MegaDeskRazorContext _context;

        public IndexModel(MegaDeskRazor.Data.MegaDeskRazorContext context)
        {
            _context = context;
        }

        public IList<Desk> Desk { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Dates { get; set; }
        [BindProperty(SupportsGet = true)]
        public string quoteDate { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Desk
                                            orderby m.DateAdded
                                            select Convert.ToString(m.DateAdded);

            var quotes = from m in _context.Desk
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                quotes = quotes.Where(s => s.customerName.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(quoteDate))
            {
                quotes = quotes.Where(x => Convert.ToString(x.DateAdded) == quoteDate);
            }
            Dates = new SelectList(await genreQuery.Distinct().ToListAsync());

            Desk = await quotes.ToListAsync();
        }
    }
}


