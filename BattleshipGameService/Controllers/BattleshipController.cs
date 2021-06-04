using BattleshipGameService.Models;
using BattleshipGameService.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BattleshipGameService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleshipController : ControllerBase
    {
        private readonly IBattleshipRespository _repository;
        private Ships enimieas;

        public BattleshipController(IBattleshipRespository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            
        }

        // GET: api/<BattleshipController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BattleshipController>/5
        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Ships), (int)HttpStatusCode.OK)]
        public int[] GetEnimies()
        {
            enimieas = new Ships();
            int[] res = _repository.GenerateEnimy();

            return res;
        }

        // POST api/<BattleshipController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BattleshipController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BattleshipController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
