using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class TreasureChest : Enemy
    {
        public TreasureChest(Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals) : base(map, itemManager, hud, cursorController, globals)
        {
            Name = globals.treasureName;
            character = globals.treasureCharacter;            
            Health = globals.treasureBasehealth;
            Strength = globals.treasureBasestrength;
            XpValue = globals.treasureXPValue;
            energyToMove = globals.treasureEnergyToMove;
            myID = globals.enemyID;
            mySpawnTile = globals.treasureSpawnPoint;
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
