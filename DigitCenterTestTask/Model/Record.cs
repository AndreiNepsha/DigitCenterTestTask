using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitCenterTestTask.Model
{
    abstract class AbstractRecord
    {
        public DateTime CreatedDate { get; }

        public AbstractRecord()
        {
            CreatedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return CreatedDate.ToString("[dd-MMMM-yy HH:mm:ss]");
        }
    }

    class MessageRecord : AbstractRecord
    {
        public string Text { get; set; }

        public MessageRecord(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return base.ToString() + " [Message] " + Text;
        }
    }

    class PersonRecord : AbstractRecord
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; }
        public int Age {
            get {
                var now = DateTime.Now;
                var age = now.Year - BirthDate.Year;
                if (BirthDate.Date > now.AddYears(-age)) return age - 1;
                else return age;
            }
        }

        public PersonRecord(string lastName, string firstName, DateTime birthDate)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
        }

        public override string ToString()
        {
            return String.Format("{0} [{1}] {2} {3}, {4}",
                base.ToString(), "Person", LastName, FirstName, Age
                );
        }
    }
}
