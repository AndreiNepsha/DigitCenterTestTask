using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DigitCenterTestTask.Model;

namespace DigitCenterTestTask.Controller
{
    interface IRecordController
    {
        IReadOnlyList<AbstractRecord> Records { get; }
        void AddRecord(AbstractRecord record);
        void DeleteRecord(AbstractRecord record);
    }

    class RecordController : IRecordController
    {
        private IList<AbstractRecord> records = new List<AbstractRecord>();

        public void AddRecord(AbstractRecord record)
        {
            records.Add(record);
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
