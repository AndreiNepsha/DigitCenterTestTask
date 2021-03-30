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
        private readonly IRecordController Controller;
        private readonly ConsoleMenu MainMenu;

        public AppConsoleView(IRecordController controller)
        {
            Controller = controller;

            MainMenu = new ConsoleMenu("Main menu:", new List<ConsoleMenuOption>()
            {
                new ConsoleMenuOption("Print records", PrintRecords),
                new ConsoleMenuOption("Add record", AddRecord),
                new ConsoleMenuOption("Delete record", DeleteRecord),
                new ConsoleMenuOption("Exit", () => { Environment.Exit(0); })
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
            var addMenuOptions = new List<ConsoleMenuOption>()
            {
                new ConsoleMenuOption("Message", AddMessageRecord),
                new ConsoleMenuOption("Person", AddPersonRecord),
                new ConsoleMenuOption("Car", AddCarRecord),
            };

            var addMenu = new ConsoleMenu("Choose record type to add:", addMenuOptions);

            addMenu.RenderAndProcessAction();
        }

        private void DeleteRecord()
        {
            var deleteMenuOptions = Controller.Records.Select(record =>
                    new ConsoleMenuOption(record.ToString(), () => Controller.DeleteRecord(record))
                ).ToList();

            var deleteMenuHeader = deleteMenuOptions.Any() ? "Choose record to delete:" : "There are no records to delete.";

            deleteMenuOptions.Add(new ConsoleMenuOption("Return to the main menu", () => { }));

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

        private void AddCarRecord()
        {
            Console.Clear();
            Console.WriteLine("Enter car model:");
            var model = Console.ReadLine();

            int? yearOfManufacture = null;
            while (yearOfManufacture == null)
            {
                Console.WriteLine("Enter car year of manufacture:");
                try
                {
                    var yearOfManufactureString = Console.ReadLine();
                    yearOfManufacture = int.Parse(yearOfManufactureString);
                }
                catch (Exception e)
                {
                    if (e is FormatException || e is OverflowException)
                        Console.WriteLine("Sorry, can't parse this year, please try again...");
                    else throw e;
                }
            }

            Controller.AddRecord(new CarRecord(
                model, yearOfManufacture ?? throw new NullReferenceException("Unexpected year of manufacture null state"))
            );
        }
    }
}
