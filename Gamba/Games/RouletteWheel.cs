namespace Gamba.Games;

public class RouletteWheel
{
    private Random _random;
    private int[] _bets;

    public RouletteWheel(Random? random = null)
    {
        _random = random ?? new Random();
        _bets = new int[] { 1, 3, 5, 10, 20 };
    }

    public virtual IEnumerable<int> GetValidBets()
    {
        return _bets.ToList();
    }

    public virtual Draw GetNextDraw()
    {
        var result = _random.Next(1, 26);

        if (result <= 12) return Draw.One;
        if (result <= 18) return Draw.Three;
        if (result <= 22) return Draw.Five;
        if (result <= 24) return Draw.Ten;
        if (result <= 25) return Draw.Twenty;

        throw new Exception("should never happen");
    }

    public virtual int GetWinReturn(Draw draw)
    {
        switch (draw)
        {
            case Draw.One:
                return 2;
            case Draw.Three:
                return 4;
            case Draw.Five:
                return 6;
            case Draw.Ten:
                return 11;
            case Draw.Twenty:
                return 21;
        }

        throw new ArgumentOutOfRangeException(nameof(draw), "Not a valid draw");
    }
}
