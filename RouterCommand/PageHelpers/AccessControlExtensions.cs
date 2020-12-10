using OpenQA.Selenium.Firefox;
using RouterCommand.Enums;
using RouterCommand.Enums.Attributes;
using RouterCommand.ViewModels;

namespace RouterCommand.PageHelpers
{
    public static class AccessControlExtensions
    {
        
        public static string GetDeviceStatus(this FirefoxDriver driver,Device device)
        {
            var macAddress = device.MacAddress();
            var xpathDevice = $"//tr[td/span/text()='{macAddress}']";
            
            var statusElement = driver.FindElementByXPath($"{xpathDevice}/td[2]");
            
            return statusElement.Text;
        }

        public static DeviceStatusSummary GetDeviceSummary(this FirefoxDriver driver, Device device, out string xpathCheckbox)
        {

            var macAddress = device.MacAddress();
            var xpathDevice = $"//tr[td/span/text()='{macAddress}']";
            
            xpathCheckbox = $"{xpathDevice}/td[1]/input";

            var statusElement = driver.FindElementByXPath($"{xpathDevice}/td[2]");
            var deviceNameElement = driver.FindElementByXPath($"{xpathDevice}/td[3]");
            var ipAddressElement = driver.FindElementByXPath($"{xpathDevice}/td[4]");
            var macAddressElement = driver.FindElementByXPath($"{xpathDevice}/td[5]");
            var connectionTypeElement = driver.FindElementByXPath($"{xpathDevice}/td[6]");
            var summary = new DeviceStatusSummary
            {
                Device = device,
                Status = statusElement.Text,
                DeviceName = deviceNameElement.Text,
                IPAddress = ipAddressElement.Text,
                MacAddress = macAddressElement.Text,
                ConnectionType = connectionTypeElement.Text
            };

            return summary;
        }
    }
}