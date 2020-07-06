using System;
using System.Linq;

namespace DiceRaceGame
{
    class Program
    {
        static void Main()
        {
            int[] highScore = new int[1];

        BeginGame:
            Console.Clear();
            Random dice = new Random();
            int WINSCORE = 100;
            int NUMBEROFPLAYERS = 0;
            Console.WriteLine("\t\t\t\t\t________________:::::::--Welcome to DICE RACE--::::::::________________\n\n");
            gameRules();
            Console.WriteLine("\nenter number of players");
            NUMBEROFPLAYERS = AskInput();
            if (NUMBEROFPLAYERS == 0) goto End;
            int[] scores = new int[NUMBEROFPLAYERS];
            Console.WriteLine("enter winning score. to set default score(100) press 1");
            WINSCORE = AskInput();
            if (WINSCORE == 1) WINSCORE = 100;
            if (WINSCORE != 100) Console.WriteLine($"winning score set to {WINSCORE} ");
            Console.WriteLine("press ENTER key to continue");
            if (extiGame() == false)
            {

                string[] players = new string[NUMBEROFPLAYERS];

                for (int i = 0; i < NUMBEROFPLAYERS; i++)
                {
                    Console.WriteLine($"enter player {i + 1} name:");
                    players[i] = Console.ReadLine();
                    if (players[i].Length == 0) players[i] = $"player{i + 1}.";
                    players[i] = players[i].ToUpper();
                }
                do
                {
                    for (int i = 0; i < NUMBEROFPLAYERS; i++)
                    {
                        Console.WriteLine($"\nplayer {i + 1} turn, press enter to roll the dice");
                        if (extiGame() == true) goto End;
                        Console.Clear();
                        gameRules();
                        //scoreCard(scores, players);
                        int roll = dice.Next(1, 7);
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine($"\nplayer {i + 1} \"{players[i]}\" rolled {roll} score is {roll + scores[i]}");

                        scores[i] += roll;
                        if (roll == 4)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("congrats, you have rolled 4, bonus score +2 added");
                            scores[i] += 2;                            
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else if (roll == 6)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("great you have rolled 6, bonus score +4 added.");
                            Console.Beep();
                            scores[i] += 4;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        if (scores[i] != 0)
                        {
                            for (int j = 0; j < scores.Length; j++)
                                if (j != i)
                                    if (scores[i] == scores[j])
                                    {
                                        scores[j] = 0;
                                        Console.BackgroundColor = ConsoleColor.Red;
                                        //Console.ForegroundColor = ConsoleColor.Black;
                                        Console.WriteLine($"{players[i]} killed {players[j]}");
                                        Console.BackgroundColor = ConsoleColor.Black;
                                    }
                        }
                        Console.WriteLine();
                        scoreCard(scores, players);

                        if (scores[i] >= WINSCORE)
                        {
                            Console.WriteLine($"player {i + 1} {players[i]} wins");
                            Console.Beep();
                            for(int design = 0; design <= 3; design++)
                            {                                
                                Console.WriteLine("| | | | | | | | | |");
                                Console.WriteLine("* * * * * * * * * *");
                                System.Threading.Thread.Sleep(100);
                                
                            }
                            break;
                        }
                    }
                } while (scores.Max() < WINSCORE);

            }
            if (highScore[0] < scores.Max()) highScore[0] = scores.Max();
            for (int i = 0; i < scores.Length; i++)
                scores[i] = 0;
            Console.WriteLine($"high score is {highScore[0]}");
            Console.WriteLine("Do you want play again?? press 1 to play again or press 0 to exit");
            if (AskInput() == 0) goto End;
            else goto BeginGame;
            End:
            Console.Clear();
            Console.WriteLine("Thank You");
        }
        public static void gameRules()
        {
            Console.WriteLine("Please read instructions: ");
            Console.WriteLine("1. The game always begins with player 1");
            Console.WriteLine("2. if present player score equals any players score the player score will become zero ");
            Console.WriteLine("3. the player whose score will be 100 or more will be winner.");
            Console.WriteLine("4. if Dice rolled 4 score bonus will be added with 4+2 if dice rolled 6 then score will be added 6+4");
            Console.WriteLine("5. to exit game at any time press 0");
            Console.WriteLine("6. if you do not prefer to enter your name(player name) just press enter default name will be given");
        }
        public static int AskInput()
        {
            int value = 0;
        begin:
            try
            {
                string input = Console.ReadLine();
                value = int.Parse(input);
            }
            catch
            {
                Console.WriteLine("please enter a valid entry ");
                goto begin;
            }
            return value;
        }
        public static bool extiGame()
        {
            try
            {
                if (int.Parse(Console.ReadLine()) == 0)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        public static void scoreCard(int[] scores, string[] players)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                Console.WriteLine($"{players[i]} : {scores[i]}");
            }
        }
        public static bool playAgain()
        {
            string key = Console.ReadLine();
            try
            {
                if (int.Parse(key) == 1) return false;
            }
            catch
            {
                return true;
            }
            return true;
        }
    }
}
