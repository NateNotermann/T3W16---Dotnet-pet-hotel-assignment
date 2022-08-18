using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets() {
            // return new List<Pet>();
            return _context.Pets.Include( pet => pet.petOwner );
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault( pet => pet.id == id );
            if ( pet is null )
            {
                return NotFound();
            }
            return pet;
        }

        [HttpPost]
       public Pet Post(Pet pet)
        {
            _context.Add(pet);
            _context.SaveChanges();
             StatusCode(201);
            return pet;
        }

        [HttpPut("{id}")]

         public Pet Put(int id, Pet pet)
        {
            pet.id = id;
            // telling the db context about our updated pet object
            _context.Update(pet);
        
            _context.SaveChanges();
            // respond back with created pet object
            return pet;
        }

        [HttpPut("{id}/checkin")]

         public Pet PutCheckIn(int id)
        {
            
            Pet pet = _context.Pets.SingleOrDefault( pet => pet.id == id );
            pet.checkedInAt = DateTime.Now;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }
      
       
        [HttpPut("{id}/checkout")]

         public Pet PutCheckOut(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault( pet => pet.id == id );
            pet.checkedInAt = null;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }

        
        [HttpDelete("{id}")]
          public ActionResult<Pet> Delete(int id)
        {
            // find the bread by id
            Pet pet = _context.Pets.Find(id);
            _context.Pets.Remove(pet);
            _context.SaveChanges();
            return StatusCode(204);
        }


        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }
    }
}
