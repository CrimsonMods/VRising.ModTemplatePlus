using DiceRoller.Services;
using VampireCommandFramework;

namespace DiceRoller.Commands;

[CommandGroup("dice")]
internal static class DiceCommands
{
#if (HOWTO)
    // HOW-TO: This is a Vampire Command Framework "Command". 
    // You can see it has a name, a shorter name, description and it is set to be executed by anyone (not just admins).
    // Someone could call this like ".roll 1d20" in the chat to execute the code inside. 

    // Note: Notice how the Command attribute has no included parameters, but the method does.
    // When using VCF all parameters must be entered into chat in order. Any parameters that are defaulted to a value will be optional. 
    // For example, you could also run this command by typing ".roll" and it would default to 1d20.

    // To learn more about VCF, see: https://github.com/deca/VampireCommandFramework
#endif
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