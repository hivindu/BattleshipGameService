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
        
        private Ships _destroyerShips;

        public BattleshipController(IBattleshipRespository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            
        }

        // GET api/<BattleshipController>/5
        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Ships), (int)HttpStatusCode.OK)]
        public Ships GetEnemies()
        {
            _destroyerShips = new Ships();
            _destroyerShips = _repository.GenerateEnemy();

            return _destroyerShips;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Ships), (int)HttpStatusCode.OK)]
        public Ships GenerateUserShips([FromBody] Ships player)
        {
            _destroyerShips = new Ships();
            if (player != null)
            {
                _destroyerShips = _repository.GeneratePlayer(player);
            }

            return _destroyerShips;
        }

        [HttpPut("[action]/{value}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponsBody), (int)HttpStatusCode.OK)]
        public ResponsBody ShotEnemy([FromBody] Ships destroyerShip,int value)
        {
            ResponsBody res = new ResponsBody();
            if (destroyerShip != null)
            {
                 res = _repository.KillEnemy(destroyerShip, value);
            }

            return res;
        }

        [HttpPut("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponsBody), (int)HttpStatusCode.OK)]
        public ResponsBody ShotOnUser([FromBody] Ships player)
        {
            ResponsBody res = new ResponsBody();
            if (player != null)
            {
                res = _repository.KilUser(player);
            }

            return res;
        }

        

    }
}
