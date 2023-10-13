using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class ItemManager
    {
        private Globals globals;               
        
        public List<string> MasterTreasureList;     // Master List of all treasure, generated on start
        public List<string> PlayerInventory;        // Player's inventory
        public List<string>[] EnemyInventory;       // Array of enemy inventories

        public ItemManager(Globals globals)                                    // constructor for Item Manager
        {             
            this.globals = globals;
            MasterTreasureList = new List<string>();
            PlayerInventory = new List<string>();
            EnemyInventory = new List<string>[globals.maxEnemies];
                        
            InitTreasureInv();            
        }        

        public void InitTreasureInv()                           // initializes the starting master treasure list
        {
            MasterTreasureList.Add("boat");
            MasterTreasureList.Add("key");
            for (int i = 0; i < globals.maxEnemies * 2; i++)    // adds an amount of potions based on number of enemies
            {
                MasterTreasureList.Add("potion");
            }
            for (int j = 0; j < globals.maxEnemies * 5; j++)    // adds an amount of gold based on number of enemies
            {
                MasterTreasureList.Add("gold");
            }
        }        
                
        public void CreateEnemyInv(string name, int id)         // utilizes enemy name and id number to create custom inventories for each enemy
        {
            int randNum; 

            switch (name)
            {
                case "Treasure":
                    int treasureRandNum = globals.random.Next(2, 5);                                    // the amount of items the enemy will have
                    EnemyInventory[id] = new List<string>();
                    for (int x = 0; x < treasureRandNum; x++)
                    {
                        randNum = globals.random.Next(0, MasterTreasureList.Count());                   // random number no more than the length of the list
                        if (MasterTreasureList.Contains("key") || MasterTreasureList.Contains("boat"))  // assures that plot critical items are distributed to chests
                        {
                            for (int y = 0; y < MasterTreasureList.Count(); y++)
                            {
                                if (MasterTreasureList[y] == "key")
                                {
                                    EnemyInventory[id].Add(MasterTreasureList.ElementAt(y));
                                    MasterTreasureList.Remove(MasterTreasureList.ElementAt(y));
                                }
                                else if (MasterTreasureList[y] == "boat")
                                {
                                    EnemyInventory[id].Add(MasterTreasureList.ElementAt(y));
                                    MasterTreasureList.Remove(MasterTreasureList.ElementAt(y));
                                }
                            }
                        }
                        else
                        {
                            EnemyInventory[id].Add(MasterTreasureList.ElementAt(randNum));      // adds to the enemy inventory the inventory item at the random location in the master list
                            MasterTreasureList.Remove(MasterTreasureList.ElementAt(randNum));   // removes the previous item from the master inventory list
                        }
                    }
                    break;
                case "Slime":                    
                    int slimeRandNum = globals.random.Next(1, globals.maxEnemies / 6);      // the amount of items the enemy will have
                    EnemyInventory[id] = new List<string>();
                    for (int x = 0; x < slimeRandNum; x++)
                    {
                        randNum = globals.random.Next(0, MasterTreasureList.Count());       // random number no more than the length of the list
                        EnemyInventory[id].Add(MasterTreasureList.ElementAt(randNum));      // adds to the enemy inventory the inventory item at the random location in the master list
                        MasterTreasureList.Remove(MasterTreasureList.ElementAt(randNum));   // removes the previous item from the master inventory list
                    } 
                    break;
                    
                case "Wyvern":                    
                    int wyvernRandNum = globals.random.Next(1, globals.maxEnemies / 2);
                    EnemyInventory[id] = new List<string>();
                    for (int x = 0; x < wyvernRandNum; x++)
                    {
                        randNum = globals.random.Next(0, MasterTreasureList.Count());                        
                        EnemyInventory[id].Add(MasterTreasureList.ElementAt(randNum));
                        MasterTreasureList.Remove(MasterTreasureList.ElementAt(randNum));
                    }                    
                    break;
                case "SeaSerpent":                    
                    int seaserpentRandNum = globals.random.Next(1, globals.maxEnemies);                    
                    EnemyInventory[id] = new List<string>();
                    for (int x = 0; x < seaserpentRandNum; x++)
                    {
                        randNum = globals.random.Next(0, MasterTreasureList.Count());                        
                        EnemyInventory[id].Add(MasterTreasureList.ElementAt(randNum));
                        MasterTreasureList.Remove(MasterTreasureList.ElementAt(randNum));
                    }                    
                    break;
                case "Dragon": 
                    EnemyInventory[id] = new List<string>();
                    for (int x = 0; x < MasterTreasureList.Count; x++)                      // The Dragon gets all remaining treasure on the list
                    {                        
                        EnemyInventory[id].Add(MasterTreasureList.ElementAt(x));
                        MasterTreasureList.Remove(MasterTreasureList.ElementAt(x));
                    }
                    break;
                default:                    
                    break;
            }
        }
        
        public void CheckForPotion(Player player)                       // called when player attempts to use a potion
        {
            System.Collections.IList                                    // converts player inventory into a list (not a necessary format since Tuple removed?)
            list = PlayerInventory;

            for (int i = 0; i < list.Count; i++)
            {
                string item = (string)list[i];
                if (item == "potion")
                {                    
                    player.HealthIncrease(Globals.potionHealAmount);    // calls the player method to increase health based on potion heal amount
                    PlayerInventory.RemoveAt(i);                        // removes the potion from the player inventory
                    break;                                              // leaves the loop to prevent the player from using ALL potions
                }               
            }         
        }

        public void Reward(Character aggressor, Character victim)
        {
            if (aggressor.Name == "Player")
            {
                int amount = victim.itemManager.EnemyInventory[victim.myID].Count();
                if (amount != 0)
                {
                    Debug.WriteLine(amount);
                    for (int i = 0; i < amount; i++)
                    {
                        Debug.WriteLine(victim.itemManager.EnemyInventory[victim.myID].ElementAt(i));
                        PlayerInventory.Add(victim.itemManager.EnemyInventory[victim.myID].ElementAt(i));
                    }
                    victim.itemManager.EnemyInventory[victim.myID].Clear();
                }
            }
            else
            {
                int amount = victim.itemManager.EnemyInventory[victim.myID].Count();
                if (amount != 0)
                {
                    Debug.WriteLine(amount);
                    for (int i = 0; i < amount; i++)
                    {
                        Debug.WriteLine(victim.itemManager.EnemyInventory[victim.myID].ElementAt(i));
                        aggressor.itemManager.EnemyInventory[aggressor.myID].Add(victim.itemManager.EnemyInventory[victim.myID].ElementAt(i));
                    }
                    victim.itemManager.EnemyInventory[victim.myID].Clear();
                }
            }            
        }

        public int CountItems(string requestedItem)                 // creates a count of an offered item and returns the amount
        {
            int itemAmount = 0;            

            foreach (string item in PlayerInventory)
            {
                if (item == requestedItem)
                {
                    itemAmount++;                    
                }                
            }
            return itemAmount;
        }
    }
}