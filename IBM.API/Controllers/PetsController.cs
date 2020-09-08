using System;
using System.Collections.Generic;
using Heroes.Core.ApplicationService;
using Heroes.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }
        // GET: <PetsController>
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAllPets()
        {
            return _petService.GetAllPets();
        }

        // GET <PetsController>/5
        [HttpGet("{id}")]
        public ActionResult<Pet> GetPetById(int id)
        {
            return _petService.GetPetById(id);
        }

        // POST <PetsController>
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            try
            {
                return Ok(_petService.CreatePet(pet));
            }
            catch (Exception e)
            {
                return BadRequest("cannot create pet sry!" + e.Message);
            }
        }

        // PUT <PetsController>/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            if (id < 1 || id != pet.Id)
            {
                return BadRequest("parameter id and pet.id must be the same");
            }
            else
            {
                return Ok(_petService.UpdatePet(pet));
            }
        }

        // DELETE <PetsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            var pet = _petService.DeletePet(id);
            if (pet == null)
            {
                return StatusCode(404, "Did not find any pet with id " + id);
            }

            return NoContent();
        }
    }
}
