﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameService.Models
{
    public class Ships
    {
        private int[,] Grid = new int[10,10];
        private bool[,] destroyers = new bool[10,10];
    }
}
