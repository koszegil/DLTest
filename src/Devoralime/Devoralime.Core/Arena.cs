namespace Devoralime.Core;

public class Arena
{
    private readonly IRandomizer randomizer;
    private readonly IOutput output;

    protected internal Dictionary<Guid, IWarrior> Warriors { get; set; } = new Dictionary<Guid, IWarrior>();

    public Arena(IRandomizer randomizer, IOutput output)
    {
        this.randomizer = randomizer;
        this.output = output;
    }

    public void GenerateWarriors(int numberOfWarriorsToGenerate)
    {
        for (var i = 0; i < numberOfWarriorsToGenerate; i++)
        {
            IWarrior? warrior = null;
            switch (randomizer.Next(0, 100)%3)
            {
                case 0:
                    warrior = new Archer();
                    break;
                case 1:
                    warrior = new Swordsman();
                    break;
                default:
                    warrior = new Horseman();
                    break;
            }

            Warriors.Add(warrior.Id, warrior);
        }
    }

    public IWarrior? FightToTheEnd()
    {
        while (Warriors.Count > 1)
        {
            IWarrior attacker, defender;

            ChoosesWarriorsToFight(out attacker, out defender);

            RegenerateNonFightingWarriors(attacker, defender);            

            var match = new Match(attacker, defender, randomizer);
            match.Fight();

            CheckedSurvivedWarriors(match.SurvivedWarriors);

            RemoveKilledWarriors(match.KilledWarriors);

            output.Write("------------New Fight-----------");
            output.Write($"Attacker: {attacker}");
            output.Write($"Defender: {defender}");
        }

        return Warriors.Values.FirstOrDefault();
    }

    private void RemoveKilledWarriors(List<IWarrior> killedWarriors)
    {
        foreach (IWarrior warrior in killedWarriors)
        {
            warrior.WasKilled();
            Warriors.Remove(warrior.Id);
        }
    }

    private void CheckedSurvivedWarriors(List<IWarrior> survivedWarriors)
    {   
        foreach (IWarrior warrior in survivedWarriors)
        {            
            if (!warrior.CanFightAgain())
            {                
                Warriors.Remove(warrior.Id);
            }
        }
    }

    private void RegenerateNonFightingWarriors(IWarrior attacker, IWarrior defender)
    {
        foreach (var warrior in Warriors.Values)
        {
            if (warrior != attacker && warrior != defender)
            {
                warrior.Regenerate();
            }
        }
    }

    private void ChoosesWarriorsToFight(out IWarrior attacker, out IWarrior defender)
    {
        attacker = Warriors.Values.ElementAt(this.randomizer.Next(0, Warriors.Count));
        do
        {
            defender = Warriors.Values.ElementAt(this.randomizer.Next(0, Warriors.Count));

        } while (attacker == defender);
    }
}