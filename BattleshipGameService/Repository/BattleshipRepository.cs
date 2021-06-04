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
        private Ships enimy;
        private Random random;
        private int[,] grid = new int[10, 10];
        private bool[,] shipSet = new bool[10, 10];
        private int[] positiosn= new int[6];
        private int count = 0;
        private int value;

        public BattleshipRepository()
        {
            random = new Random();
        }

        public int[] GenerateEnimy()
        {
            enimy = new Ships();
            GenerateGrid();
            GenerateEnimiShips();
            AssignBattleshipShip();
            //SetDistroyers();
            enimy.Grid = positiosn;
            //enimy.ShipsGrid = shipSet;

            return enimy.Grid;
        }

        public Ships KillEnimy(Ships enimy, int value)
        {
            throw new NotImplementedException();
        }

        public Ships KilUser(Ships User)
        {
            throw new NotImplementedException();
        }

        // Generate 10x10 Grid
        private void GenerateGrid()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    count++;
                    grid[r, c] = count;
                }
            }

            count = 0;
        }

        //Generate enimiies ship list (make all false)
        private void GenerateEnimiShips()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                { 
                    shipSet[r, c] = false;
                }
            }
        }

        // locate enimies BattleshipShip 
        private void AssignBattleshipShip()
        {
            int number = random.Next(100);
            
            int coun;
            int round = 1;
            int res;

            bool state = false;
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    value = grid[r, c];

                    if (value == number)
                    {
                        state = shipSet[r, c];
                        if (state != true)
                        {
                            positiosn[0] = r;
                            if (c==0)
                            {
                                coun = c;

                                while (round < 6)
                                {
                                    shipSet[r, coun] = true;
                                    coun++;
                                    positiosn[round] = coun;
                                    round++;
                                }
                            }
                            else if (c==9)
                            {
                                coun = c;
                                while (round < 6)
                                {
                                    shipSet[r, coun] = true;
                                    coun--;
                                    positiosn[round] = coun;
                                    round++;
                                }
                            }
                            else {
                                int temp = c;
                                res = 10 - (++temp);
                                if (res >= 5)
                                {
                                    coun = c;
                                    while (round < 6)
                                    {
                                        shipSet[r, coun] = true;
                                        coun++;
                                        positiosn[round] = coun;
                                        round++;
                                    }
                                }
                                else if (res < 5)
                                {
                                    coun = c;
                                    while (round < 6)
                                    {
                                        shipSet[r, coun] = true;
                                        coun--;
                                        positiosn[round] = coun;
                                        round++;
                                    }
                                }
                            }
                            round = 0;
                        }
                        else {
                            break;
                        }
                    }
                }
            }

            //return shipSet;
        }

        private void SetDistroyers(int[] slots)
        {
            int counter=0;
            int number;
            while (counter < 2)
            {
                number = random.Next(100);
                for (int r = 0; r < 10; r++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        value = grid[r, c];
                        if (value == number)
                        {
                            
                        }

                    }
                }
            }

            
        }

        public int[] GetDistroyers()
        {
            throw new NotImplementedException();
        }
    }
}
