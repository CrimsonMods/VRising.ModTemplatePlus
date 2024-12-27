using VTest.Services;
using VampireCommandFramework;

namespace VTest.Commands;

[CommandGroup("dice")]
internal static class DiceCommands
{
    [Command(name: "roll", shortHand: "r", description: "Roll dice", adminOnly: false)]
    public static void Roll(ChatCommandContext ctx, string dies = "1d20")
    {
        if (!DiceService.ValidateHandOfDice(dies, out int die, out int amount))
        {
            ctx.Reply("Invalid dice format. Use the format 'AMOUNTdDIE' (e.g. 1d20)");
            return;
        }

        int result = DiceService.RollDice(die, amount);

        ctx.Reply($"{ctx.User.CharacterName} Rolled {amount}d{die} = {result}");
    }
}