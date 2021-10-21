using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    // [Table("Salarys")]
    public class Salary{
        [Key]
        [Display(Name="Bậc lương")]
        public int Salary_scale{ get; set; }
        
        [Required(ErrorMessage ="{0} phải nhập")]
        [Display(Name="Lương cơ bản")]
        [Column(TypeName = "nvarchar")]
        public int Basic_salary { get; set; }


        [Required]
        [Display(Name="Hệ số lương")]
        public int Coe_salary { get; set; }
        [Column(TypeName = "ntext")]
        [Display(Name="Nội dung")]
        public int Allowance_coe { get; set;}
        
        public string UserId {set; get;}
        [ForeignKey("UserId")]
        public AppUser User { get; set;}

    }
}