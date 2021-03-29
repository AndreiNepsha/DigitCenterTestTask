using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DigitCenterTestTask.View;
using DigitCenterTestTask.Controller;

namespace DigitCenterTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainController = new MainController();
            var consoleView = new AppConsoleView(mainController);

            mainController.AddRecord(new Model.MessageRecord("Test message"));
            mainController.AddRecord(
                new Model.PersonRecord("Ivanov", "Ivan", DateTime.Parse("1995-04-26"))
            );

            consoleView.StartProcessRenderLoop();
        }
    }
}
