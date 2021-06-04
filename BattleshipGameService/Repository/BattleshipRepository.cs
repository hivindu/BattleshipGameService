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
        private Random random;
        private int[,] grid = new int[10, 10];
        private bool[,] shipSet = new bool[10, 10];
        private int[] positiosn= new int[6];
        private int[] dis1 = new int[4];
        private int[] dis2 = new int[4];
        private int count = 0;
        private int value;
        private bool state = false;
        private int temp;
        private int r1, r2, c1, c2;
        private bool res = false;
        DistroyerShip disShip;

        public BattleshipRepository()
        {
            random = new Random();
            GenerateGrid();
            GenerateEnimiShips();
        }

        public DistroyerShip GenerateEnimy()
        {
            disShip = new DistroyerShip();
            AssignBattleshipShip();
            SetDistroyers();
            disShip.Battleship = positiosn;
            disShip.Ship1 = dis1;
            disShip.Ship2 = dis2;

            return disShip;
        }


        public bool KillEnimy(DistroyerShip enimy, int value)
        {
            disShip = enimy;

            positiosn = disShip.Battleship;
            dis1 = disShip.Ship1;
            dis2 = disShip.Ship2;

            temp = positiosn[0];

            for (int i = 1; i < 6; i++)
            {
                shipSet[temp, i] = true;
            }

            r1 = dis1[0];
            c1 = dis1[1];
            r2 = dis1[2];
            c2 = dis1[3];

            shipSet[r1, c1] = true;
            shipSet[r2, c2] = true;

            r1 = dis2[0];
            c1 = dis2[1];
            r2 = dis2[2];
            c2 = dis2[3];

            shipSet[r1, c1] = true;
            shipSet[r2, c2] = true;

            res = HitMiss(value);

            return res;
        }

        public DistroyerShip KilUser(DistroyerShip User)
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

            
        }

        //Generate locations od Distroyerships 
        private void SetDistroyers()
        {
            int counter=0;
            int number;
            int row ;
            int column;

            while (counter < 2)
            {
                number = random.Next(100);
                for (int r = 0; r < 10; r++)
                {
                    for (int c = 0; c < 10; c++)
                    {
                        temp = c;
                        row = r;
                        value = grid[r, c];
                        if (value == number)
                        {
                            state = shipSet[r, c];
                            if (state!=true)
                            {
                                if (c == 9)
                                {
                                    temp--;
                                }
                                else if (c == 0)
                                {
                                    temp++;
                                }
                                else {
                                    temp++;
                                }
                                
                                state = shipSet[r, temp];
                                if (state != true)
                                {
                                    shipSet[r, c] = true;
                                    shipSet[r, temp] = true;
                                    r1 = r;
                                    c1 = c;
                                    r2 = r;
                                    c2 = temp;
                                }
                                else
                                {
                                    if(0<c && c<9)
                                    { 
                                    temp = temp - 2;
                                    state = shipSet[r, temp];
                                    if (state != true)
                                    {
                                        shipSet[r, c] = true;
                                        shipSet[r, temp] = true;
                                        r1 = r;
                                        c1 = c;
                                        r2 = r;
                                        c2 = temp;
                                    } 
                                    else if (row != 0)
                                    {
                                        row--;
                                        state = shipSet[row, c];
                                        if (state != true)
                                        {
                                            shipSet[r, c] = true;
                                            shipSet[row, c] = true;

                                            r1 = r;
                                            c1 = c;
                                            r2 = row;
                                            c2 = c;
                                        }
                                        else {
                                            row = row + 2;
                                            state = shipSet[row, c];
                                            if (state != true)
                                            {
                                                shipSet[r, c] = true;
                                                shipSet[row, c] = true;
                                                r1 = r;
                                                c1 = c;
                                                r2 = row;
                                                c2 = c;
                                            }
                                        }
                                    }
                                    }
                                }
                                switch (counter)
                                {
                                    case 0:
                                        dis1[0] = r1;
                                        dis1[1] = c1;
                                        dis1[2] = r2;
                                        dis1[3] = c2; break;
                                    case 1:
                                        dis2[0] = r1;
                                        dis2[1] = c1;
                                        dis2[2] = r2;
                                        dis2[3] = c2; break;
                                }
                                counter++;
                            }// end of checking status condition

                        }// end first If condition

                    }// end of second for loop
                }// end of first for loop
            }

        }

        //Check hit true or false
        private bool HitMiss(int index)
        {
            bool final = false;
            bool item = false;
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    value = grid[r, c];
                    if (value == index)
                    {
                        item = shipSet[r, c];
                        if (item)
                        {
                            shipSet[r, c] = false;
                            final = true;
                        }
                    }
                }
            }

            return final;
        }
    }
}
