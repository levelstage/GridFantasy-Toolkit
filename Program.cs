using System;
using System.Collections.Generic;
using GfEngine.Core;
using GfEngine.Stages;
using GfEngine.Behaviors;
using GfEngine.Models.Statuses;
using GfEngine.Models.Actors;
public class Program
{
    // A helper function to parse coordinates like "A1", "F5", etc.
    private static (int, int) ParseCoordinate(string input, int mapSizeY)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 2)
            return (-1, -1);

        input = input.ToLower();
        int x = input[0] - 'a';
        
        if (!int.TryParse(input.Substring(1), out int yNum))
            return (-1, -1);
            
        int y = mapSizeY - yNum;

        if (x < 0 || x >= mapSizeY || y < 0 || y >= mapSizeY)
            return (-1, -1);

        return (x, y);
    }

    public static void Main(string[] args)
    {
        // 1. Game Initialization
        Wave currentWave = new Wave();
        var map = currentWave.Map;
        int mapSize = map.GetLength(0);
        string[,] closerMatrix = new string[mapSize, mapSize];
        ResetCloserMatrix(closerMatrix);

        Square selectedSquare = null;
        Behavior selectedBehavior = null;
        List<UnitAction> possibleActions = null;

        // 2. Main Game Loop
        while (true)
        {
            // Display the current game state
            DisplayMap(map, closerMatrix);

            if (selectedSquare != null && selectedSquare.Occupant != null)
            {
                DisplaySquareInfo(selectedSquare, mapSize);
            }

            // --- State Machine for Player Actions ---

            // STATE 1: No unit or action is selected. Waiting for the player to choose a unit.
            if (selectedSquare == null)
            {
                Console.WriteLine(GameData.Text.Get(GameData.Text.Key.UI_SelectSquare) + " (e.g., 'F3')");
                string input = Console.ReadLine();
                var (x, y) = ParseCoordinate(input, mapSize);

                if (x == -1)
                {
                    Console.WriteLine(GameData.Text.Get(GameData.Text.Key.UI_InvalidCoordinate));
                    Console.ReadLine();
                    continue;
                }

                Square targetSquare = map[y, x];
                if (targetSquare.Occupant != null && targetSquare.Occupant.Team == Teams.Players)
                {
                    selectedSquare = targetSquare; // Unit selected! Move to next state.
                }
                else
                {
                    DisplaySquareInfo(targetSquare, mapSize); // Just show info for non-player squares
                    Console.ReadLine();
                }
            }
            // STATE 2: A unit is selected. Waiting for the player to choose a behavior (Move/Attack).
            else if (selectedBehavior == null)
            {
                Console.WriteLine(GameData.Text.Get(GameData.Text.Key.UI_ChooseAction));
                var unitBehaviors = selectedSquare.Occupant.Behaviors;
                for (int i = 0; i < unitBehaviors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {unitBehaviors[i].Name} (AP: {unitBehaviors[i].ApCost})");
                }
                Console.WriteLine("0. Cancel Selection");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice > 0 && choice <= unitBehaviors.Count)
                {
                    selectedBehavior = unitBehaviors[choice - 1];
                    possibleActions = selectedBehavior.ActionSearcher(selectedSquare, map, selectedBehavior);
                    HighlightForConsole(possibleActions, closerMatrix); // Highlight the map and move to next state.
                }
                else
                {
                    selectedSquare = null; // Cancel and return to State 1
                }
            }
            // STATE 3: A behavior is selected. Waiting for the player to choose a target square.
            else
            {
                Console.WriteLine($"Target for {selectedBehavior.Name}? (Enter coordinate, or '0' to cancel)");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    selectedBehavior = null;
                    possibleActions = null;
                    ResetCloserMatrix(closerMatrix); // Cancel and return to State 2
                    continue;
                }

                var (x, y) = ParseCoordinate(input, mapSize);
                if (x == -1)
                {
                    Console.WriteLine(GameData.Text.Get(GameData.Text.Key.UI_InvalidCoordinate));
                    Console.ReadLine();
                    continue;
                }

                // Check if the chosen target is a valid, accessible action
                UnitAction chosenAction = possibleActions.Find(action => action.X == x && action.Y == y);
                if (chosenAction != null && chosenAction.Type == ActionType.Accessible)
                {
                    // Execute the action!
                    Square targetSquare = map[y, x];
                    string resultMessage = selectedBehavior.Excute(selectedSquare, targetSquare);
                    
                    if (!string.IsNullOrEmpty(resultMessage))
                    {
                        Console.Write("\x1b[3J\x1b[H\x1b[2J");
                        Console.WriteLine(resultMessage);
                        Console.WriteLine(GameData.Text.Get(GameData.Text.Key.UI_PressEnterToContinue));
                        Console.ReadLine();
                    }
                    
                    // Reset everything for the next turn/action
                    selectedSquare = null;
                    selectedBehavior = null;
                    possibleActions = null;
                    ResetCloserMatrix(closerMatrix);
                }
                else
                {
                    Console.WriteLine("You can't perform that action on that square. Try again.");
                    Console.ReadLine();
                }
            }
        }
    }
	public static void HighlightForConsole(List<UnitAction> actions, string[,] cmatrix)
	{
		foreach (var action in actions)
		{
			int x = action.X;
			int y = action.Y;

			if (action.Type == ActionType.Unaccessible)
			{
				cmatrix[y, x] = "*";
			}
			else if (action.Type == ActionType.Accessible)
			{
				cmatrix[y, x] = "$";
			}
		}
	}
    
  // (DisplaySquareInfo 와 DisplayMap 함수는 이전과 동일합니다)
  public static void DisplaySquareInfo(Square square, int map_ysize)
  {
    char coordX = (char)('A' + square.X);
    int coordY = map_ysize - square.Y;
   
    Console.WriteLine($"\n--- 선택된 칸 정보 ({coordX}{coordY}) ---");
    Console.WriteLine($"지형: {square.Terrain}");
    if (square.Occupant == null)
    {
        Console.WriteLine("유닛 없음");
    }
    else
    {
        Unit unit = square.Occupant;
        Status finalStat = unit.LiveStat.Buffed();
        Console.WriteLine("\n[유닛 정보]");
	    Console.WriteLine($"이름: {unit.Name}");
        Console.WriteLine($"소속: {unit.Team}");
        Console.WriteLine($"클래스: {unit.MoveClass}");
		Console.WriteLine($"무기: {unit.WeaponClass}");
        Console.WriteLine($"HP: {unit.LiveStat.CurrentHp} / {finalStat.MaxHp}");
        Console.WriteLine($"공격력: {finalStat.Attack} | 마법공격력: {finalStat.MagicAttack}");
        Console.WriteLine($"방어력: {finalStat.Defense} | 마법방어력: {finalStat.MagicDefense}");
        Console.WriteLine($"민첩: {finalStat.Agility}");
    }
        Console.WriteLine("--------------------------");
  }

  public static void DisplayMap(Square[,] map, string[,] cmatrix)
  {
		int xsize, ysize;
		ysize = map.GetLength(0);
		xsize = map.GetLength(1);
        Console.Write("\x1b[3J\x1b[H\x1b[2J");
        Console.WriteLine("\n  A  B  C  D  E  F  G  H");
        for (int i = 0; i < ysize; i++)
        {
            Console.Write(ysize - i);
            for (int j = 0; j < xsize; j++)
            {
                Console.Write(map[i, j].ToString() + cmatrix[i,j]);
            }
            Console.WriteLine();
        }
  }
	
	public static void ResetCloserMatrix(string[,] cmatrix)
    {
        for (int i = 0; i < cmatrix.GetLength(0); i++)
        {
             for (int j = 0; j < cmatrix.GetLength(1); j++)
                {
                    cmatrix[i, j] = "]";
                }
        }
    }
}


