using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IslandsOfDiscoveryTxtRPG
{
    internal abstract class Character                       //abstract prevents you from constructing this class on the field
    {
        protected int basestrength;        
        public int Health { get; set; }        
        public int Strength { get; set; }
        public string Name { get; set; }                    //name of character
        public int XpValue { get; set; }
        public int myID;                                    //id of character(enemy)        

        protected string character;                         //the alive version of the character
        protected string corpse;                            //the dead version of the character        
        public int posX, posY, oldPosX, oldPosY;            //current and one step prior locations for characters (utilized for map redrawing)
        public bool dead;                                //decides whether the character is dead or not, which determines what char is drawn for the character        

        public Map map;
        public Player player;
        public ItemManager itemManager;
        public HUD hud;
        public CursorController cursorController;
        public Globals globals;        

        public Character (Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals)      //constructor, required to create string of data down inherited classes (ex. character -> enemy -> sea serpent)
        {            
            this.map = map;            
            this.itemManager = itemManager;
            this.hud = hud;
            this.cursorController = cursorController;
            this.globals = globals;
        }

        virtual protected void Walkable(int x, int y)
        {
            if (map.CheckForTerrain('^', x, y))
            {
                ResetMyPOS();
            }
            else if (map.CheckForTerrain('~', x, y))
            {
                ResetMyPOS();
            }
            else 
            {
                 
            }
        }         
        virtual protected void StoreMyPOS() 
        {
            oldPosX = posX;
            oldPosY = posY;
        }
        virtual protected void ResetMyPOS() 
        {
            posX = oldPosX;
            posY = oldPosY;
        }        
        virtual protected void DeathCheck()
        {
            if (Health <= 0)
            {
                Health = 0;
                dead = true;
            }
        }
        virtual public Character HealthDecrease(int amount)
        {
            CursorController.InputAreaCursor(4, 0);
            Console.WriteLine("The " + Name + " has lost " + amount + " health!          ");
            Health -= amount;
            if (Health < 0)                     //if this character dies as a result of the fight, returns the character to the CombatManager
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        virtual public void HealthIncrease(int amount)
        {
            CursorController.InputAreaCursor(3, 0);
            Console.WriteLine("The " + Name + " has healed for " + amount + " health!");
            Health += amount;
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
