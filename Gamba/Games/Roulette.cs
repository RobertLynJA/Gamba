using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Games
{
    public class Roulette
    {
        private int _wallet;
        private RouletteWheel _wheel;

        public Roulette(int startingCurrency, RouletteWheel? wheel = null)
        {
            if (startingCurrency < 0)
                throw new ArgumentOutOfRangeException($"{nameof(startingCurrency)} cannot be negative");

            _wallet = startingCurrency;
            _wheel = wheel ??= new RouletteWheel();
        }

        public (bool win, int currentWallet, Draw roll) Bet(Draw draw, int amount)
        {
            if (amount < _wallet)
                throw new InvalidOperationException("Not enough currency for bet");

            if (amount < 0)
                throw new ArgumentOutOfRangeException($"{nameof(amount)} cannot be negative");
    
            var roll = _wheel.GetNextDraw();
            var result = roll == (int)draw;

            _wallet -= amount;

            if (roll == (int)draw)
            {
                _wallet += amount * _wheel.GetWinReturn((int)draw);
            }

            return (result, _wallet, (Draw)roll);
        }

        public int Wallet
        {
            get
            {
                return _wallet;
            }
        }
    }
}
