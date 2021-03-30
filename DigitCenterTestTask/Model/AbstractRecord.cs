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
}
