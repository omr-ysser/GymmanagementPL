using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagementBLL.ViewModels.MemberViewModels
{
    internal class HealthRecordViewModels
    {
        [Required(ErrorMessage = "Height Is Required")]
        [Range(30, 300, ErrorMessage = "Height Must be Between 30 cm and 300 cm")]
        public decimal Height { get; set; }
    }
}
