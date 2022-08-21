using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Games
{
    public class Roulette
    {
        #region "Fields"
        private int _wallet;
        private RouletteWheel _wheel;
        private int _maxWallet;
        private int _minWallet;
        private Dictionary<Draw, long> _drawAmounts = new Dictionary<Draw, long>();
        private long _totalDraws = 0;
        private long _totalLosses = 0;
        private long _totalWins = 0;
        private Result? _lastResult;
        #endregion

        #region "Properties"        
        public int Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public int MinWallet
        {
            get
            {
                return _minWallet;
            }
        }

        public int MaxWallet
        {
            get
            {
                return _maxWallet;
            }
        }

        public long TotalDraws
        {
            get
            {
                return _totalDraws;
            }
        }

        public long TotalWins
        {
            get
            {
                return _totalWins;
            }
        }

        public long TotalLosses
        {
            get
            {
                return _totalLosses;
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

        public Roulette(int startingCurrency, RouletteWheel? wheel = null)
        {
            if (startingCurrency < 0)
                throw new ArgumentOutOfRangeException($"{nameof(startingCurrency)} cannot be negative");

            _wallet = startingCurrency;
            _wheel = wheel ??= new RouletteWheel();
            _maxWallet = startingCurrency;
            _minWallet = startingCurrency;

            foreach (var draw in (Draw[])Enum.GetValues(typeof(Draw)))
            {
                _drawAmounts[draw] = 0;
            }
        }

        private void SetMaxAndMin()
        {
            _maxWallet = Math.Max(_maxWallet, _wallet);
            _minWallet = Math.Min(_minWallet, _wallet);
        }

        public virtual Result Bet(Draw draw, int amount)
        {
            if (amount > _wallet)
                throw new InvalidOperationException("Not enough currency for bet");

            if (amount < 0)
                throw new ArgumentOutOfRangeException($"{nameof(amount)} cannot be negative");

            var roll = _wheel.GetNextDraw();
            var result = roll == (int)draw;

            _drawAmounts[(Draw)roll]++;

            _wallet -= amount;

            if (roll == (int)draw)
            {
                _wallet += amount * _wheel.GetWinReturn((int)draw);
            }

            if (result)
            {
                _totalWins++;
            }
            else
            {
                _totalLosses++;
            }

            SetMaxAndMin();
            _totalDraws++;

            return _lastResult = new Result(result, result ? amount * _wheel.GetWinReturn((int)draw) : 0, _wallet, (Draw)roll, amount, draw);
        }

        public virtual long GetTotalDraws(Draw draw)
        {
            return _drawAmounts[draw];
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
}
