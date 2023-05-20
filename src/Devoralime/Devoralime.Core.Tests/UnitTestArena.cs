namespace Devoralime.Core.Tests;
public class UnitTestArena
{
    [Theory]
    [InlineData(typeof(Archer), 0)]
    [InlineData(typeof(Swordsman), 1)]
    [InlineData(typeof(Horseman), 2)]        
    public void TestGenerateWarriors(Type warriorType, int random)
    {
        //Arrange
        var randomizerMock = new Moq.Mock<IRandomizer>();
        randomizerMock.Setup(i => i.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(random);

        var arena = new Arena(randomizerMock.Object, null);

        //Act
        arena.GenerateWarriors(10);

        //Assert
        Assert.Equal(10, arena.Warriors.Count);
        Assert.True(arena.Warriors.All(i => i.Value.GetType().Equals(warriorType)));
    }

    [Fact]
    public void TestFightToTheEnd()
    {
        //Arrange
        var randomizerMock = new Moq.Mock<IRandomizer>();
        randomizerMock
            .Setup(i => i.Next(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((int min, int max) => new Random(DateTime.Now.Millisecond).Next(min, max));
        var outputMock = new Moq.Mock<IOutput>();            
        var arena = new Arena(randomizerMock.Object, outputMock.Object);
        arena.GenerateWarriors(4);

        //Act
        var winnerWarrior = arena.FightToTheEnd();

        //Assert
        Assert.True(arena.Warriors.Count <= 1);
        if (arena.Warriors.Count == 1)
        {
            Assert.Equal(winnerWarrior, arena.Warriors.Values.First());
        }
        outputMock.Verify(i => i.Write(It.IsAny<string>()));
    }
}