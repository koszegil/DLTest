// See https://aka.ms/new-console-template for more information
using Devoralime.Core;

var numberOfFightersArray = Environment.GetCommandLineArgs();

if (numberOfFightersArray.Length > 2 || !int.TryParse(numberOfFightersArray[1], out var numberOfFighters))
{
    throw new ArgumentOutOfRangeException();
}

var arena = new Arena(new Randomizer(), new Output());
arena.GenerateWarriors(numberOfFighters);
var winner = arena.FightToTheEnd();
Console.WriteLine("--------------Winner------------");
Console.WriteLine($"Winner is: {winner?.Id.ToString() ?? "Nobody"}");
