namespace Devoralime.Core;

internal class Match
{
    private readonly IWarrior attacker;
    private readonly IWarrior defender;
    private readonly IRandomizer randomizer;

    public List<IWarrior> KilledWarriors { get; private set; } = new List<IWarrior>();
    public List<IWarrior> SurvivedWarriors { get; private set; } = new List<IWarrior>();

    public Match(IWarrior attacker, IWarrior defender, IRandomizer randomizer)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.randomizer = randomizer;

        attacker.PrepareToFight();
        defender.PrepareToFight();
    }

    public void Fight()
    {
        switch (attacker)
        {
            case Archer archerAttacker:
                switch (defender)
                {
                    case Archer archerDefender:
                    case Swordsman swordsmanDefender:                        
                        KilledWarriors.Add(defender);
                        SurvivedWarriors.Add(attacker);
                        break;
                    case Horseman horsemanDefender:
                        if (randomizer.Next(0, 100) > 40)
                        {
                            SurvivedWarriors.Add(defender);
                            SurvivedWarriors.Add(attacker);
                        }
                        else
                        {
                            KilledWarriors.Add(defender);
                            SurvivedWarriors.Add(attacker);
                        }
                        break;
                }
                break;
            case Swordsman swordsmanAttacker:
                switch (defender)
                {
                    case Archer archerDefender:
                    case Swordsman swordsmanDefender:
                        KilledWarriors.Add(defender);
                        SurvivedWarriors.Add(attacker);
                        break;
                    case Horseman horsemanDefender:
                        SurvivedWarriors.Add(defender);
                        SurvivedWarriors.Add(attacker);
                        break;
                }
                break;
            case Horseman horsemanAttacker:
                switch (defender)
                {
                    case Archer archerDefender:
                    case Horseman horsemanDefender:
                        KilledWarriors.Add(defender);
                        SurvivedWarriors.Add(attacker);                            
                        break;
                    case Swordsman swordsmanDefender:
                        KilledWarriors.Add(attacker);
                        SurvivedWarriors.Add(defender);
                        break;                        
                }
                break;                
        }
    }

    
    
}