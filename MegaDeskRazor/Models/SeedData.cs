using MegaDeskRazor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MegaDeskRazorContext(
                serviceProvider.GetRequiredService<DbContextOptions<MegaDeskRazorContext>>()))
            {
                // Look for any Quotes.
                if (context.Desk.Any())
                {
                    return;   // DB has been seeded
                }

                context.Desk.AddRange(
                    new Desk
                    {
                        customerName = "Bill Gates",
                        width = 30.00,
                        depth = 30.00,
                        numberOfDrawers = 2,
                        price = 1025.00M,
                        surfaceMaterial = "Rosewood",
                        rushOrder = "7",
                        DateAdded = DateTime.Parse("2021-02-25")
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
