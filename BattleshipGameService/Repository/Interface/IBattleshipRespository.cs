using BattleshipGameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameService.Repository.Interface
{
    public interface IBattleshipRespository
    {
        DistroyerShip GenerateEnimy();
        DistroyerShip GetDistroyers(int[] battleship);
        DistroyerShip KillEnimy(DistroyerShip enimy,int value);
        DistroyerShip KilUser(DistroyerShip User);
    }
}
