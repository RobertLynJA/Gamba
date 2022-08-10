using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Games
{
    public class Roulette
    {
        private Random _random;
        private int[] _bets;

        public Roulette(Random? random = null)
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
    }
}
