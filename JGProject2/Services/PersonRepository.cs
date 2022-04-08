using JGProject2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// CRUD operation on people
    /// </summary>
    public class PersonRepository:IPersonRepository
    {
        private ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Person person)
        {
            if (person != null)
            {
                await _db.People.AddAsync(person);
                await _db.SaveChangesAsync();

            }
        }

        public async Task DeleteAsync(int personId)
        {
            var person = await ReadAsync(personId);
            
            
           _db.People.Remove(person);
           await _db.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Person>> ReadAllAsync()
        {
            return await _db.People.ToListAsync();
        }

        public async Task<Person> ReadAsync(int personId)
        {
            return await _db.People.FirstOrDefaultAsync(p => p.Id == personId);
        }

        public async Task UpdateAsync(int personId, Person person)
        {
            var toUpdate = await ReadAsync(personId);
            if (person != null)
            {
                toUpdate.FirstName = person.FirstName;
                toUpdate.MiddleName = person.MiddleName;
                toUpdate.LastName = person.LastName;
                toUpdate.Email = person.Email;
               

                await _db.SaveChangesAsync();
            }
        }
    }
}
