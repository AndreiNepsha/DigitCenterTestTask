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
            var mainController = new RecordController();
            var consoleView = new AppConsoleView(mainController);

            consoleView.StartProcessRenderLoop();
        }
    }
}
