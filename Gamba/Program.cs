namespace Gamba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new Dictionary<int, long>();
            dictionary.Add(1, 0);
            dictionary.Add(3, 0);
            dictionary.Add(5, 0);
            dictionary.Add(10, 0);
            dictionary.Add(20, 0);

            var wallet = 10000;
            var roulette = new Games.RouletteWheel();
            var max = int.MinValue;
            var min = int.MaxValue;
            var bet = 10;
            var lastWin = true;
            var count = 1;

            var setRanges = () =>
            {
                max = Math.Max(max, wallet);
                min = Math.Min(min, wallet);
            };

            setRanges();

            while (wallet >= 10)
            {
                var result = roulette.GetNextDraw();
                dictionary[result]++;
                var draw = 1;
                var win = 0;

                if (lastWin)
                {
                    bet = 10;
                }
                else if (bet * 2 <= wallet)
                //else if (bet + 10 <= wallet)
                {
                    bet *= 2;
                    //bet += 10;
                }

                while (bet > wallet && bet >= 10)
                {
                    bet /= 2;
                    //bet -= 10;
                }

                if (bet > wallet)
                    break;

                if (result == draw)
                {
                    win = roulette.GetWinReturn(draw) * bet;
                    lastWin = true;
                }
                else
                {
                    lastWin = false;
                }

                wallet -= bet;
                wallet += win;

                setRanges();
                Console.WriteLine($"{count++}. Rolled: {result,2} - Winnings: {win,5} - Bet: {-bet,6} - Wallet: {wallet} - Min/Max: {min}|{max}");
                //System.Threading.Thread.Sleep(10);
            }

            foreach (var val in dictionary.Keys)
            {
                Console.WriteLine($"{val}: {dictionary[val] / ((double)count-1)}");
            }
        }
    }
}