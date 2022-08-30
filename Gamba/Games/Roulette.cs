using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Games;

public class Roulette
{
    #region "Fields"
    private readonly RouletteWheel _wheel;
    private Result? _lastResult;
    private readonly RouletteStats _stats;
    #endregion

    #region "Properties"        
    public int Wallet
    {
        get
        {
            return _stats.Wallet;
        }
    }

    public int MinWallet
    {
        get
        {
            return _stats.MinWallet;
        }
    }

    public int MaxWallet
    {
        get
        {
            return _stats.MaxWallet;
        }
    }

    public long TotalDraws
    {
        get
        {
            return _stats.TotalDraws;
        }
    }

    public long TotalWins
    {
        get
        {
            return _stats.TotalWins;
        }
    }

    public long TotalLosses
    {
        get
        {
            return _stats.TotalLosses;
        }
    }

    public Result LastResult
    {
        get
        {
            if (_lastResult == null)
                throw new InvalidOperationException("Need to call Draw() at least once before results are published");

            return _lastResult;
        }
    }
    #endregion

    public Roulette(int startingCurrency, RouletteWheel? wheel = null, RouletteStats? stats = null)
    {
        if (startingCurrency < 0)
            throw new ArgumentOutOfRangeException($"{nameof(startingCurrency)}", "Parameter cannot be negative");

        _wheel = wheel ?? new RouletteWheel();
        _stats = stats ?? new RouletteStats(startingCurrency);
    }

    public virtual Result Bet(Draw draw, int amount)
    {
        if (amount > _stats.Wallet)
            throw new InvalidOperationException("Not enough currency for bet");

        if (amount < 0)
            throw new ArgumentOutOfRangeException($"{nameof(amount)}", "Parameter cannot be negative");

        var roll = _wheel.GetNextDraw();
        var result = roll == draw;
        var winReturn = _wheel.GetWinReturn(draw);

        _stats.AddBet(result, roll, amount, winReturn);

        return _lastResult = new Result(result, result ? amount * _wheel.GetWinReturn(draw) : 0, _stats.Wallet, (Draw)roll, amount, draw);
    }

    public virtual long GetTotalDraws(Draw draw)
    {
        return _stats.GetTotalDraws(draw);
    }

    #region "Result"
    public class Result 
    {
        public readonly bool Won;
        public readonly int AmountWon;
        public readonly int CurrentWallet;
        public readonly Draw Roll;
        public readonly int Wager;
        public readonly Draw Bet;

        public Result(bool won, int amountWon, int currentWallet, Draw roll, int wager, Draw bet)
        {
            Won = won;
            AmountWon = amountWon;
            CurrentWallet = currentWallet;
            Roll = roll;
            Wager = wager;
            Bet = bet;
        }
    }
    #endregion
}
