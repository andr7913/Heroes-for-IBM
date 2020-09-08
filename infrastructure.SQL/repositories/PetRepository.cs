using System.Collections.Generic;
using System.Linq;
using Heroes.Core.DomainService;
using Heroes.Entity;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.SQL.repositories
{
    public class PetRepository : IPetRepository
    {


        private readonly DatabaseContext _context;

        public PetRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Pet CreatePet(Pet createdPet)
        {
            if (createdPet.Hero != null)
            {
                _context.Attach(createdPet.Hero).State = EntityState.Unchanged;
            }

            var savedPet = _context.Add(createdPet).Entity;
            createdPet.Hero.Pets.Add(savedPet);
            _context.SaveChanges();
            return savedPet;
        }

        public Pet DeletePet(int id)
        {
            var petToRemove = _context.Remove(new Pet { Id = id }).Entity;
            _context.SaveChanges();
            return petToRemove;
        }

        public List<Pet> GetAllPet()
        {
            return _context.Pets.AsNoTracking().ToList();
        }

        public Pet GetPetById(int id)
        {
            return _context.Pets.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            _context.Attach(updatedPet).State = EntityState.Modified;
            _context.Update(updatedPet);
            _context.SaveChanges();
            return updatedPet;
        }

    }
}