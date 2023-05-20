namespace Devoralime.Core.Tests;

public class UnitTestMatch    {

    public enum KilledInCombat
    { 
        None, 
        Attacker, 
        Defender
    }


    [Theory]
    [InlineData(typeof(Archer), typeof(Archer), KilledInCombat.Defender)]
    [InlineData(typeof(Archer), typeof(Swordsman), KilledInCombat.Defender)]
    [InlineData(typeof(Archer), typeof(Horseman), KilledInCombat.Defender, 40)]
    [InlineData(typeof(Archer), typeof(Horseman), KilledInCombat.None, 41)]
    [InlineData(typeof(Swordsman), typeof(Horseman), KilledInCombat.None)]
    [InlineData(typeof(Swordsman), typeof(Swordsman), KilledInCombat.Defender)]
    [InlineData(typeof(Swordsman), typeof(Archer), KilledInCombat.Defender)]
    [InlineData(typeof(Horseman), typeof(Horseman), KilledInCombat.Defender)]
    [InlineData(typeof(Horseman), typeof(Swordsman), KilledInCombat.Attacker)]
    [InlineData(typeof(Horseman), typeof(Archer), KilledInCombat.Defender)]
    public void TestFight(Type attackerType, Type defenderType, KilledInCombat killedInCombat, int randomizerResult = 0)
    {
        //Arrange
        var attacker = Activator.CreateInstance(attackerType) as IWarrior;
        var defender = Activator.CreateInstance(defenderType) as IWarrior;
        //Arrange
        var randomizerMock = new Moq.Mock<IRandomizer>();
        randomizerMock.Setup(i => i.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(randomizerResult);
        var match = new Match(attacker, defender, randomizerMock.Object);

        //Act
        match.Fight();

        //Assert
        switch (killedInCombat)
        {
            case KilledInCombat.None:
                Assert.Empty(match.KilledWarriors);
                Assert.Equal(2, match.SurvivedWarriors.Count);
                Assert.Contains(attacker, match.SurvivedWarriors);
                Assert.Contains(defender, match.SurvivedWarriors);
                break;
            case KilledInCombat.Attacker:
                Assert.Single(match.KilledWarriors);
                Assert.Equal(attacker, match.KilledWarriors.First());
                Assert.Single(match.SurvivedWarriors);
                Assert.Equal(defender, match.SurvivedWarriors.First());
                break;
            case KilledInCombat.Defender:
                Assert.Single(match.KilledWarriors);
                Assert.Equal(defender, match.KilledWarriors.First());
                Assert.Single(match.SurvivedWarriors);
                Assert.Equal(attacker, match.SurvivedWarriors.First());
                break;
            default:
                throw new NotImplementedException();
        }
    }

    [Fact]
    public void TestFightersPreparedToFight()
    {
        //Arrange
        var attacker = new Moq.Mock<Archer>();
        var defender = new Moq.Mock<Horseman>();

        //Act
        new Match(attacker.Object, defender.Object, null);

        //Assert
        attacker.Verify(i => i.PrepareToFight());
        defender.Verify(i => i.PrepareToFight());
    }
}
