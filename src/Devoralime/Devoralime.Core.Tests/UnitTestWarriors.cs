namespace Devoralime.Core.Tests;

public class UnitTestWarriors
{
    [Theory]
    [InlineData(typeof(Archer), 100)]
    [InlineData(typeof(Swordsman), 120)]
    [InlineData(typeof(Horseman), 150)]
    public void TestWarriorsDefaultLifeStrength(Type warriorType, int defaultStrength)
    {
        //Arrange & Act
        IWarrior warrior = Activator.CreateInstance(warriorType) as IWarrior;

        //Assert
        Assert.Equal(defaultStrength, warrior.LifeStrength);
    }

    [Fact]
    public void TestWarriorsRegenerateLifeNotAtMaxStrength()
    {
        //Arrange
        Warrior warrior = new Archer();        
        warrior.LifeStrength = warrior.LifeStrength - 20;
        var warriorStrength = warrior.LifeStrength;

        //Act
        warrior.Regenerate();

        //Assert
        Assert.True(warrior.LifeStrength > warriorStrength);
    }

    [Fact]
    public void TestWarriorsRegenerateLifeToMaxStrength()
    {
        //Arrange
        Warrior warrior = new Archer();
        var warriorDefaultrStrength = warrior.LifeStrength;
        warrior.LifeStrength = warrior.LifeStrength - 20;
        

        //Act
        warrior.Regenerate();
        warrior.Regenerate();
        warrior.Regenerate();

        //Assert
        Assert.Equal(warriorDefaultrStrength, warrior.LifeStrength);
    }

    [Fact]
    public void TestWarriorsPrepareToFight()
    {
        //Arrange
        Warrior warrior = new Archer();
        var warriorOriginalStrength = warrior.LifeStrength;
        var warriorExpectedStrength = warrior.LifeStrength/2;

        //Act
        warrior.PrepareToFight();

        //Assert
        Assert.Equal(warriorOriginalStrength, warrior.LifeStrengthBeforeFight);
        Assert.Equal(warriorExpectedStrength, warrior.LifeStrength);
    }

    [Fact]
    public void TestWarriorsWasKilled()
    {
        //Arrange
        Warrior warrior = new Archer();        

        //Act
        warrior.WasKilled();

        //Assert
        Assert.Equal(0, warrior.LifeStrength);
    }

    [Theory]
    [InlineData(typeof(Archer))]
    [InlineData(typeof(Swordsman))]
    [InlineData(typeof(Horseman))]
    public void TestWarriorCanNotFightAgain(Type warriorType)
    { 
        //Arrange
        Warrior worrior = Activator.CreateInstance(warriorType) as Warrior;
        worrior.LifeStrength = worrior.MaxLifeStrength / 4 - 1;

        //Act
        var canFightAgain = worrior.CanFightAgain();

        //Arrange
        Assert.False(canFightAgain);

    }

    [Theory]
    [InlineData(typeof(Archer))]
    [InlineData(typeof(Swordsman))]
    [InlineData(typeof(Horseman))]
    public void TestWarriorCanFightAgain(Type warriorType)
    {
        //Arrange
        Warrior worrior = Activator.CreateInstance(warriorType) as Warrior;
        worrior.LifeStrength = worrior.MaxLifeStrength / 4;

        //Act
        var canFightAgain = worrior.CanFightAgain();

        //Arrange
        Assert.True(canFightAgain);
    }
}


