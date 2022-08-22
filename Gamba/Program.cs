using Gamba.Games;

namespace Gamba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var startingCurrency = 10000;
            var minBet = 10;
            var roulette = new Games.Roulette(startingCurrency);
            var bet = minBet;
            var betMultiplier = 2;
            
            while (roulette.Wallet >= 10)
            {
                if (roulette.TotalDraws == 0 || roulette.LastResult.Won)
                {
                    bet = minBet;
                }
                else if (bet * betMultiplier <= roulette.Wallet)
                {
                    bet *= betMultiplier;
                }

                while (bet > roulette.Wallet && bet >= minBet)
                {
                    bet /= betMultiplier;
                }

                if (bet > roulette.Wallet)
                {
                    break;
                }

                var result = roulette.Bet(Games.Draw.One, bet);

                Console.WriteLine($"{roulette.TotalDraws}. Rolled: {(int)result.Roll,2} - Bet {(int)result.Bet,2} - Winnings: {result.AmountWon,5} - Wager: {result.Wager,6} - Wallet: {result.CurrentWallet,7} - Wallet Min|Max: {roulette.MinWallet}|{roulette.MaxWallet}");

                //System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine();
            Console.WriteLine(" ------------- RESULTS -------------");
            Console.WriteLine($"Wins: {roulette.TotalWins} Losses: {roulette.TotalLosses} Total: {roulette.TotalDraws}");
            Console.WriteLine($"Wallet Maximum: {roulette.MaxWallet:C} ");

            foreach (var draw in (Draw[])Enum.GetValues(typeof(Draw)))
            {
                Console.WriteLine($"{(int)draw,2} - {((roulette.GetTotalDraws(draw) / (double)roulette.TotalDraws) * 100),6:F2}");
            }
        }
    }
}