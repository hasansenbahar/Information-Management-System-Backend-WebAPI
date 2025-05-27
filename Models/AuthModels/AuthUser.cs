﻿using System.ComponentModel.DataAnnotations;

namespace WebService.API.Models.AuthModels
{
    public class AuthUser
    {
        [Required]
        [StringLength(50)]
        public string? UserName { get; set; }
        

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

    }
}
