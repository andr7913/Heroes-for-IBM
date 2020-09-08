using Heroes.Entity;
using System.Collections.Generic;
using System.Linq;
using Heroes.Core.DomainService;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.SQL.repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly DatabaseContext _dbContext;

        public HeroRepository(DatabaseContext context)
        {
            _dbContext = context;
        }

        public Hero CreateHero(Hero createdHero)
        {
            _dbContext.Attach(createdHero).State = EntityState.Added;
            _dbContext.SaveChanges();
            return createdHero;
        }

        public List<Hero> GetAllHeroes()
        {
            return _dbContext.Heroes.AsNoTracking().ToList();
        }

        public Hero GetHeroById(int id)
        {
            return _dbContext.Heroes.AsNoTracking().FirstOrDefault(h => h.HeroId == id);
        }

        public Hero UpdateHero(Hero updatedHero)
        {
            _dbContext.Attach(updatedHero).State = EntityState.Modified;
            _dbContext.Update(updatedHero);
            _dbContext.SaveChanges();
            return updatedHero;
        }

        public Hero DeleteHero(int id)
        {
            var heroToDelete = _dbContext.Remove(new Hero { HeroId = id }).Entity;
            _dbContext.SaveChanges();
            return heroToDelete;
        }
    }
}