namespace Devoralime.Core.Interfaces;

public interface IWarrior
{
    Guid Id { get; }
    int LifeStrength { get; }

    bool CanFightAgain();
    void PrepareToFight();
    void Regenerate();
    void WasKilled();
}