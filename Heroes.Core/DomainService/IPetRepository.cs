using Heroes.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.DomainService
{
   public interface IPetRepository
    {
        Pet CreatePet(Pet createdPet);

        List<Pet> GetAllPet();

        Pet GetPetById(int id);

        Pet UpdatePet(Pet updatedPet);

        Pet DeletePet(int id);
    }
}
