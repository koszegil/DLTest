// See https://aka.ms/new-console-template for more information
using Devoralime.Core.Interfaces;

public class Randomizer : IRandomizer
{
    readonly Random random = new Random(DateTime.Now.Microsecond);

    public int Next(int min, int max)
    {
        return random.Next(min, max);
    }
}
