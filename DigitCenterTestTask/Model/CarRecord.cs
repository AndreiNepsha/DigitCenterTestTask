namespace DigitCenterTestTask.Model
{
    class CarRecord : AbstractRecord
    {
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }

        public CarRecord(string model, int yearOfManufacture)
        {
            Model = model;
            YearOfManufacture = yearOfManufacture;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}] {2}, {3}",
                base.ToString(), "Car", Model, YearOfManufacture
                );
        }
    }
}
