using System;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace IslandsOfDiscoveryTxtRPG
{
    internal class Globals
    {

        public string Difficulty { get; set; } // Add a property for difficulty
        // Game Management
        public bool gameOver = false;
        public Random random = new Random();        

        // Player Information
        public string playerName { get; set; }
        public string playerCharacter { get; set; }
        public string playerCorpse { get; set; }
        public int playerLevel { get; set; }
        public int playerBasehealth { get; set; }
        public int playerBasestrength { get; set; }
        public bool isPlayerDead = false;

        // All Enemy Information
        public int enemyID = 0;
        public const string enemyCorpse = "x";
        public int maxEnemies { get; set; }             // this should equal the total amount of enemies to spawn in each enemy category

        // Enemy - Slime
        public string slimeName { get; set; }
        public string slimeCharacter { get; set; }
        public char slimeSpawnPoint { get; set; }
        public int slimeAmountToSpawn { get; set; }
        public int slimeBasehealth { get; set; }
        public int slimeBasestrength { get; set; }
        public int slimeXPValue { get; set; }
        public int slimeEnergyToMove { get; set; }

        // Enemy - Wyvern
        public string wyvernName { get; set; }
        public string wyvernCharacter { get; set; }
        public char wyvernSpawnPoint { get; set; }
        public int wyvernAmountToSpawn { get; set; }
        public int wyvernBasehealth { get; set; }
        public int wyvernBasestrength { get; set; }
        public int wyvernXPValue { get; set; }
        public int wyvernEnergyToMove { get; set; }

        // Enemy - Sea Serpent
        public string seaserpentName { get; set; }
        public string seaserpentCharacter { get; set; }
        public char seaserpentSpawnPoint { get; set; }
        public int seaserpentAmountToSpawn { get; set; }
        public int seaserpentBasehealth { get; set; }
        public int seaserpentBasestrength { get; set; }
        public int seaserpentXPValue { get; set; }
        public int seaserpentEnergyToMove { get; set; }

        // Enemy - Dragon
        public string dragonName { get; set; }
        public string dragonCharacter { get; set; }
        public char dragonSpawnPoint { get; set; }
        public int dragonAmountToSpawn { get; set; }
        public int dragonBasehealth { get; set; }
        public int dragonBasestrength { get; set; }
        public int dragonXPValue { get; set; }
        public int dragonEnergyToMove { get; set; }

        // Enemy - Treasure Chest
        public string treasureName { get; set; }
        public string treasureCharacter { get; set; }
        public char treasureSpawnPoint { get; set; }
        public int treasureChestAmountToSpawn { get; set; }
        public int treasureBasehealth { get; set; }
        public int treasureBasestrength { get; set; }
        public int treasureXPValue { get; set; }
        public int treasureEnergyToMove { get; set; }

        public void LoadDifficultySettings(Globals target)
        {
            // Load settings from the appropriate JSON file based on the selected difficulty
            string filePath = "DifficultySettings/IsleOfDiscoveryNormalDifficultySettings.txt";
            string jsonData = File.ReadAllText(filePath);
            var settings = JsonSerializer.Deserialize<Globals>(jsonData);

            // Copy the settings to the current instance
            target.CopySettings(settings);
        }

        private void CopySettings(Globals source)
        {
            // Copy settings from source to current instance
            // Player Data
            playerName = source.playerName;
            playerCharacter = source.playerCharacter;
            playerCorpse = source.playerCorpse;
            playerLevel = source.playerLevel;
            playerBasehealth = source.playerBasehealth;
            playerBasestrength = source.playerBasestrength;

            // Slime Data
            slimeName = source.slimeName;
            slimeCharacter = source.slimeCharacter;
            slimeSpawnPoint = source.slimeSpawnPoint;
            slimeAmountToSpawn = source.slimeAmountToSpawn;
            slimeBasehealth = source.slimeBasehealth;
            slimeBasestrength = source.slimeBasestrength;
            slimeXPValue = source.slimeXPValue;
            slimeEnergyToMove = source.slimeEnergyToMove;

            // Wyvern Data
            wyvernName = source.wyvernName;
            wyvernCharacter = source.wyvernCharacter;
            wyvernSpawnPoint = source.wyvernSpawnPoint;
            wyvernAmountToSpawn = source.wyvernAmountToSpawn;
            wyvernBasehealth = source.wyvernBasehealth;
            wyvernBasestrength = source.wyvernBasestrength;
            wyvernXPValue = source.wyvernXPValue;
            wyvernEnergyToMove = source.wyvernEnergyToMove;

            // SeaSerpent Data
            seaserpentName = source.seaserpentName;
            seaserpentCharacter = source.seaserpentCharacter;
            seaserpentSpawnPoint = source.seaserpentSpawnPoint;
            seaserpentAmountToSpawn = source.seaserpentAmountToSpawn;
            seaserpentBasehealth = source.seaserpentBasehealth;
            seaserpentBasestrength = source.seaserpentBasestrength;
            seaserpentXPValue = source.seaserpentXPValue;
            seaserpentEnergyToMove = source.seaserpentEnergyToMove;

            // Dragon Data
            dragonName = source.dragonName;
            dragonCharacter = source.dragonCharacter;
            dragonSpawnPoint = source.dragonSpawnPoint;
            dragonAmountToSpawn = source.dragonAmountToSpawn;
            dragonBasehealth = source.dragonBasehealth;
            dragonBasestrength = source.dragonBasestrength;
            dragonXPValue = source.dragonXPValue;
            dragonEnergyToMove = source.dragonEnergyToMove;

            // Treasure Chest Data
            treasureName = source.treasureName;
            treasureCharacter = source.treasureCharacter;
            treasureSpawnPoint = source.treasureSpawnPoint;
            treasureBasehealth = source.treasureBasehealth;
            treasureBasestrength = source.treasureBasestrength;
            treasureXPValue = source.treasureXPValue;
            treasureEnergyToMove = source.treasureEnergyToMove;

            maxEnemies = CalculateMaxEnemies();
        }

        private int CalculateMaxEnemies()
        {
            int value = slimeAmountToSpawn + wyvernAmountToSpawn + seaserpentAmountToSpawn + dragonAmountToSpawn + treasureChestAmountToSpawn;
            return value;
        }

        // Maps Information
        public const string worldMap = "Maps_and_Overlays/OverworldMap_01.txt";
        public const string borders = "Maps_and_Overlays/HudBorders.txt";

        // Map Colours
        public const ConsoleColor mountainColor = ConsoleColor.Gray;
        public const ConsoleColor grassColor = ConsoleColor.Green;
        public const ConsoleColor waterColor = ConsoleColor.Blue;
        public const ConsoleColor forestColor = ConsoleColor.DarkGreen;
        public const ConsoleColor sandColor = ConsoleColor.Yellow;
        public const ConsoleColor dungeonEntrance = ConsoleColor.DarkGray;
        public const ConsoleColor castleEntrance = ConsoleColor.Magenta;
        public const ConsoleColor backgroundColor = ConsoleColor.Black;

        // Item Information
        public const int potionHealAmount = 10;
    }
    
}
