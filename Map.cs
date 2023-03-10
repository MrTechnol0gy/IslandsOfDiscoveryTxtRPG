using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class Map
    {       
        public char[,] map;                
        
        public static bool firstmaprender = true;

        public int rows;
        public int cols;
        
        public void Update()
        {
            if (firstmaprender == true)
            {
                //Console.Clear();
                firstmaprender = false;                
                Draw();
            }
        }

        public Map() //constructor
        {
            string[] mapString = File.ReadAllLines(@"Maps_and_Overlays/OverworldMap_01.txt");
            map = new char[mapString.GetLength(0), mapString[0].Length];

            for (int x = 0; x < mapString.GetLength(0); x++)
            for (int y = 0; y < mapString[0].Length; y++)
                
            map[x, y] = mapString[x][y];                
            
            rows = map.GetLength(0);
            cols = map.GetLength(1);
        }        
        public void Draw()
        {
            CursorController.CursorInner(0, 0);
            int posY = 1;                                           //accounts for offset of border
                for (int x = 0; x < rows; x++)
                {                    
                    for (int y = 0; y < cols; y++)
                    {                           
                        ColourCode(x, y);                        
                        Console.Write(map[x, y]);                           
                    }
                    Console.BackgroundColor = ConsoleColor.Black;                        
                    Console.WriteLine();
                    CursorController.CursorInner(0, posY);          //adjusts the cursor to print each line on the correct line
                    posY++;
                }                
        }
        public void ColourCode(int x, int y)
        {
            switch (map[x, y]) //checks the characters in the array and assigns them colours
            {
                case '^': //mountain
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case '`': //grass
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case '~': //water
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case '*': //forest
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case '∩': //dungeon entrance
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case 'C': //castle entrance
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case '#': //sand
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
        public void Redraw(int x, int y)
        {
            CursorController.CharacterPrintCursor(x, y);                            //sets the cursor position to the players previous location                  
            ColourCode(y - 1, x - 1);                                               //gets the appropriate colour for reprinting the previous position
            Console.Write(map[y - 1, x - 1]);                                       //writes the map array location at the player's previous location
            Console.BackgroundColor = ConsoleColor.Black;                           //resets the console color for normal printing        
        }    
        
        public bool TerrainCheck(char i, int x, int y)                              //takes in a char representing a terrain tile and an x/y location and returns true/false if it matches the location on the map
        {            
            if (x > cols || x < 0 + 1 || y > rows || y < 0 + 1)                     //checks to see if the move is outside the bounds of the map first
            {
                return false;
            }
            else if (map[y - 1, x - 1] == i)
            {
                return true;
            }
            return false;            
        }
    }
}
