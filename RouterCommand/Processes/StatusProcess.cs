using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RouterCommand.Enums;
using RouterCommand.Enums.Attributes;
using RouterCommand.ParseOptions;
using RouterCommand.ViewModels;
using SeleniumExtras.WaitHelpers;

namespace RouterCommand.Processes
{
    public class StatusProcess : ProcessBase<StatusOption>
    {
        public override void Process(StatusOption options)
        {
            Logger.Information($"Change Status of Device {options.Device} into {options.Status}");

            using (var driver = new FirefoxDriver())
            {
                Logger.Information("WebDriver Initialized.");
                var wait = new WebDriverWait(driver, Configuration.ShortDelay);

                driver.Navigate().GoToUrl($"http://{Configuration.Username}:{Configuration.Password}@{Configuration.StartUrl}");
                Logger.Information("Start");

                var tabAdvance = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("AdvanceTab")));
                tabAdvance.Click();
                Logger.Information("Go to Advanced tab");

                var menuSecurity = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("security_header")));
                menuSecurity.Click();
                Logger.Information("Expand the Security menu");

                var lnkAccessControl =
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("access_control")));
                lnkAccessControl.Click();
                Logger.Information("Open Access Control page");


                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page2")));
                var iframe = driver.FindElementById("page2");
                driver.SwitchTo().Frame(iframe);

                //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[text()=' Access Control']")));
                //var framecontent = driver.PageSource;
                Logger.Information("In the Frame.");

                var summary = GetDeviceSummary(options.Device, driver, out var xpathCheckbox);

                Logger.Information($"Device: {summary.Device}");
                Logger.Information($"Status: {summary.Status}");
                Logger.Information($"Device Name: {summary.DeviceName}");
                Logger.Information($"IP Address: {summary.IPAddress}");
                Logger.Information($"MAC Address: {summary.MacAddress}");
                Logger.Information($"Connection Type: {summary.ConnectionType}");

                if (options.Status == DeviceStatus.Allowed && summary.Status.Equals("Blocked"))
                {
                    var chk = driver.FindElementByXPath(xpathCheckbox);
                    chk.Click();
                    Logger.Information("Check");

                    var btnAllow = driver.FindElementById("allow");
                    btnAllow.Click();
                    Logger.Information("Allow");

                    wait.Until(ExpectedConditions.StalenessOf(chk));
                }

                if (options.Status == DeviceStatus.Blocked && summary.Status.Equals("Allowed"))
                {
                    var chk = driver.FindElementByXPath(xpathCheckbox);
                    chk.Click();
                    Logger.Information("Check");

                    var btnBlock = driver.FindElementById("block");
                    btnBlock.Click();
                    Logger.Information("Block");

                    wait.Until(ExpectedConditions.StalenessOf(chk));
                }

                var status = GetDeviceStatus(options.Device, driver);
                Logger.Information($"Device {options.Device} is {status}");
            }
        }
        private static string GetDeviceStatus(Device device, FirefoxDriver driver)
        {
            var macAddress = device.MacAddress();
            var xpathDevice = $"//tr[td/span/text()='{macAddress}']";
            
            var statusElement = driver.FindElementByXPath($"{xpathDevice}/td[2]");
            
            return statusElement.Text;
        }
        private static DeviceStatusSummary GetDeviceSummary(Device device, FirefoxDriver driver, out string xpathCheckbox)
        {
            var test = driver.PageSource;

            var macAddress = device.MacAddress();
            var xpathDevice = $"//tr[td/span/text()='{macAddress}']";
            var deviceRow = driver.FindElementByXPath(xpathDevice);
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathDevice)));
            
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
