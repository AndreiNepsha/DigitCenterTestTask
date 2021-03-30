using System;

namespace DigitCenterTestTask.View
{
    class ConsoleMenuOption
    {
        public string Name { get; }
        public Action OnSelected { get; }

        public ConsoleMenuOption(string name, Action onSelected)
        {
            Name = name;
            OnSelected = onSelected;
        }
    }
}
