namespace lab_work_5
{
    public abstract class ElectronicDevice
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ScreenSize { get; set; }
        public string Resolution { get; set; }
        public bool IsSmartTV { get; set; }
        public int SoundPower { get; set; }

        public abstract void DisplayInfo();
    }
}
