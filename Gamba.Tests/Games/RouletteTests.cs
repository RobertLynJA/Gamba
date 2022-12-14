using Gamba.Games;

namespace Gamba.Tests.Games;

internal class RouletteTests
{
    private Roulette _roulette;
    private Mock<RouletteWheel> _rouletteWheel;
    private Mock<Random> _randomMock;
    private const int _startingWallet = 1000;

    [SetUp]
    public void Setup()
    {
        _randomMock = new Mock<Random>();
        _rouletteWheel = new Mock<RouletteWheel>(new object[] { _randomMock.Object });
        _roulette = new Roulette(_startingWallet, _rouletteWheel.Object);
    }

    [Test]
    public void Bet_LargerThanWallet_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => _roulette.Bet(Draw.One, _startingWallet + 1));
    }

    [Test(ExpectedResult = 990)]
    public int Bet_LosingAmount_WalletDecreased()
    {
        _rouletteWheel.Setup(r => r.GetNextDraw()).Returns(Draw.Three);

        _roulette.Bet(Draw.One, 10);

        return _roulette.Wallet;
    }

    [Test(ExpectedResult = 1040)]
    public int Bet_WinningAmount_WalletIncreased()
    {
        _rouletteWheel.Setup(r => r.GetNextDraw()).Returns(Draw.One);
        _rouletteWheel.Setup(r => r.GetWinReturn(Draw.One)).Returns(5);

        _roulette.Bet(Draw.One, 10);

        return _roulette.Wallet;
    }
}
