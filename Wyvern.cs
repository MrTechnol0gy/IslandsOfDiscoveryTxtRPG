﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class Wyvern : Enemy
    {
        public Wyvern(int x, int y, Map map, Player player, ItemManager itemManager) : base(x, y, map, player, itemManager)
        {
            name = "Wyvern";
            character = "W";
            basehealth = 12;
            basespeed = 12;
            basestrength = 4;
            health = basehealth;            
            strength = basestrength;
            xpValue = 5;
            moveEnergy = 0;
            energyToMove = 100;
            base.map = map;
            base.player = player;
            base.itemManager = itemManager;
        }

        override protected void WallCheck(int x, int y, ItemManager itemManager) //checks to see if the character is allowed to move onto the map location
        {
            if (x > map.cols || x < 0 + 1) //prevents character from moving outside bounds of border
            {
                moveRollBack = true;
            }
            else if (y > map.rows || y < 0 + 1) //prevents character from moving outside bounds of border
            {
                moveRollBack = true;
            }
            else
            {
                switch (map.map[y - 1, x - 1])
                {                    
                    default:
                        moveRollBack = false;
                        break;
                }
            }
        }
    }
}
