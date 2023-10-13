using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class Dragon : Enemy
    {
        public Dragon(Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals) : base(map, itemManager, hud, cursorController, globals)
        {
            Name = globals.dragonName;
            character = globals.dragonCharacter;            
            Health = globals.dragonBasehealth;
            Strength = globals.dragonBasestrength;
            XpValue = globals.dragonXPValue;
            energyToMove = globals.dragonEnergyToMove;
            myID = globals.enemyID;
            mySpawnTile = globals.dragonSpawnPoint;
            base.map = map;
            base.itemManager = itemManager;
            base.hud = hud;
            base.cursorController = cursorController;
            base.globals = globals;

            SpawnPoint(mySpawnTile);
            itemManager.CreateEnemyInv(Name, myID);
        }
    }
}
