using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Games
{
    public class RouletteStats
    {
        private int _maxWallet;
        private int _minWallet;
        private Dictionary<Draw, long> _drawAmounts = new Dictionary<Draw, long>();
        private long _totalDraws = 0;
        private long _totalLosses = 0;
        private long _totalWins = 0;
        private int _wallet;

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

        public int Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public virtual long GetTotalDraws(Draw draw)
        {
            return _drawAmounts[draw];
        }

        private void SetMaxAndMin()
        {
            _maxWallet = Math.Max(_maxWallet, _wallet);
            _minWallet = Math.Min(_minWallet, _wallet);
        }

        public void AddBet(bool result, Draw roll, int betAmount, int winReturn)
        {
            _wallet -= betAmount;

            var winAmount = betAmount * winReturn;

            if (result)
            {
                _wallet += winAmount;
                _totalWins++;
            }
            else
            {
                _totalLosses++;
            }

            _drawAmounts[roll] += 1;
            _totalDraws++;
            SetMaxAndMin();
        }

        public RouletteStats(int startingWallet)
        {
            _wallet = startingWallet;
            _minWallet = startingWallet;
            _maxWallet = startingWallet;

            foreach (var draw in (Draw[])Enum.GetValues(typeof(Draw)))
            {
                _drawAmounts[draw] = 0;
            }
        }
    }
}
