﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class SeaSerpent : Enemy
    {
        public SeaSerpent(int x, int y, Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals) : base(x, y, map, itemManager, hud, cursorController, globals)
        {
            Name = globals.seaserpentName;
            character = globals.seaserpentCharacter;
            basehealth = globals.seaserpentBasehealth;
            basespeed = globals.seaserpentBasespeed;
            basestrength = globals.seaserpentBasestrength;
            Health = basehealth;            
            Strength = basestrength;
            xpValue = globals.seaserpentXPValue;            
            energyToMove = globals.seaserpentEnergyToMove;
            myID = globals.enemyID;
            base.map = map;            
            base.itemManager = itemManager;
            base.hud = hud;
            base.cursorController = cursorController; 
            base.globals = globals;
        }
        
        override protected void Walkable(int x, int y)
        {
            if (!map.TerrainCheck('~', x, y))
            {
                ResetMyPOS();
            }            
        }
    }
}
