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
        private int[] positiosn= new int[10];
        private int[] dis1 = new int[4];
        private int[] dis2 = new int[4];
        private int count = 0;
        private int value;
        private bool state = false;
        private int temp;
        private int r1, r2, c1, c2;
        private bool res = false;
        DistroyerShip disShip;
        ResponsBody response;

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

        public ResponsBody KilUser(DistroyerShip User)
        {
            response = new ResponsBody();
            disShip = User;
            
            temp = random.Next(100);
            for (int r =0; r <10; r++)
            {
                for (int c=0; c<10; c++)
                {
                    value = grid[r, c];
                     if (temp == value)
                     {
                        state = shipSet[r, c];
                        if (state == true)
                        {
                            res = true;
                        }
                        else {
                            res = false;
                        }
                        response.Hit = res;
                        response.SelectedValue = temp;
                    }
                }
            }

            return response;
            
        }

        // Locate player ship set
        public DistroyerShip GeneratePlayer(DistroyerShip player)
        {
            disShip = new DistroyerShip();
            SetDistroyers(player);
            SetBattleship(player);

            disShip.Battleship = positiosn;
            disShip.Ship1 = dis1;
            disShip.Ship2 = dis2;

            return disShip;
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
            int round = 0;
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
                            
                            if (c==0)
                            {
                                coun = c;

                                while (round < 9)
                                {
                                    shipSet[r, coun] = true;
                                    coun++;
                                    positiosn[round] = r;
                                    positiosn[(round+1)] = coun;
                                    round+=2;
                                }
                            }
                            else if (c==9)
                            {
                                coun = c;
                                while (round < 9)
                                {
                                    shipSet[r, coun] = true;
                                    coun--;
                                    positiosn[round] = r;
                                    positiosn[(round + 1)] = coun;
                                    round += 2;
                                }
                            }
                            else {
                                int temp = c;
                                res = 10 - (++temp);
                                if (res >= 5)
                                {
                                    coun = c;
                                    while (round < 9)
                                    {
                                        shipSet[r, coun] = true;
                                        coun++;
                                        positiosn[round] = r;
                                        positiosn[(round + 1)] = coun;

                                        round += 2;
                                    }
                                }
                                else if (res < 5)
                                {
                                    coun = c;
                                    while (round < 9)
                                    {
                                        shipSet[r, coun] = true;
                                        coun--;
                                        positiosn[round] = r;
                                        positiosn[(round + 1)] = coun;
                                        round += 2;
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
        
        //Generate locations enimies Distroyerships 
        private void SetDistroyers()
        {
            int counter=0;
            int number;
            int row ;

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

        // set player Distroyer ships
        private void SetDistroyers(DistroyerShip player)
        {
            DistroyerShip playership = player;
            int counter = 2;
            int row = 0;
            int column =0;
            int r = 0;
            while (counter > 0)
            {
                switch (counter)
                {
                    case 2:
                        row = playership.Ship1[0];
                        column = playership.Ship1[1];break;
                    case 1:
                        row = playership.Ship2[0];
                        column = playership.Ship2[1]; break;
                }
                temp = column;
                r = row;
                state = shipSet[row, column];
                if (state != true)
                {
                    if (column == 9)
                    {
                        temp--;
                    }
                    else if (column == 0)
                    {
                        temp++;
                    }
                    else
                    {
                        temp++;
                    }

                    state = shipSet[row, temp];
                    if (state != true)
                    {
                        shipSet[row, column] = true;
                        shipSet[row, temp] = true;
                        r1 = row;
                        c1 = column;
                        r2 = row;
                        c2 = temp;
                    }
                    else
                    {
                        if (0 < column && column < 9)
                        {
                            temp = temp - 2;
                            state = shipSet[row, temp];
                            if (state != true)
                            {
                                shipSet[row, column] = true;
                                shipSet[row, temp] = true;
                                r1 = row;
                                c1 = column;
                                r2 = row;
                                c2 = temp;
                            }
                            else if (r != 0)
                            {
                                r--;
                                state = shipSet[r, column];
                                if (state != true)
                                {
                                    shipSet[row, column] = true;
                                    shipSet[r, column] = true;

                                    r1 = row;
                                    c1 = column;
                                    r2 = r;
                                    c2 = column;
                                }
                                else
                                {
                                    r = r + 2;
                                    state = shipSet[r, column];
                                    if (state != true)
                                    {
                                        shipSet[row, column] = true;
                                        shipSet[r, column] = true;
                                        r1 = row;
                                        c1 = column;
                                        r2 = r;
                                        c2 = column;
                                    }
                                }
                            }
                        }
                    }
                    switch (counter)
                    {
                        case 2:
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
                    counter--;
                }

            }

        }

        //set player Battlship
        private void SetBattleship(DistroyerShip player)
        {
            DistroyerShip playership = player;
            int row = playership.Battleship[0];
            int column = playership.Battleship[1];
            
            shipSet[row, column] = true;
            positiosn[0] = row;
            positiosn[1] = column;
            bool res = FindColumn(column,row);

            if (res != true)
            {
                findRow(column, row);
            }
           
        }

        //Find available row
        private bool FindColumn(int column,int row)
        {
            int colCount = 0;
            int counter = 0;
            bool res = false;
            bool result = false;

            colCount = 10 - (column+1);
            
            if (colCount < 4)
            {
                temp = column;
                while (result != true) // validate row and column
                {
                    temp--;
                    state = shipSet[row, temp];
                    if (state != true)
                    {
                        counter++;
                    }
                    else {
                        res = false;
                        result = true;
                        break;
                    }

                    if (counter == 4)
                    {
                        temp = column;
                        for (int i = 2; i < 9; i = i + 2)
                        {
                            temp--;
                            int t = i + 1;
                            positiosn[i] = row;
                            positiosn[t] = temp;
                        }
                        res = true;
                        result = true;
                    }

                }
            }
            else if (colCount > 4)
            {
                temp = column;
                while (result != true) // validate row
                {
                    temp++;
                    state = shipSet[row, temp];
                    if (state != true)
                    {
                        counter++;
                    }
                    else {
                        res = false;
                        result = true;
                        break;
                    }

                    if (counter == 4)
                    {
                        temp = column;
                        for (int i = 2; i < 9; i = i + 2)
                        {
                            temp++;
                            int t = i + 1;
                            positiosn[i] = row;
                            positiosn[t] = temp;
                        }
                        res = true;
                        result = true;
                    }
                }
            }
            else if (colCount == 4)
            {
                temp = column;
                while (result != true) // validate row forward
                {
                    temp++;
                    state = shipSet[row, temp];
                    if (state != true)
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                        temp = column;

                        while (result != true) // validate row backward
                        {
                            temp--;
                            state = shipSet[row, temp];
                            if (state != true)
                            {
                                counter++;
                            }
                            else {
                                res = false;
                                result = true;
                                break;
                            }

                            if (counter == 4)
                            {
                                temp = column;
                                for (int i = 2; i < 9; i = i + 2)
                                {
                                    temp--;
                                    int t = i + 1;
                                    positiosn[i] = row;
                                    positiosn[t] = temp;
                                }
                                res = true;
                                result = true;
                            }
                        }
                        result = true;
                    }

                    if (counter == 4)
                    {
                        temp = column;
                        for (int i = 2; i < 9; i = i + 2)
                        {
                            temp++;
                            int t = i + 1;
                            positiosn[i] = row;
                            positiosn[t] = temp;
                        }
                        res = true;
                        result = true;
                    }
                }
            }

            return res;
        }

        // Find available row
        private bool findRow(int column, int row)
        {
            bool res = false;
            bool result = false;
            int rowCount = 0;
            int counter = 0;
            int r = row + 1;
            rowCount = 10 - (r);

            if (rowCount < 4)
            {
                temp = row;
                while (result != true) // validate row and column
                { 
                    temp--;
                    state = shipSet[temp, column];
                    if (state != true)
                    {
                        counter++;
                    }
                    else
                    {
                        res = false;
                        result = true;
                        break;
                    }

                    if (counter == 4)
                    {
                        temp = row;
                        for (int i = 2; i < 9; i = i + 2)
                        {
                            temp--;
                            int t = i + 1;
                            positiosn[i] = temp;
                            positiosn[t] = column;
                        }
                        res = true;
                        result = true;
                    }

                }
            }
            else if (rowCount > 4)
            {
                temp = row;
                while (result != true) // validate row
                {
                    temp++;
                    state = shipSet[temp, column];
                    if (state != true)
                    {
                        counter++;
                    }
                    else
                    {
                        res = false;
                        result = true;
                        break;
                    }

                    if (counter == 4)
                    {
                        temp = row;
                        for (int i = 2; i < 9; i = i + 2)
                        {
                            temp++;
                            int t = i + 1;
                            positiosn[i] = temp;
                            positiosn[t] = column;
                        }
                        res = true;
                        result = true;
                    }
                }
            }
            else if (rowCount == 4)
            {
                temp = row;
                while (result != true) // validate row forward
                {
                    temp++;
                    state = shipSet[temp, column];
                    if (state != true)
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                        temp = row;
                        while (result != true) // validate row backward
                        {
                            temp--;
                            state = shipSet[temp, column];
                            if (state != true)
                            {
                                counter++;
                            }
                            else
                            {
                                res = false;
                                result = true;
                                break;
                            }

                            if (counter == 4)
                            {
                                temp = row;
                                for (int i = 2; i < 9; i = i + 2)
                                {
                                    temp--;
                                    int t = i + 1;
                                    positiosn[i] = temp;
                                    positiosn[t] = column;
                                }
                                res = true;
                                result = true;
                            }
                        }
                        result = true;
                    }

                    if (counter == 4)
                    {
                        temp = row;
                        for (int i = 2; i < 9; i = i + 2)
                        {
                            temp++;
                            int t = i + 1;
                            positiosn[i] = temp;
                            positiosn[t] = column;
                        }
                        res = true;
                        result = true;
                    }
                }
            }

            return res;
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
