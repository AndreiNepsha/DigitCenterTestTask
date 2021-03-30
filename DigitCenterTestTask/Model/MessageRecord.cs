namespace DigitCenterTestTask.Model
{
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
}
