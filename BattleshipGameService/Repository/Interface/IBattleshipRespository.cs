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
        DistroyerShip GeneratePlayer(DistroyerShip player);
        ResponsBody KilUser(DistroyerShip User);
        ResponsBody KillEnemy(DistroyerShip enimy,int value);
    }
}
