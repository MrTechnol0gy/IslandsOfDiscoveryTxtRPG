using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; //Debug.WriteLine()

namespace IslandsOfDiscoveryTxtRPG
{
    internal class Enemy : Character
    {              
        //protected int xpValue;
        protected int moveEnergy;
        protected int energyToMove;
        protected bool hasSpawned;
        protected char mySpawnTile;

        public List<Tuple<int, int>> possibleSpawnPoints;       

        private Character target;
        public Enemy(Map map, ItemManager itemManager, HUD hud, CursorController cursorController, Globals globals) : base(map, itemManager, hud, cursorController, globals)
        {
            posX = 0;
            posY = 0;
            oldPosX = posX;
            oldPosY = posY;                        
            corpse = Globals.enemyCorpse;
            dead = false;
            Health = 0;            
            Strength = basestrength;
            XpValue = 0;
            moveEnergy = 0;
            myID = 0;
            hasSpawned = false;  
                        
            base.map = map;            
            this.itemManager = itemManager;
            this.hud = hud;
            this.cursorController = cursorController;  
            this.globals = globals;
        }
        public void Update(CombatManager combatManager)
        {
            SpawnMe();            
            DeathCheck();
            if (dead == false)
            {
                StoreMyPOS();
                MoveMe();
                if (map.CheckForBorder(posX, posY))
                {
                    ResetMyPOS();
                }
                Walkable(posX, posY);
                target = combatManager.FightCheck(this);                
                if (target != null)
                {
                    ResetMyPOS();
                    combatManager.Battle(this, target);                    
                }
                DeathCheck();
                map.Redraw(oldPosX, oldPosY);
            }
        }
        
        public void SpawnMe()
        {            
            Draw(posX, posY); //draws the enemy on the map            
        }

        public void MoveMe()
        {
            moveEnergy += 100;
            if (moveEnergy >= energyToMove)
            {
                int enemyMove = globals.random.Next(1, 5); //a random number to represent the four cardinal directions
            
                switch (enemyMove)
                {
                    case 1:
                    posY--;
                        break;

                    case 2:
                    posX--;
                        break;

                    case 3:
                    posY++;                
                        break;

                    case 4:
                    posX++;              
                        break;

                    default:             
                        break;
                }
                moveEnergy -= energyToMove;
            }
        }        
        override protected void DeathCheck()
        {
            if (dead == true)
            {
                return;
            }
            else if (Health <= 0)
            {
                Health = 0;                
                dead = true;    
            }
        }

        protected void SpawnPoint(char enemySpawnPoint)
        {            
            possibleSpawnPoints = map.SpawnPointsArray(enemySpawnPoint);        // gets all possible spawn points, based on the enemy specific desired char, from the Map
            int listLength = possibleSpawnPoints.Count;                         // limiter for the upper bounds of the random number
            int randomNum = globals.random.Next(0, listLength);                 // a random number that will be used to get a location in the possible spawn points list
            
            posY = possibleSpawnPoints.ElementAt(randomNum).Item1;              // sets the posY based on the first item at that random list location
            posX = possibleSpawnPoints.ElementAt(randomNum).Item2;              // sets the posX based on the second item at that same random list location

            possibleSpawnPoints.Clear();                                        // clears the list to prevent ending up with a massive mixed list; super important
        }
    }
}
