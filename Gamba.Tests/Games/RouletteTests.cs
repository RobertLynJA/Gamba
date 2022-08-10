﻿using Gamba.Games;
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
            var randomValue = Enumerable.Range(1, 25).ToList();
            var results = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 5, 5, 5, 5, 10, 10, 20 };

            for (var i = 0; i < randomValue.Count(); i++)
            {
                _randomMock.Reset();
                _randomMock.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(randomValue[i]);

                var result = _roulette.GetNextDraw();

                Assert.That(result, Is.EqualTo(results[i]), $"i: {i} Random: {randomValue[i]} - Result: {result}", new int[] { randomValue[i] });
            }
        }
    }
}