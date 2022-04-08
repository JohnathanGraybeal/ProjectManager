using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.Entities
{
    /// <summary>
    /// Model for user roles
    /// </summary>
    public class AppRole
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }
        public List<ProjectRole> ProjectRoles { get; set; }
    }
}
