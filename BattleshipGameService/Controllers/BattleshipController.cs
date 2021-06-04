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
        
        private DistroyerShip _distroyerShips;

        public BattleshipController(IBattleshipRespository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            
        }


        // GET api/<BattleshipController>/5
        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DistroyerShip), (int)HttpStatusCode.OK)]
        public DistroyerShip GetEnimies()
        {
            _distroyerShips = new DistroyerShip();
            _distroyerShips = _repository.GenerateEnimy();

            return _distroyerShips;
        }

        [HttpPut("[action]/distroyerShip/value")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DistroyerShip), (int)HttpStatusCode.OK)]
        public bool ShotEnimy(DistroyerShip distroyerShip,int value)
        {
            bool res = false;
            if (distroyerShip != null)
            {
                 res = _repository.KillEnimy(distroyerShip, value);
            }

            return res;
        }

        [HttpPut("[action]/player")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponsBody), (int)HttpStatusCode.OK)]
        public ResponsBody ShotOnUser(DistroyerShip player)
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
