// See https://aka.ms/new-console-template for more information
using Devoralime.Core.Interfaces;

public class Output : IOutput
{
    public void Write(string value)
    {
        Console.WriteLine(value);
    }
}