using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.Entities
{
    /// <summary>
    /// Intermediate table model for many to many relationships
    /// </summary>
    public class ProjectRole
    {
      
        public int Id { get; set; }
        [Required, Range(8.00, 100.00), DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int RoleId { get; set; }
        public AppRole AppRole { get; set; }
       
      

    }
}
