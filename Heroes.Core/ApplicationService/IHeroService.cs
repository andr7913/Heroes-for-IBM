using Heroes.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.ApplicationService
{
  public  interface IHeroService
    {
        Hero CreateHero(Hero createdHero);

        Hero GetHeroById(int id);

        List<Hero> GetAllHeroes();

        Hero UpdateHero(Hero updatedHero);

        Hero DeleteHero(int id);
    }
}
