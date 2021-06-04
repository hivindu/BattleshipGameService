using BattleshipGameService.Models;
using BattleshipGameService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameService.Repository
{
    public class BattleshipRepository : IBattleshipRespository
    {
        public Task<Ships> GenerateEnimy()
        {
            throw new NotImplementedException();
        }

        public Task<Ships> KillEnimy(Ships enimy, int value)
        {
            throw new NotImplementedException();
        }

        public Task<Ships> KilUser(Ships User)
        {
            throw new NotImplementedException();
        }
    }
}
