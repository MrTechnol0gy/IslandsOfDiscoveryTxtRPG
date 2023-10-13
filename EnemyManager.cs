using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class EnemyManager
    {
        public Map map;        
        public ItemManager itemManager;
        public HUD hud;
        public CursorController cursorController;
        public Globals globals;
        
        public List<Enemy> EnemiesList { get; set; }
        public List<Enemy> DeadEnemiesList { get; set; }
        public EnemyManager (Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals)
        {   
            this.map = map;            
            this.hud = hud;
            this.cursorController = cursorController;            
            this.itemManager = itemManager;
            this.globals = globals;

            EnemiesList = new List<Enemy>();
            DeadEnemiesList = new List<Enemy>();
            InitEnemiesList(map, itemManager, hud, cursorController, globals);            
        }

        public void Update(CombatManager combatManager)
        {            
            foreach (Enemy enemy in EnemiesList)
            {
                enemy.Update(combatManager);
                if (enemy.dead == true)
                {
                    DeadEnemy(enemy);                           // adds the dead enemy to the dead enemies list
                    enemy.Draw(enemy.posX, enemy.posY);         // draws the dead enemies corpse
                }
            }     
            ClearDeadEnemies();                                 // clear the list of dead enemies 
        }

        public void InitEnemiesList(Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals)
        {            
            for (int j = 0; j < globals.treasureChestAmountToSpawn; j++)
            {
                EnemiesList.Add(new TreasureChest(map, itemManager, hud, cursorController, globals));
                globals.enemyID += 1;
            }
            for (int j = 0; j < globals.slimeAmountToSpawn; j++)
            {
                EnemiesList.Add(new Slime(map, itemManager, hud, cursorController, globals));
                globals.enemyID += 1;
            } 
            for (int j = 0; j < globals.wyvernAmountToSpawn; j++)
            {
                EnemiesList.Add(new Wyvern(map, itemManager, hud, cursorController, globals));
                globals.enemyID += 1;
            } 
            for (int j = 0; j < globals.seaserpentAmountToSpawn; j++)
            {
                EnemiesList.Add(new SeaSerpent(map, itemManager, hud, cursorController, globals));
                globals.enemyID += 1;
            }                    
            for (int j = 0; j < globals.dragonAmountToSpawn; j++)
            {
                EnemiesList.Add(new Dragon(map, itemManager, hud, cursorController, globals));
                globals.enemyID += 1;
            }                    
        }

        public void Draw()
        {
            foreach (Enemy enemy in EnemiesList)
            {
                enemy.Draw(enemy.posX, enemy.posY);
            }
        }

        public List<Enemy> GetEnemyList()
        {
            return EnemiesList;
        }

        public void DeadEnemy(Enemy enemy)
        {
            DeadEnemiesList.Add(enemy);
        }

        public void ClearDeadEnemies()
        {
            foreach (Enemy enemy in DeadEnemiesList)
            {
                EnemiesList.Remove(enemy);
            }
            DeadEnemiesList.Clear();
        }
    }
}
