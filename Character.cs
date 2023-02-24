﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal abstract class Character                       //abstract prevents you from constructing this class on the field
    {
        protected int basehealth, basespeed, basestrength;        
        public int health { get; set; }        
        public int strength { get; set; }
        public string name { get; set; }                    //name of character

        protected string character;                         //the alive version of the character
        protected string corpse;                            //the dead version of the character        
        public int posX, posY, oldPosX, oldPosY;            //current and one step prior locations for characters (utilized for map redrawing)
        protected bool dead = false;                        //decides whether the character is dead or not, which determines what char is drawn for the character
        protected bool moveRollBack = false;                //switch to flip if character is attempting to move into an illegal location        
        protected bool makeAttack = false;                  //switch to flip if character is able to make an attack

        public Map map;
        public Player player;
        public ItemManager itemManager;

        protected Random random = new Random();

        public Character (int posX, int posY, Map map, Player player, ItemManager itemManager)      //constructor, required to create string of data down inherited classes (ex. character -> enemy -> sea serpent)
        {
            this.posX = posX;
            this.posY = posY;
            this.map = map;
            this.player = player;
            this.itemManager = itemManager;
        }

        virtual protected void WallCheck(int x, int y, ItemManager itemManager)      //checks to see if the character is allowed to move onto the map location
        {
            if (x > map.cols || x < 0 + 1)                  //prevents character from moving outside bounds of border
            {
                moveRollBack = true;
            }
            else if (y > map.rows || y < 0 + 1)             //prevents character from moving outside bounds of border
            {
                moveRollBack = true;
            }
            else
            {
                switch (map.map[y - 1, x - 1])
                {
                    case '^':
                        moveRollBack = true;
                        break;
                    case '~':                        
                        moveRollBack = true;
                        break;
                    default:
                        moveRollBack = false;
                        break;
                }
            }
        }
        virtual protected Enemy FightCheck(Player player, Enemy enemy, Enemy enemy1, Enemy enemy2)
        {
            if (player.posX == enemy.posX && player.posY == enemy.posY)
            {
                makeAttack = true;                
                return enemy;
            }
            else if (player.posX == enemy1.posX && player.posY == enemy1.posY)
            {
                makeAttack = true;                
                return enemy1;
            }
            else if (player.posX == enemy2.posX && player.posY == enemy2.posY)
            {
                makeAttack = true;                
                return enemy2;
            }
            else if (enemy.posX == player.posX && enemy.posY == player.posY)
            {
                makeAttack = true;                                
            }
            else if (enemy1.posX == player.posX && enemy1.posY == player.posY)
            {
                makeAttack = true;                
            }
            else if (enemy2.posX == player.posX && enemy2.posY == player.posY)
            {
                makeAttack = true;                
            }
            else if (enemy.posX == enemy1.posX && enemy.posY == enemy1.posY || enemy.posX == enemy2.posX && enemy.posY == enemy2.posY)
            {
                moveRollBack = true;
            }
            else if (enemy1.posX == enemy2.posX && enemy1.posY == enemy2.posY || enemy1.posX == enemy.posX && enemy1.posY == enemy.posY)
            {
                moveRollBack = true;
            }
            else if (enemy2.posX == enemy1.posX && enemy2.posY == enemy1.posY || enemy2.posX == enemy.posX && enemy2.posY == enemy.posY)
            {
                moveRollBack = true;
            }            
            return null;
        }        
        virtual protected void GetMyPOS() //getter
        {
            oldPosX = posX;
            oldPosY = posY;
        }
        virtual protected void ResetMyPOS() //setter
        {
            posX = oldPosX;
            posY = oldPosY;
        }
        virtual protected void DeathCheck()
        {
            if (health <= 0)
            {
                health = 0;
                dead = true;
            }
        }
        virtual public void Draw(int posX, int posY)
        {
            CursorController.CharacterPrintCursor(posX, posY);
            if (dead == false)
            {
                Console.Write(character);
            }
            else if (dead == true)
            {
                Console.Write(corpse);
            }
        }
    }
}
