using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DigitCenterTestTask.Controller;
using DigitCenterTestTask.Model;

namespace DigitCenterTestTask.View
{
    class AppConsoleView
    {
        private readonly MainController Controller;
        private readonly ConsoleMenu MainMenu;

        public AppConsoleView(MainController controller)
        {
            Controller = controller;

            MainMenu = new ConsoleMenu("Main menu:", new List<Option>()
            {
                new Option("Print records", PrintRecords),
                new Option("Add record", AddRecord),
                new Option("Delete record", DeleteRecord),
                new Option("Exit", () => { Environment.Exit(0); })
            });
        }

        public void StartProcessRenderLoop()
        {
            while (true)
            {
                MainMenu.RenderAndProcessAction();
            }
        }

        private void PrintRecords()
        {
            Console.Clear();
            var records = Controller.Records;

            if (!records.Any())
            {
                Console.WriteLine("There are no records yet.");
            }
            else
            {
                foreach (AbstractRecord record in records)
                {
                    Console.WriteLine(record.ToString());
                }
            }

            Console.WriteLine("Press any key to return to the main menu...");

            Console.ReadKey();
        }

        private void AddRecord()
        {
            var addMenuOptions = new List<Option>()
            {
                new Option("Message", AddMessageRecord),
                new Option("Person", AddPersonRecord),
            };

            var addMenu = new ConsoleMenu("Choose record type to add:", addMenuOptions);

            addMenu.RenderAndProcessAction();
        }

        private void DeleteRecord()
        {
            var deleteMenuOptions = Controller.Records.Select(record =>
                    new Option(record.ToString(), () => Controller.DeleteRecord(record))
                ).ToList();

            var deleteMenuHeader = deleteMenuOptions.Any() ? "Choose record to delete:" : "There are no records to delete.";

            deleteMenuOptions.Add(new Option("Return to the main menu", () => { }));

            var deleteMenu = new ConsoleMenu(deleteMenuHeader, deleteMenuOptions);

            deleteMenu.RenderAndProcessAction();
        }

        private void AddMessageRecord()
        {
            Console.Clear();
            Console.WriteLine("Enter message text:");
            var text = Console.ReadLine();
            Controller.AddRecord(new MessageRecord(text));
        }

        private void AddPersonRecord()
        {
            Console.Clear();
            Console.WriteLine("Enter person last name:");
            var lastName = Console.ReadLine();

            Console.WriteLine("Enter person first name:");
            var firstName = Console.ReadLine();

            DateTime? birthDate = null;
            while (birthDate == null)
            {
                Console.WriteLine("Enter person birth date:");
                try
                {
                    var birthDateString = Console.ReadLine();
                    birthDate = DateTime.Parse(birthDateString);
                } catch (FormatException)
                {
                    Console.WriteLine("Sorry, can't parse this date, please try again...");
                }
            }

            Controller.AddRecord(new PersonRecord(
                lastName, firstName, 
                birthDate ?? throw new NullReferenceException("Unexpected birth date null state"))
            );
        }
    }

    class ConsoleMenu
    {
        private IList<Option> Options;
        private Option SelectedOption;
        private readonly string Header;

        public ConsoleMenu(string header, IList<Option> options)
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

            foreach (Option option in Options)
            {
                SetupColors(option);

                Console.WriteLine(option.Name);
            }
        }

        private void SetupColors(Option option)
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

    class Option
    {
        public string Name { get; }
        public Action OnSelected { get; }

        public Option(string name, Action onSelected)
        {
            Name = name;
            OnSelected = onSelected;
        }
    }
}
