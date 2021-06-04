using BattleshipGameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameService.Repository.Interface
{
    public interface IBattleshipRespository
    {
        int[] GenerateEnimy();
        int[] GetDistroyers();
        Ships KillEnimy(Ships enimy,int value);
        Ships KilUser(Ships User);
    }
}
