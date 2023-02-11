﻿using System;
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
                
        public static int scale = 1;
        public static bool firstmaprender = true;

        public int rows;
        public int cols;
        
        public void Update()
        {
            if (firstmaprender == true)
            {
                //Console.Clear();
                firstmaprender = false;
                
                DisplayMap(scale);
            }
        }

        public Map()
        {
            string[] mapString = File.ReadAllLines(@"OverworldMap_01.txt");
            map = new char[mapString.GetLength(0), mapString[0].Length];

            for (int x = 0; x < mapString.GetLength(0); x++)
            for (int y = 0; y < mapString[0].Length; y++)
                
            map[x, y] = mapString[x][y];                
            
            rows = map.GetLength(0);
            cols = map.GetLength(1);
        }        
        public void DisplayMap(int scale)
        {              
                for (int x = 0; x < rows; x++)
                {
                    for (int m = 0; m < scale; m++)
                    {                        
                        for (int y = 0; y < cols; y++)
                        {
                            for (int z = 0; z < scale; z++)
                            {
                                ColourCode(x, y);
                                //set the cursor here?
                                Console.Write(map[x, y]);
                            }
                        }
                        Console.BackgroundColor = ConsoleColor.Black;                        
                        Console.WriteLine();
                    }
                }                
        }
        public void ColourCode(int x, int y)
        {
            switch (map[x, y]) //checks the characters in the array and assigns them colours
            {
                case '^':
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case '`':
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case '~':
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case '*':
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case '∩':
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case 'C':
                    Console.BackgroundColor = ConsoleColor.Magenta;
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
    }
}
