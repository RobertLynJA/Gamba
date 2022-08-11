using Gamba.Games;
using Newtonsoft.Json.Linq;
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
        private Mock<Random> _randomMock;

        [SetUp]
        public void Setup()
        {
            _randomMock = new Mock<Random>();
            _roulette = new Roulette(_randomMock.Object);
        }

        [Test]
        public void GetValidBets_AllValidBetsByOdds()
        {
            var actual = _roulette.GetValidBets();

            var expected = new int[] { 1, 3, 5, 10, 20 };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNextDraw_Random_ValidBet()
        {
            var randomToResults = new (int Random, int Result)[] { (1, 1), (2, 1), (3, 1), (4, 1), (5, 1), (6, 1), (7, 1), (8, 1), (9, 1), (10, 1),
                (11, 1), (12, 1), (13, 3), (14, 3), (15, 3),(16, 3), (17, 3), (18, 3), (19, 5), (20, 5),(21, 5), (22, 5), (23, 10), (24, 10), (25, 20)};

            for (var i = 0; i < randomToResults.Count(); i++)
            {
                _randomMock.Reset();
                _randomMock.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(randomToResults[i].Random);

                var result = _roulette.GetNextDraw();

                Assert.That(result, Is.EqualTo(randomToResults[i].Result), $"i: {i} Random: {randomToResults[i].Random} - Result: {result}", new int[] { randomToResults[i].Random });
            }
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

        [Test]
        public void GetWinReturn_Draw_ReturnsResult()
        {
            var drawsToResults = new (int Draw, int Result)[] { (1, 2), (3, 4), (5, 6), (10, 11), (20, 21) };
            
            for (var i = 0; i < drawsToResults.Count(); i++)
            {
                var result = _roulette.GetWinReturn(drawsToResults[i].Draw);

                Assert.That(result, Is.EqualTo(drawsToResults[i].Result), $"i: {i} Draw: {drawsToResults[i].Draw} - Result: {result}", new int[] { drawsToResults[i].Draw });
            }
        }
    }
}
