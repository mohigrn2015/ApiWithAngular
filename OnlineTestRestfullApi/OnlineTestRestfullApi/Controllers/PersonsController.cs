using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTestRestfullApi.DTO;
using OnlineTestRestfullApi.Models;

namespace OnlineTestRestfullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPersons()
        {
            var liat = (from p in _context.Persons
                        select new
                        {
                            p.PersonId,
                            p.FName,
                            p.LName,
                            p.Dob,
                            p.Address,
                            p.Email,
                            p.Phone
                        }).ToListAsync();
            return await liat;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPerson(int id)
        {
            var person = (from p in _context.Persons
                          where p.PersonId==id
                          select new
                          {
                              p.PersonId,
                              p.FName,
                              p.LName,
                              p.Dob,
                              p.Address,
                              p.Email,
                              p.Phone                              
                          }).ToListAsync();            

            if (person == null)
            {
                return NotFound();
            }

            return await person;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            Person p;

            if (id != person.PersonId)
            {
                return BadRequest();
            }

            if (person.PersonId !=0)
            {
                p = new Person();
                p.FName = person.FName;
                p.LName = person.LName;
                p.Dob = person.Dob;
                p.Address = person.Address;
                p.Email = person.Email;
                p.Phone = person.Phone;
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson([FromBody]Person person)
        {
            Person p;
            if (person.PersonId == 0)
            {
                p = new Person();
                p.FName = person.FName;
                p.LName = person.LName;
                p.Address = person.Address;
                p.Dob = person.Dob;
                p.Email = person.Email;
                p.Phone = person.Phone;

            }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }       
            
            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }
}
