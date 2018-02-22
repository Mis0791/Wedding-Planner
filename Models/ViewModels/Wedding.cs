using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "Wedder 1")]
        [MinLength(2)]
        public string Wedder1 { get; set; }
 
        [Required]
        [MinLength(2)]
        [Display(Name = "Wedder 2")]
        public string Wedder2 { get; set; }

        [Required]
        [Display(Name = "Date")]
        [MyDate(ErrorMessage = "Date must be in the future!")]
        public DateTime Date { get; set; }
 
        [Required]
        [Display(Name = "Wedding Address")]
        public string Address { get; set; }
    }
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }
}