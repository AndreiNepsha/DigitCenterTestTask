using System;

namespace DigitCenterTestTask.Model
{
    class PersonRecord : AbstractRecord
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; }
        public int Age { get; }

        public PersonRecord(string lastName, string firstName, DateTime birthDate)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;

            var now = DateTime.Now;
            var age = now.Year - BirthDate.Year;
            Age = BirthDate.Date > now.AddYears(-age) ? age-1 : age;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}] {2} {3}, {4}",
                base.ToString(), "Person", LastName, FirstName, Age
                );
        }
    }
}
