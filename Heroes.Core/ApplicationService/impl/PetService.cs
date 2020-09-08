using Heroes.Core.DomainService;
using Heroes.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Heroes.Core.ApplicationService.impl
{
   public class PetService : IPetService
    {

        private readonly IPetRepository _petRepository;
        private readonly IHeroRepository _heroRepository;

        public PetService(IPetRepository petRepo, IHeroRepository heroRepository)
        {
            _petRepository = petRepo;
            _heroRepository = heroRepository;
        }
        public Pet CreatePet(Pet createdPet)
        {
            if (string.IsNullOrEmpty(createdPet.Name))
                throw new InvalidDataException("Pet must have a Name");

            if (string.IsNullOrEmpty(createdPet.Race))
                throw new InvalidDataException("Pet must have a Race");

            if (_heroRepository.GetHeroById(createdPet.Hero.HeroId) == null)
                throw new InvalidDataException("Can't find Hero with the chosen Id");

            return _petRepository.CreatePet(createdPet);
        }

        public Pet DeletePet(int id)
        {
            if (_petRepository.DeletePet(id) == null)
                throw new InvalidDataException("No pet with id: " + id + " exist");
            return _petRepository.DeletePet(id);
        }

        public List<Pet> GetAllPets()
        {
            return _petRepository.GetAllPet();
        }

        public Pet GetPetById(int id)
        {
            if (_petRepository.GetPetById(id) == null)
                throw new InvalidDataException("Can't find pet with the id: " + id);
            return _petRepository.GetPetById(id);
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            return _petRepository.UpdatePet(updatedPet);
        }
    }
}
