using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View Model used to assign a person a role
    /// </summary>
    public class AssignRoleVM
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        [Required, Range(8.00, 100.00)]
        public decimal HourlyRate { get; set; } = 8.00m;
    }
}
