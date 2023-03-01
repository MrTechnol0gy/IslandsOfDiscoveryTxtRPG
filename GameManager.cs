﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class GameManager
    {
        public static bool gameOver = false;

        static Map map = new Map();
        HUD hud = new HUD();
        static ItemManager itemManager = new ItemManager();
        static Player player = new Player(22, 14, map, player, itemManager);
        Enemy enemy = new SeaSerpent(4, 4, map, player, itemManager);
        Enemy enemy2 = new Wyvern(24, 15, map, player, itemManager);
        Enemy enemy3 = new Slime(25, 14, map, player, itemManager);        
        CursorController cursorController = new CursorController();

        //Game Loop
        public void RunGame()
        {            
            while (gameOver == false)
            {
                //updates
                itemManager.Update();                
                hud.Update(player, itemManager);
                map.Update();
                player.Update(enemy, enemy2, enemy3);
                enemy.Update(enemy, enemy2, enemy3);
                enemy2.Update(enemy, enemy2, enemy3);
                enemy3.Update(enemy, enemy2, enemy3);                                           

                //draws 
                //hud.Draw();
                player.Draw(player.posX, player.posY);
                enemy.Draw(enemy.posX, enemy.posY);
                enemy2.Draw(enemy2.posX, enemy2.posY);
                enemy3.Draw(enemy3.posX, enemy3.posY);
            }           
        }
    }
}
