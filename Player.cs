﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class Player : Character
    {                
        public ConsoleKeyInfo key;
        private int level;
        public Player(int x, int y, Map map) : base(x, y, map)
        {
            posX = x;
            posY = y;
            oldPosX = x;
            oldPosY = y;
            character = "P";
            corpse = "X";
            dead = false;
            level = 1;
            basehealth = 25;
            basespeed = 5;
            basestrength = 5;
            health = basehealth + level;
            speed = basespeed + level;
            strength = basestrength + level;
            base.map = map;
        }
        public void Update(Enemy enemy, Enemy enemy2, Enemy enemy3)
        {
            GetMyPOS();
            PlayerChoice();
            WallCheck(posX, posY);
            CombatManager.FightCheck(posX, posY, enemy.posX, enemy.posY, enemy2.posX, enemy2.posY, enemy3.posX, enemy3.posY);
            if (moveRollBack == true || CombatManager.moveRollBack == true)
            {
                CombatManager.moveRollBack = false;
                moveRollBack = false;
                ResetMyPOS();
            }
            map.Redraw(oldPosX, oldPosY);
        }

        public void PlayerChoice()
        {
            CursorController.InputAreaCursor(0, 0);

            Console.WriteLine("Press 'W' to move North, 'A' to move West, 'S' to move South, or 'D' to move East. Press 'ESC' to Quit.");

            CursorController.InputAreaCursor(1, 0);

            key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    GameManager.gameOver = true;
                    break;
                case ConsoleKey.W:
                    posY--;
                    break;
                case ConsoleKey.A:
                    posX--;                    
                    break;
                case ConsoleKey.S:
                    posY++;                    
                    break;
                case ConsoleKey.D:
                    posX++;                   
                    break;
                default:
                    break;
            }
        }
    }
}
