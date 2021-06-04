using BattleshipGameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameService.Repository.Interface
{
    public interface IBattleshipRespository
    {
        Task<Ships> GenerateEnimy();
        Task<Ships> KillEnimy(Ships enimy,int value);
        Task<Ships> KilUser(Ships User);
    }
}
