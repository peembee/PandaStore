﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;


namespace PandaStore.Models
{
    public class PandaUser : IdentityUser
    {
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}<br/>{Email}";


        [Required]
        [StringLength(50)]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [DisplayName("Efternamn")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DisplayName("Registrerings datum")]
        public DateTime RegistreredDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(70)]
        public string Adress { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [DisplayName("Stad")]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(5)]
        [DisplayName("Postnummer")]
        public string ZipCode { get; set; } = string.Empty;


        [StringLength(50)]
        [DisplayName("Admin")]
        public bool? isAdmin { get; set; } = false;

        public virtual ICollection<CustomerProduct>? CustomerProducts { get; set; }
        public virtual ICollection<Campaign>? Campaigns { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
