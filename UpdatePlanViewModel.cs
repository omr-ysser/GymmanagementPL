using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.ViewModels.PlanViewModel
{
    public class UpdatePlanViewModel
    {
        public string PlanName { get; set; } = null!;


        [Required(ErrorMessage = "Description Name Is Required")]
        [StringLength(50,MinimumLength = 5 , ErrorMessage = "Description Must Be between 5 and 50 Char")]
        public string Description { get; set; } = null!;


        [Required(ErrorMessage = "Duration Days Name Is Required")]
        [Range(1,365,ErrorMessage = "Duration Days between 1 and 365")]
        public int DurationDays { get; set; }


        [Required(ErrorMessage = "Price Is Required")]
        [Range(0.1, 10000, ErrorMessage = "Price between 0.1 and 10000")]
        public decimal Price { get; set; }
    }
}
