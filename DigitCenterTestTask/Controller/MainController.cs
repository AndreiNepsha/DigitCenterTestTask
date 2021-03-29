using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DigitCenterTestTask.Model;

namespace DigitCenterTestTask.Controller
{
    class MainController
    {
        private IList<AbstractRecord> records = new List<AbstractRecord>();

        public void AddRecord(AbstractRecord record)
        {
            records.Add(record);
        }

        public IReadOnlyList<MessageRecord> MessageRecords {
            get { return new List<MessageRecord>(records.OfType<MessageRecord>()); }
        }

        public IReadOnlyList<PersonRecord> PersonRecords {
            get { return new List<PersonRecord>(records.OfType<PersonRecord>()); }
        }

        public IReadOnlyList<AbstractRecord> Records {
            get { return new List<AbstractRecord>(records); }
        }

        public void DeleteRecord(AbstractRecord record)
        {
            records.Remove(record);
        }
    }
}
