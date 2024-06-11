using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Аккаунты
    {
        [Key]
        public int Код_аккаунта { get; set; } 

        [Required]
        [StringLength(255)]
        public string Логин { get; set; } 

        [Required]
        [StringLength(255)]
        public string Пароль { get; set; }
        [ForeignKey("Код_роли")]
        public Роли Роли { get; set; }
    }
}