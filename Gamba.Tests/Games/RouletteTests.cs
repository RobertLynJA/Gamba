using Gamba.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Tests.Games
{
    internal class RouletteTests
    {
        private Roulette _roulette;
        private Mock<RouletteWheel> _rouletteWheel;

        [SetUp]
        public void Setup()
        {
            _rouletteWheel = new Mock<RouletteWheel>();
            _roulette = new Roulette(1000, _rouletteWheel.Object);
        }

        [Test]
        public void Test1()
        {

        }
    }
}
