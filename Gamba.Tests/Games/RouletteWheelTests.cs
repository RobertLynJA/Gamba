using Gamba.Games;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Tests.Games;

internal class RouletteWheelTests
{
    private RouletteWheel _roulette;
    private Mock<Random> _randomMock;

    [SetUp]
    public void Setup()
    {
        _randomMock = new Mock<Random>();
        _roulette = new RouletteWheel(_randomMock.Object);
    }

    [Test]
    public void GetValidBets_AllValidBetsByOdds()
    {
        var actual = _roulette.GetValidBets();

        var expected = new int[] { 1, 3, 5, 10, 20 };

        CollectionAssert.AreEqual(expected, actual);
    }

    [TestCase(1, ExpectedResult = Draw.One)]
    [TestCase(2, ExpectedResult = Draw.One)]
    [TestCase(3, ExpectedResult = Draw.One)]
    [TestCase(4, ExpectedResult = Draw.One)]
    [TestCase(5, ExpectedResult = Draw.One)]
    [TestCase(6, ExpectedResult = Draw.One)]
    [TestCase(7, ExpectedResult = Draw.One)]
    [TestCase(8, ExpectedResult = Draw.One)]
    [TestCase(9, ExpectedResult = Draw.One)]
    [TestCase(10, ExpectedResult = Draw.One)]
    [TestCase(11, ExpectedResult = Draw.One)]
    [TestCase(12, ExpectedResult = Draw.One)]
    [TestCase(13, ExpectedResult = Draw.Three)]
    [TestCase(14, ExpectedResult = Draw.Three)]
    [TestCase(15, ExpectedResult = Draw.Three)]
    [TestCase(16, ExpectedResult = Draw.Three)]
    [TestCase(17, ExpectedResult = Draw.Three)]
    [TestCase(18, ExpectedResult = Draw.Three)]
    [TestCase(19, ExpectedResult = Draw.Five)]
    [TestCase(20, ExpectedResult = Draw.Five)]
    [TestCase(21, ExpectedResult = Draw.Five)]
    [TestCase(22, ExpectedResult = Draw.Five)]
    [TestCase(23, ExpectedResult = Draw.Ten)]
    [TestCase(24, ExpectedResult = Draw.Ten)]
    [TestCase(25, ExpectedResult = Draw.Twenty)]
    public Draw GetNextDraw_Random_ValidBet(int randomValue)
    {
        _randomMock.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(randomValue);

        var result = _roulette.GetNextDraw();

        return result;
    }

    [Test]
    public void GetNextDraw_CallsRandom()
    {
        var result = _roulette.GetNextDraw();

        _randomMock.Verify(r => r.Next(1, 26));
    }

    [Test]
    public void GetWinReturn_InvalidDraw_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _roulette.GetWinReturn(0));
    }

    [TestCase(Draw.One, ExpectedResult = 2)]
    [TestCase(Draw.Three, ExpectedResult = 4)]
    [TestCase(Draw.Five, ExpectedResult = 6)]
    [TestCase(Draw.Ten, ExpectedResult = 11)]
    [TestCase(Draw.Twenty, ExpectedResult = 21)]
    public int GetWinReturn_Draw_ReturnsResult(Draw draw)
    {
        var result = _roulette.GetWinReturn(draw);

        return result;
    }
}
