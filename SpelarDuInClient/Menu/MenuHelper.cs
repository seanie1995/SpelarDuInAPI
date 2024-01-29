using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Menu
{
    internal class MenuHelper
    {
        public static void LoadingScreen(CancellationToken cancellationToken)
        {
            Console.CursorVisible = false;  //hide cursor
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Loading: ");

            char[] spinnerAnimationFrames = { '|', '/', '-', '\\' };
            int currentAnimationFrame = 0;

            while (!cancellationToken.IsCancellationRequested)  //Continue the animation untill a cancellation is requested, thsi comes from cancellationtoken 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(100);

                var originalX = Console.CursorLeft; //record the current cursor possition 
                var originalY = Console.CursorTop;

                Console.Write(spinnerAnimationFrames[currentAnimationFrame]);
                currentAnimationFrame++;    //Move to next frame

                if (currentAnimationFrame == spinnerAnimationFrames.Length)  //resets frames to create a loop
                {
                    currentAnimationFrame = 0;
                }
                Console.SetCursorPosition(originalX, originalY);    //restores current cursor possition to overwrite the previous frame

                Console.ResetColor();
                //break;
            }
        }
        public static int RunMeny(string[] options, bool alignment, bool vertical, int position1, int position2)
        {
            Console.CursorVisible = false;
            int selectedIndex = 0;
            //string[] options = { "Account information", "Main meny" };
            ConsoleKey keyPressed;
            do
            {
                if (alignment == true)
                {
                    Console.SetCursorPosition(1, Console.WindowHeight - 1);
                }
                else if (alignment == false)
                {
                    Console.SetCursorPosition(position1, position2);
                }

                for (int i = 0; i < options.Length; i++)
                {
                    string currentOption = options[i];
                    string prefix;

                    if (i == selectedIndex)
                    {
                        prefix = " ";

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        prefix = " ";

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    if (vertical == true)
                    {
                        Console.Write($"{prefix}{currentOption}");
                    }
                    else if (vertical == false)
                    {
                        Console.WriteLine($"{prefix}{currentOption}");
                    }
                }

                Console.ResetColor();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Registers key info 
                keyPressed = keyInfo.Key;   //Update selectedIndex based on arrow keys
                if (vertical == true)
                {

                    if (keyPressed == ConsoleKey.LeftArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                        {
                            selectedIndex = 0;  //Set to max so it always resets when left key reaches array position -1 it resets to 0.
                        }
                    }
                    else if (keyPressed == ConsoleKey.RightArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Length)
                        {
                            selectedIndex = options.Length - 1; //Set to max so it always resets when left key reaches array of its lenght and resets to -1.
                        }
                    }
                }
                else if (vertical == false)
                {
                    if (keyPressed == ConsoleKey.UpArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                        {
                            selectedIndex = 0;  //Set to max so it always resets when left key reaches array position -1 it resets to 0.
                        }
                    }
                    else if (keyPressed == ConsoleKey.DownArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Length)
                        {
                            selectedIndex = options.Length - 1; //Set to max so it always resets when left key reaches array of its lenght and resets to -1.
                        }
                    }
                }

            }
            while (keyPressed != ConsoleKey.Enter); //While loop aslong keypress is not enter. 
            {

            }
            return selectedIndex;
        }
    }
}
