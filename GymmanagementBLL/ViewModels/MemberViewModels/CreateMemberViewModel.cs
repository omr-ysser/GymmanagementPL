using GymmanagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagementBLL.ViewModels.MemberViewModels
{
    internal class CreateMemberViewModel
    {
        //Omar Yasser
        [Required(ErrorMessage="Name Is Required")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Name Must be Between 2 and 50 char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public String Name { get; set; } = null!;
        [Required(ErrorMessage="Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100,MinimumLength=5,ErrorMessage ="Email Must be Less Than 100 char")]
        public String Email { get; set; } = null!;
        //01125987520
        [Required(ErrorMessage="Phone Is Required")]
        [Phone(ErrorMessage ="Invalid Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be Valid Egyption PhoneNumber.")]
        public String Phone { get; set; } = null!;
        [Required(ErrorMessage="Date Of Birth Is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage= "Gender Is Required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage ="Building Number Is Required")]
        [Range(1,1000,ErrorMessage ="Building Number Must be Between 1 And 1000")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage ="Street Is Required")]
        [StringLength(30,MinimumLength =2,ErrorMessage ="Street Must be Between 2 and 30 char")]
        public string Street { get; set; }= null!;
        [Required(ErrorMessage ="City Is Required")]
        [StringLength(30,MinimumLength =2,ErrorMessage ="City Must be Between 2 and 30 char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        public string city { get; set; }= null!;



    }
}
