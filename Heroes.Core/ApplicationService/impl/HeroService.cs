using Heroes.Core.DomainService;
using Heroes.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Heroes.Core.ApplicationService.impl
{
 public class HeroService : IHeroService
    {
        private readonly IHeroRepository _heroRepository;

        public HeroService(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public Hero CreateHero(Hero createdHero)
        {
            if (string.IsNullOrEmpty(createdHero.Name))
                throw new InvalidDataException("Hero must have a Name");

            return _heroRepository.CreateHero(createdHero);
        }

        public Hero GetHeroById(int id)
        {
            if (_heroRepository.GetHeroById(id) == null)
                throw new InvalidDataException("Can't find Hero with the id: " + id);
            return _heroRepository.GetHeroById(id);
        }

        public List<Hero> GetAllHeroes()
        {
            return _heroRepository.GetAllHeroes();
        }

        public Hero UpdateHero(Hero updatedHero)
        {
            return _heroRepository.UpdateHero(updatedHero);
        }

        public Hero DeleteHero(int id)
        {
            if (_heroRepository.DeleteHero(id) == null)
                throw new InvalidDataException("Can't delete Hero with the id: " + id);
            return _heroRepository.DeleteHero(id);
        }
    }
}
