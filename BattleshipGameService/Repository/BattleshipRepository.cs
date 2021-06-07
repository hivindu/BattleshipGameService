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
        private int[] battlesipPosition= new int[10];
        private int[] destroyerShip1 = new int[4];
        private int[] destroyerShip2 = new int[4];
        private int count = 0;
        private int value;
        private bool state = false;
        private int temp;
        private int row1, row2, column1, column2;
        private bool res = false;
        Ships disShip;
        ResponsBody response;

        public BattleshipRepository()
        {
            random = new Random();
            GenerateGrid();
            GenerateEnimiShips();
        }

        public Ships GenerateEnemy()
        {
            disShip = new Ships();
            AssignBattleshipShip();
            SetDistroyers();
            disShip.Battleship = battlesipPosition;
            disShip.DestroyerShip1 = destroyerShip1;
            disShip.DestroyerShip2 = destroyerShip2;

            return disShip;
        }


        public ResponsBody KillEnemy(Ships enemy, int value)
        {
            response = new ResponsBody();
            disShip = enemy;

            battlesipPosition = disShip.Battleship;
            destroyerShip1 = disShip.DestroyerShip1;
            destroyerShip2 = disShip.DestroyerShip2;

            temp = battlesipPosition[0];

            for (int i = 1; i < 6; i++)
            {
                shipSet[temp, i] = true;
            }

            row1 = destroyerShip1[0];
            column1 = destroyerShip1[1];
            row2 = destroyerShip1[2];
            column2 = destroyerShip1[3];

            shipSet[row1, column1] = true;
            shipSet[row2, column2] = true;

            row1 = destroyerShip2[0];
            column1 = destroyerShip2[1];
            row2 = destroyerShip2[2];
            column2 = destroyerShip2[3];

            shipSet[row1, column1] = true;
            shipSet[row2, column2] = true;

            res = HitMiss(value);

            if (res != true)
            {
                response.Hit = false;
            }
            else {
                response.Hit = true;
            }

            response.SelectedValue = value;
            return response;
        }

        public ResponsBody KilUser(Ships User)
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
        public Ships GeneratePlayer(Ships player)
        {
            disShip = new Ships();
            SetDistroyers(player);
            SetBattleship(player);

            disShip.Battleship = battlesipPosition;
            disShip.DestroyerShip1 = destroyerShip1;
            disShip.DestroyerShip2 = destroyerShip2;

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

        //Generate enemies ship list (make all false)
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

        // locate enemies BattleshipShip 
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
                                    battlesipPosition[round] = r;
                                    battlesipPosition[(round+1)] = coun;
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
                                    battlesipPosition[round] = r;
                                    battlesipPosition[(round + 1)] = coun;
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
                                        battlesipPosition[round] = r;
                                        battlesipPosition[(round + 1)] = coun;

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
                                        battlesipPosition[round] = r;
                                        battlesipPosition[(round + 1)] = coun;
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
                                    row1 = r;
                                    column1 = c;
                                    row2 = r;
                                    column2 = temp;
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
                                        row1 = r;
                                        column1 = c;
                                        row2 = r;
                                        column2 = temp;
                                    } 
                                    else if (row != 0)
                                    {
                                        row--;
                                        state = shipSet[row, c];
                                        if (state != true)
                                        {
                                            shipSet[r, c] = true;
                                            shipSet[row, c] = true;

                                            row1 = r;
                                            column1 = c;
                                            row2 = row;
                                            column2 = c;
                                        }
                                        else {
                                            row = row + 2;
                                            state = shipSet[row, c];
                                            if (state != true)
                                            {
                                                shipSet[r, c] = true;
                                                shipSet[row, c] = true;
                                                row1 = r;
                                                column1 = c;
                                                row2 = row;
                                                column2 = c;
                                            }
                                        }
                                    }
                                    }
                                }
                                switch (counter)
                                {
                                    case 0:
                                        destroyerShip1[0] = row1;
                                        destroyerShip1[1] = column1;
                                        destroyerShip1[2] = row2;
                                        destroyerShip1[3] = column2; break;
                                    case 1:
                                        destroyerShip2[0] = row1;
                                        destroyerShip2[1] = column1;
                                        destroyerShip2[2] = row2;
                                        destroyerShip2[3] = column2; break;
                                }
                                counter++;
                            }// end of checking status condition

                        }// end first If condition

                    }// end of second for loop
                }// end of first for loop
            }

        }

        // set player Distroyer ships
        private void SetDistroyers(Ships player)
        {
            Ships playership = player;
            int counter = 2;
            int row = 0;
            int column =0;
            int r = 0;
            while (counter > 0)
            {
                switch (counter)
                {
                    case 2:
                        row = playership.DestroyerShip1[0];
                        column = playership.DestroyerShip1[1];break;
                    case 1:
                        row = playership.DestroyerShip2[0];
                        column = playership.DestroyerShip2[1]; break;
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
                        row1 = row;
                        column1 = column;
                        row2 = row;
                        column2 = temp;
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
                                row1 = row;
                                column1 = column;
                                row2 = row;
                                column2 = temp;
                            }
                            else if (r != 0)
                            {
                                r--;
                                state = shipSet[r, column];
                                if (state != true)
                                {
                                    shipSet[row, column] = true;
                                    shipSet[r, column] = true;

                                    row1 = row;
                                    column1 = column;
                                    row2 = r;
                                    column2 = column;
                                }
                                else
                                {
                                    r = r + 2;
                                    state = shipSet[r, column];
                                    if (state != true)
                                    {
                                        shipSet[row, column] = true;
                                        shipSet[r, column] = true;
                                        row1 = row;
                                        column1 = column;
                                        row2 = r;
                                        column2 = column;
                                    }
                                }
                            }
                        }
                    }
                    switch (counter)
                    {
                        case 2:
                            destroyerShip1[0] = row1;
                            destroyerShip1[1] = column1;
                            destroyerShip1[2] = row2;
                            destroyerShip1[3] = column2; break;
                        case 1:
                            destroyerShip2[0] = row1;
                            destroyerShip2[1] = column1;
                            destroyerShip2[2] = row2;
                            destroyerShip2[3] = column2; break;
                    }
                    counter--;
                }

            }

        }

        //set player Battlship
        private void SetBattleship(Ships player)
        {
            Ships playership = player;
            int row = playership.Battleship[0];
            int column = playership.Battleship[1];
            
            shipSet[row, column] = true;
            battlesipPosition[0] = row;
            battlesipPosition[1] = column;
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
                            battlesipPosition[i] = row;
                            battlesipPosition[t] = temp;
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
                            battlesipPosition[i] = row;
                            battlesipPosition[t] = temp;
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
                                    battlesipPosition[i] = row;
                                    battlesipPosition[t] = temp;
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
                            battlesipPosition[i] = row;
                            battlesipPosition[t] = temp;
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
                            battlesipPosition[i] = temp;
                            battlesipPosition[t] = column;
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
                            battlesipPosition[i] = temp;
                            battlesipPosition[t] = column;
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
                                    battlesipPosition[i] = temp;
                                    battlesipPosition[t] = column;
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
                            battlesipPosition[i] = temp;
                            battlesipPosition[t] = column;
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
