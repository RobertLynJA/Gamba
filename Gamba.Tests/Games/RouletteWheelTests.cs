using Gamba.Games;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Tests.Games
{
    internal class RouletteWheelTests
    {
        private RouletteWheel _roulette;
        private Mock<Random> _randomMock;

        [SetUp]
        public void Setup()
        {
            _randomMock = new Mock<Random>();
            _roulette = new RouletteWheel(_randomMock.Object);
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
            var randomToResults = new (int Random, Draw Result)[] { (1, Draw.One), (2, Draw.One), (3, Draw.One), (4, Draw.One), (5, Draw.One), (6, Draw.One), (7, Draw.One), (8, Draw.One), (9, Draw.One), (10, Draw.One),
                (11, Draw.One), (12, Draw.One), (13, Draw.Three), (14, Draw.Three), (15, Draw.Three),(16, Draw.Three), (17, Draw.Three), (18, Draw.Three), (19, Draw.Five), (20, Draw.Five),(21, Draw.Five), (22, Draw.Five), (23, Draw.Ten), (24, Draw.Ten), (25, Draw.Twenty)};

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
            var drawsToResults = new (Draw Draw, int Result)[] { (Draw.One, 2), (Draw.Three, 4), (Draw.Five, 6), (Draw.Ten, 11), (Draw.Twenty, 21) };
            
            for (var i = 0; i < drawsToResults.Count(); i++)
            {
                var result = _roulette.GetWinReturn(drawsToResults[i].Draw);

                Assert.That(result, Is.EqualTo(drawsToResults[i].Result), $"i: {i} Draw: {drawsToResults[i].Draw} - Result: {result}", new Draw[] { drawsToResults[i].Draw });
            }
        }
    }
}
