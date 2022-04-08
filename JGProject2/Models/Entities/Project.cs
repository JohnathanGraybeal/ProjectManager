using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.Entities
{
    /// <summary>
    /// Model for project implements IValidatableObject for custom validation logic
    /// </summary>
    public class Project:IValidatableObject
    {
        
        public int Id { get; set; }
        [Required, MaxLength(30)]
        //project naem
        public string Name { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DueDate { get; set; }
        public List<ProjectRole> ProjectRoles { get; set; }

        public Project()
        {
            ProjectRoles = new List<ProjectRole>();
            
        }
        //https://stackoverflow.com/questions/21777412/mvc-model-validation-for-date/42036626#42036626
        /// <summary>
        /// Validates a startdate and duedate
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (StartDate < DateTime.Today)
            {

                results.Add(new ValidationResult("Start date must be greater than or equal to current date", new[] { "StartDate" }));
            }

            if (DueDate < StartDate)
            {
                results.Add(new ValidationResult("Due Date must be greater than Start Date", new[] { "DueDate" }));
            }

            

            return results;
        }

    }
}
