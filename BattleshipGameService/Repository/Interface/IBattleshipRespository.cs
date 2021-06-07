using BattleshipGameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameService.Repository.Interface
{
    public interface IBattleshipRespository
    {
        Ships GenerateEnemy();
        Ships GeneratePlayer(Ships player);
        ResponsBody KilUser(Ships User);
        ResponsBody KillEnemy(Ships enimy,int value);
    }
}
