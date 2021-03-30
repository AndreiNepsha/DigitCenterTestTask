using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitCenterTestTask.View
{
    class ConsoleMenu
    {
        private IList<ConsoleMenuOption> Options;
        private ConsoleMenuOption SelectedOption;
        private readonly string Header;

        public ConsoleMenu(string header, IList<ConsoleMenuOption> options)
        {
            Header = header;
            Options = options;
            SelectedOption = Options.First();
        }

        public void RenderAndProcessAction()
        {
            ConsoleKeyInfo keyinfo;
            bool actionPerformed = false;
            do
            {
                RenderMenu();

                keyinfo = Console.ReadKey();

                switch (keyinfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (Options.Last() != SelectedOption)
                        {
                            SelectedOption = Options[Options.IndexOf(SelectedOption) + 1];
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (Options.First() != SelectedOption)
                        {
                            SelectedOption = Options[Options.IndexOf(SelectedOption) - 1];
                        }
                        break;

                    case ConsoleKey.Enter:
                        SetDefaultColors();
                        SelectedOption.OnSelected();
                        actionPerformed = true;
                        break;
                }
            }
            while (!actionPerformed);
        }

        private void RenderMenu()
        {
            SetDefaultColors();
            Console.Clear();

            Console.WriteLine(Header);

            foreach (ConsoleMenuOption option in Options)
            {
                SetupColors(option);

                Console.WriteLine(option.Name);
            }
        }

        private void SetupColors(ConsoleMenuOption option)
        {
            if (SelectedOption == option) SetInverseColors();
            else SetDefaultColors();
        }

        private void SetDefaultColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void SetInverseColors()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
