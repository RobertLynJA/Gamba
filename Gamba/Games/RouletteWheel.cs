﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Games
{
    public class RouletteWheel
    {
        private Random _random;
        private int[] _bets;

        public RouletteWheel(Random? random = null)
        {
            _random = random ??= new Random();
            _bets = new int[] { 1, 3, 5, 10, 20 };
        }

        public IEnumerable<int> GetValidBets()
        {
            return _bets.ToList();
        }

        public int GetNextDraw()
        {
            var result = _random.Next(1, 26);

            if (result <= 12) return 1;
            if (result <= 18) return 3;
            if (result <= 22) return 5;
            if (result <= 24) return 10;
            if (result <= 25) return 20;

            throw new Exception("should never happen");
        }

        public int GetWinReturn(int draw)
        {
            switch (draw)
            {
                case 1:
                    return 2;
                case 3:
                    return 4;
                case 5:
                    return 6;
                case 10:
                    return 11;
                case 20:
                    return 21;
            }

            throw new ArgumentOutOfRangeException($"{nameof(draw)} is not a valid draw");
        }
    }
}