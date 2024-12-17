using System;
using System.Collections.Generic;

namespace DiceRoller.Services;

#if (HOWTO)
// HOW-TO: This is an example of a "Service" class. Services are used to encapsulate logic and data for a specific purpose.
// In this example, the DiceService is used to encapsulate the logic for rolling dice.
// This is a great way to keep your code organized and easy to maintain.
// Although this is a rather simple example, it is typically where you will see modders running Queries and manipulating with Entities. 
// For a large source of Service examples, see: https://github.com/Odjit/KindredCommands/tree/main/Services
#endif 
internal class DiceService
{
    public static List<int> ValidDice;

    public DiceService()
    {
        ValidDice = new List<int> { 4, 6, 8, 10, 12, 20, 100 };
    }

    public static bool IsValidDie(int die)
    {
        return ValidDice.Contains(die);
    }

    public static int RollDie(int die)
    {
        return new Random().Next(1, die + 1);
    }

    public static int RollDice(int die, int count)
    {
        int total = 0;
        for (int i = 0; i < count; i++)
        {
            total += RollDie(die);
        }
        return total;
    }

    public static bool ValidateHandOfDice(string hand, out int die, out int amount)
    {
        die = 0;
        amount = 0;

        if (string.IsNullOrEmpty(hand)) return false;
        if (!hand.Contains('d')) return false;

        string[] parts = hand.ToLower().Split('d');
        if (parts.Length != 2) return false;

        if (!int.TryParse(parts[0], out amount) || !int.TryParse(parts[1], out die))
            return false;

        if (amount <= 0) return false;

        return IsValidDie(die);
    }
}