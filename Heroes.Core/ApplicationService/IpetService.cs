using Heroes.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.ApplicationService
{
  public  interface IPetService
    {
        Pet CreatePet(Pet createdPet);

        Pet GetPetById(int id);

        Pet DeletePet(int id);

        List<Pet> GetAllPets();

        Pet UpdatePet(Pet updatedPet);
    }
}
