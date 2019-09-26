﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        
        public string CardNumber { get; set; }
        [Required]
        public string SecurityNumber { get; set; }
        [Required]
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string Expiration { get; set; }
       
        public string CardHolderName { get; set; }

        //public int CardType { get; set; }
       
        public string Street { get; set; }
        
        public string City { get; set; }
      
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        
        public string ZipCode { get; set; }
      
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        
    }
}