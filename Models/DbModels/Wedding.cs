using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Wedding : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int WeddingId { get; set; }
        public int CreatedBy { get; set; }
 
        public string Wedder1 { get; set; }

        public string Wedder2 { get; set; }

        public DateTime Date { get; set; }
        public string Address { get; set; }

        public List<UserWedding> UserWedding { get; set; }

        public Wedding()
        {
            UserWedding = new List<UserWedding>();
        }

    }
}