using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class UserWedding 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserWeddingId { get; set; }

        public int UserId { get; set; }
        public RegisterUser User { get; set; }

        public int WeddingId { get; set; }
        public Wedding Wedding { get; set; }

    }
}