using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class Wyvern : Enemy
    {
        public Wyvern(Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals) : base(map, itemManager, hud, cursorController, globals)
        {
            Name = globals.wyvernName;
            character = globals.wyvernCharacter;            
            Health = globals.wyvernBasehealth;            
            Strength = globals.wyvernBasestrength;
            XpValue = globals.wyvernXPValue;            
            energyToMove = globals.wyvernEnergyToMove;
            myID = globals.enemyID;
            mySpawnTile = globals.wyvernSpawnPoint;

            base.map = map;            
            base.itemManager = itemManager;
            base.hud = hud;
            base.cursorController = cursorController;    
            base.globals = globals;

            SpawnPoint(mySpawnTile);
            itemManager.CreateEnemyInv(Name, myID);
        }

        override protected void Walkable(int x, int y)
        {
            if (map.CheckForTerrain('~', x, y))
            {
                ResetMyPOS();
            }
        }
    }
}
