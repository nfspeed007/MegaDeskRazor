using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDeskRazor.Data;
using MegaDeskRazor.Models;

namespace MegaDeskRazor.Pages.DeskQuotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDeskRazor.Data.MegaDeskRazorContext _context;

        public CreateModel(MegaDeskRazor.Data.MegaDeskRazorContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Desk Desk { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //get current date for date
            Desk.DateAdded = DateTime.Today;

            //add the bace things for calc
            var materials = 0; ;
            var orderRush = 0;
            var basePrice = 200;
            var material = Request.Form["surfaceMaterial"];


            // Calc the area and drawer price
            var area = Desk.width * Desk.depth;
            var drawers = Desk.numberOfDrawers * 50;
            

            // Materials calc
            if (material == "Laminate")
                materials = 100;
            else if (material == "Oak")
                materials = 200;
            else if (material == "Pine")
                materials = 50;
            else if (material == "Rosewood")
                materials = 300;
            else if (material == "Veneer")
                materials = 125;


            // rush level calc
            int rushLevel = Convert.ToInt32(Desk.rushOrder);
            
            if (rushLevel == 3 && area < 1000)
                orderRush = 60;
            else if (rushLevel == 5 && area < 1000)
                orderRush = 40;
            else if (rushLevel == 7 && area < 1000)
                orderRush = 30;

            if (rushLevel == 3 && (area >= 1000 && area <= 2000))
                orderRush = 70;
            else if (rushLevel == 5 && (area >= 1000 && area <= 2000))
                orderRush = 50;
            else if (rushLevel == 7 && (area >= 1000 && area <= 2000))
                orderRush = 35;

            if (rushLevel == 3 && area > 2000)
                orderRush = 80;
            else if (rushLevel == 5 && area > 2000)
                orderRush = 60;
            else if (rushLevel == 7 && area > 2000)
                orderRush = 40;

            //Calc the final price
            Desk.price = Convert.ToDecimal(basePrice + area + drawers + materials + orderRush);

            //send desk to the database
            _context.Desk.Add(Desk);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
