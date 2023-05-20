namespace Devoralime.Core.Models;

public abstract class Warrior : IWarrior
{
    protected internal int MaxLifeStrength { get; set; }
    public Guid Id { get; } = Guid.NewGuid();
    public int LifeStrength { get; protected internal set; }
    protected internal int LifeStrengthBeforeFight { get; set; }

    protected Warrior(int maxLifeStrength)
    {
        MaxLifeStrength = maxLifeStrength;
        LifeStrength = maxLifeStrength;
    }

    public virtual void Regenerate()
    {
        LifeStrength = LifeStrength + 10;            

        if (LifeStrength > MaxLifeStrength)
        {
            LifeStrength = MaxLifeStrength;
        }
    }

    public virtual void PrepareToFight()
    {
        LifeStrengthBeforeFight = LifeStrength;
        LifeStrength = LifeStrength / 2;
    }

    public virtual void WasKilled()
    {
        LifeStrength = 0;
    }

    public virtual bool CanFightAgain()
    {
        return LifeStrength >= MaxLifeStrength / 4;
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} Id: {Id} Strength: {LifeStrengthBeforeFight}->{LifeStrength}";
    }
}