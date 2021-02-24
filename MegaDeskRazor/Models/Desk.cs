using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class Desk
    {
        public int ID { get; set; }

        [Display(Name = "Customer Name")]
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string customerName { get; set; }

        [Display(Name = "Desk Width")]
        [Range(24, 96)]
        [Required]
        public double width { get; set; }

        [Display(Name = "Desk Depth")]
        [Range(12, 48)]
        [Required]
        public double depth { get; set; }

        [Display(Name = "Number of Drawers")]
        [Range(0, 7)]
        [Required]
        public int numberOfDrawers { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal price { get; set; }

        [Display(Name = "Surface Material")]
        [Required]
        public string surfaceMaterial { get; set; }

        [Display(Name = "Rush Order")]
        [Required]
        public string rushOrder { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
