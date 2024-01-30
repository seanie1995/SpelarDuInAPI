using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Menu
{
    internal class MenuRepositoryHandler
    {

        public static void WelcomeSign(CancellationToken token)
        {

            string welcomeSign1 = @"  ______   _______   ______        __       __  __    __   ______   ______   ______  
 /      \ |       \ |      \      |  \     /  \|  \  |  \ /      \ |      \ /      \ 
|  $$$$$$\| $$$$$$$\ \$$$$$$      | $$\   /  $$| $$  | $$|  $$$$$$\ \$$$$$$|  $$$$$$\
| $$___\$$| $$  | $$  | $$        | $$$\ /  $$$| $$  | $$| $$___\$$  | $$  | $$   \$$
 \$$    \ | $$  | $$  | $$        | $$$$\  $$$$| $$  | $$ \$$    \   | $$  | $$      
 _\$$$$$$\| $$  | $$  | $$        | $$\$$ $$ $$| $$  | $$ _\$$$$$$\  | $$  | $$   __ 
|  \__| $$| $$__/ $$ _| $$_       | $$ \$$$| $$| $$__/ $$|  \__| $$ _| $$_ | $$__/  \
 \$$    $$| $$    $$|   $$ \      | $$  \$ | $$ \$$    $$ \$$    $$|   $$ \ \$$    $$
  \$$$$$$  \$$$$$$$  \$$$$$$       \$$      \$$  \$$$$$$   \$$$$$$  \$$$$$$  \$$$$$$ 
                                                                                    ";



            string welcomeSign2 = @"  ______   _______   ______        __       __  __    __   ______   ______   ______  
 /      \ /       \ /      |      /  \     /  |/  |  /  | /      \ /      | /      \ 
/$$$$$$  |$$$$$$$  |$$$$$$/       $$  \   /$$ |$$ |  $$ |/$$$$$$  |$$$$$$/ /$$$$$$  |
$$ \__$$/ $$ |  $$ |  $$ |        $$$  \ /$$$ |$$ |  $$ |$$ \__$$/   $$ |  $$ |  $$/ 
$$      \ $$ |  $$ |  $$ |        $$$$  /$$$$ |$$ |  $$ |$$      \   $$ |  $$ |      
 $$$$$$  |$$ |  $$ |  $$ |        $$ $$ $$/$$ |$$ |  $$ | $$$$$$  |  $$ |  $$ |   __ 
/  \__$$ |$$ |__$$ | _$$ |_       $$ |$$$/ $$ |$$ \__$$ |/  \__$$ | _$$ |_ $$ \__/  |
$$    $$/ $$    $$/ / $$   |      $$ | $/  $$ |$$    $$/ $$    $$/ / $$   |$$    $$/ 
 $$$$$$/  $$$$$$$/  $$$$$$/       $$/      $$/  $$$$$$/   $$$$$$/  $$$$$$/  $$$$$$/ ";


            bool toggle = false;
            int startLine = Console.CursorTop;
           
            while (!token.IsCancellationRequested)
            {

                if (toggle)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, startLine);
                    Console.WriteLine(welcomeSign1);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(0, startLine);
                    Console.WriteLine(welcomeSign2.PadRight(welcomeSign1.Length));
                }
                Console.ResetColor();

                Thread.Sleep(500);

                toggle = !toggle;


                Console.SetCursorPosition(33, 13);
                Console.WriteLine("Press <ENTER> to enter");
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.ResetColor();
            }
        }


    }
}
