using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FirstClassAPI.Models;

namespace FirstClassAPI.Controllers
{
    [Route("api/[controller]")]
    public class ScoutsController : Controller
    {
        private readonly IScoutRepository _scoutRepository;

        public ScoutsController(IScoutRepository _scoutRepository) {
            this._scoutRepository = _scoutRepository;
        }

        // GET api/scouts
        [HttpGet]
        public IEnumerable<Scout> GetAll()
        {
            return _scoutRepository.GetAll();
        }

        // GET api/scouts/5
        [HttpGet("{id}", Name = "GetScout")]
        public IActionResult GetById(int id)
        {
            var scout = _scoutRepository.Find(id);
            if (scout == null)
            {
                return NotFound();
            }
            return new ObjectResult(scout);
        }

        // POST api/scouts
        [HttpPost]
        public IActionResult Create([FromBody]Scout scout)
        {
            if (scout == null)
            {
                return BadRequest();
            }

            _scoutRepository.Add(scout);

            return CreatedAtRoute("GetScout", new { id = scout.Key }, scout);
        }

        // PUT api/scouts/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]Scout item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var scout = _scoutRepository.Find(id);
            if (scout == null)
            {
                return NotFound();
            }

            scout.FirstName = item.FirstName;
            scout.LastName = item.LastName;
            scout.Rank = item.Rank;

            _scoutRepository.Update(scout);
            return new NoContentResult();
        }

        // DELETE api/scouts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var scout = _scoutRepository.Find(id);
            if (scout == null)
            {
                return NotFound();
            }

            _scoutRepository.Remove(id);
            return new NoContentResult();
        }
    }
}
