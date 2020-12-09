using RouterCommand.Enums;

namespace RouterCommand.ViewModels
{
    public class DeviceStatusSummary
    {
        public Device Device { get; set; }
        public string Status { get; set; }
        public string DeviceName { get; set; }
        public string IPAddress { get; set; }
        public string MacAddress { get; set; }
        public string ConnectionType { get; set; }
    }
}