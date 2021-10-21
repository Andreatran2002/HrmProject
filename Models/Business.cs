using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models{
    public class Business{
        [Display(Name="Mã nhân viên")]
        public string EmployeeId { get; set; }
        public AppUser employee { get; set; }

        [Display(Name="Mã công việc")]
        public string JobId { get; set; }
        [ForeignKey("EmployeeId")]
        public Job job { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name="Ngày nhậm chức")]
        public DateTime Take_office_date { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Kết thúc nhậm chức")]
        public DateTime? Take_office_date_end { get; set; }

        

    }
}