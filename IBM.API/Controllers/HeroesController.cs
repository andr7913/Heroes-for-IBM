using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heroes.Core.ApplicationService;
using Heroes.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroService _heroService;
        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }
        // GET: <HeroesController>
        [HttpGet]
        public ActionResult<IEnumerable<Hero>> GetAllHeroes()
        {
            try
            {
                return Ok(_heroService.GetAllHeroes());
            }
            catch (Exception e)
            {
                return BadRequest("Did not find any Heroes" + e.Message);
            }
        }

        // GET <HeroesController>/5
        [HttpGet("{id}")]
        public ActionResult<Hero> GetHeroById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0");
            }

            return Ok(_heroService.GetHeroById(id));
        }

        // POST <HeroesController>
        [HttpPost]
        public ActionResult<Hero> Post([FromBody] Hero hero)
        {
            try
            {
                return Ok(_heroService.CreateHero(hero));
            }
            catch (Exception e)
            {
                return BadRequest("Cannot create Hero. Reason: " + e.Message);
            }
        }

        // PUT <HeroesController>/5
        [HttpPut("{id}")]
        public ActionResult<Hero> Put(int id, [FromBody] Hero hero)
        {
            if (id < 1 || id != hero.HeroId)
            {
                return BadRequest("Parameter Id and Hero Id must be the same");
            }

            return Ok(_heroService.UpdateHero(hero));
        }

        // DELETE <HeroesController>/5
        [HttpDelete("{id}")]
        public ActionResult<Hero> Delete(int id)
        {
            var hero = _heroService.DeleteHero(id);
            if (hero == null)
            {
                return StatusCode(404, "Did not find any Hero with Id" + id);
            }

            return NoContent();
        }
    }
}
