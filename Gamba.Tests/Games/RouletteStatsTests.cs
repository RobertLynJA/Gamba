using Gamba.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamba.Tests.Games
{
    internal class RouletteStatsTests
    {
        private RouletteStats _stats;
        private const int _startingWallet = 1000;

        [SetUp]
        public void Setup()
        {
            _stats = new RouletteStats(_startingWallet);
        }

        [Test(ExpectedResult = _startingWallet)]
        public int Wallet_StartingAmount()
        {
            return _stats.Wallet;
        }

        [Test(ExpectedResult = _startingWallet)]
        public int MinWallet_StartingAmount()
        {
            return _stats.MinWallet;
        }

        [Test(ExpectedResult = _startingWallet)]
        public int MaxWallet_StartingAmount()
        {
            return _stats.MaxWallet;
        }

        [TestCase(20, ExpectedResult = _startingWallet - 20)]
        public int AddBet_Loss_DecreaseWallet(int betAmount)
        {
            _stats.AddBet(false, Draw.One, betAmount, 2);

            return _stats.Wallet;
        }

        [TestCase(20, 3, ExpectedResult = _startingWallet - 20 + (20 * 3))]
        public int AddBet_Win_IncreaseWallet(int betAmount, int winReturn)
        {
            _stats.AddBet(true, Draw.One, betAmount, winReturn);

            return _stats.Wallet;
        }

        [Test]
        public void AddBet_ThreeDraws_IncreaseTotals()
        {
            _stats.AddBet(true, Draw.One, 20, 1);
            _stats.AddBet(true, Draw.One, 20, 1);
            _stats.AddBet(false, Draw.One, 20, 1);

            Assert.That(_stats.TotalWins, Is.EqualTo(2));
            Assert.That(_stats.TotalLosses, Is.EqualTo(1));
            Assert.That(_stats.TotalDraws, Is.EqualTo(3));
        }
    }
}
