﻿using System.ComponentModel.DataAnnotations;

namespace DevChallengeL1.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]        
        public string Name { get; set; }
    }
}