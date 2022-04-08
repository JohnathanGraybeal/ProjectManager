using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// Interface for crud operations on people 
    /// </summary>
    public interface IPersonRepository
    {

        Task<Person> ReadAsync(int personId);
        Task<IEnumerable<Person>> ReadAllAsync();
        Task CreateAsync(Person person);
        Task UpdateAsync(int personId, Person person);
        Task DeleteAsync(int personId);
    }
}
