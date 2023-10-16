using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class GameManager
    {    
        //Game Loop
        public void RunGame()
        {        
            // Create an instance of Globals
            Globals globals = new Globals();

            // Load difficulty settings into the Globals instance
            globals.LoadDifficultySettings(globals);
            //Debug.Assert(globals.playerBasehealth != 0);

            // Pass the Globals instance to the components that need it
            Map map = new Map();
            HUD hud = new HUD();
            ItemManager itemManager = new ItemManager(globals);
            CursorController cursorController = new CursorController();
            EnemyManager enemyManager = new EnemyManager(map, itemManager, hud, cursorController, globals);
            Player player = new Player(22, 14, map, null, itemManager, hud, cursorController, globals);
            player = new Player(22, 14, map, player, itemManager, hud, cursorController, globals);
            CombatManager combatManager = new CombatManager(player, enemyManager, itemManager);

            while (globals.gameOver == false)
            {
                //updates                
                hud.Update(player, itemManager);
                map.Update();
                player.Update(combatManager);
                enemyManager.Update(combatManager);

                //draws                  
                player.Draw(player.posX, player.posY);
                enemyManager.Draw();
            }           
        }
    }
}
