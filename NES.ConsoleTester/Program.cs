using NESControllerLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NES.ConsoleTester
{
    public class Program
    {
        private static NESController controller = new NESController();
        private static Random random = new Random();
        private static Point mapSize = new Point(15, 10), playerLocation = GetRandomPointOnBoard(), food = GetRandomPointOnBoard();
        private static object lockObject = new();
        private static Timer timer;
        private static char playerChar = 'O';

        static void Main(string[] args)
        {

            TimerCallback callBack = TimerCallBackMethod;
            timer = new Timer(callBack, null, 0, 250);

            Console.SetWindowSize(mapSize.X, mapSize.Y);
            Console.SetBufferSize(mapSize.X, mapSize.Y);
            Console.SetWindowSize(mapSize.X, mapSize.Y);
            Console.CursorVisible = false;
            controller.ButtonStateChanged += Controller_ButtonStateChanged;

            while (playerLocation == food)
            {
                playerLocation = GetRandomPointOnBoard();
            }
            DrawScreen();
            Console.ReadLine();
        }

        private static void DrawScreen()
        {
            lock (lockObject)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                for (int y = 0; y < mapSize.Y; y++)
                {
                    for (int x = 0; x < mapSize.X; x++)
                    {
                        if (y == 0 || x == 0 || y == mapSize.Y - 1 || x == mapSize.X - 1)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write("*");
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int y = 1; y < mapSize.Y - 1; y++)
                {
                    for (int x = 1; x < mapSize.X - 1; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(".");
                    }
                }

                Console.SetCursorPosition(playerLocation.X, playerLocation.Y);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(playerChar);

                Console.SetCursorPosition(food.X, food.Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("%");
                Console.ResetColor();
            }
        }

        private static void Controller_ButtonStateChanged(object? sender, NESControllerEventArgs e)
        {
            if (e.Action != NESControllerButtonAction.Pressed) { return; }
            switch (e.Button)
            {
                case NESControllerButton.Select:
                    break;
                case NESControllerButton.Start:
                    break;
                case NESControllerButton.B:
                    break;
                case NESControllerButton.A:
                    break;
                case NESControllerButton.Right:
                    playerLocation.X++;
                    break;
                case NESControllerButton.Up:
                    playerLocation.Y--;
                    break;
                case NESControllerButton.Down:
                    playerLocation.Y++;
                    break;
                case NESControllerButton.Left:
                    playerLocation.X--;
                    break;
                default:
                    break;
            }

            if (playerLocation.X < 1) { playerLocation.X = 1; }
            if (playerLocation.X >= mapSize.X - 1) { playerLocation.X = mapSize.X - 2; }
            if (playerLocation.Y < 1) { playerLocation.Y = 1; }
            if (playerLocation.Y >= mapSize.Y - 1) { playerLocation.Y = mapSize.Y - 2; }

            if (playerLocation == food)
            {
                food = GetRandomPointOnBoard();
            }
            //DrawScreen();
        }

        private static Point GetRandomPointOnBoard()
        {
            return new Point(random.Next(1, mapSize.X - 2), random.Next(1, mapSize.Y - 2));
        }

        private static void TimerCallBackMethod(Object state)
        {
            playerChar = playerChar == 'O' ? 'o' : 'O';
            DrawScreen();
        }
    }
}
