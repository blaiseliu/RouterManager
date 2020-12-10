using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RouterCommand.Enums;
using RouterCommand.LoggerHelpers;
using RouterCommand.PageHelpers;
using RouterCommand.ParseOptions;
using SeleniumExtras.WaitHelpers;
using Serilog.Core;

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

                Logger.Information("In the Frame.");

                var summary = driver.GetDeviceSummary(options.Device, out var xpathCheckbox);

                Logger.DeviceStatusInformation(summary);

                var allow = options.Status == DeviceStatus.Allowed && summary.Status.Equals("Blocked");
                var block = options.Status == DeviceStatus.Blocked && summary.Status.Equals("Allowed");
                if (!(allow ||block))
                {
                    Logger.Information($"The device {options.Device} is {summary.Status}. No need to change.");
                    return;
                }
                if (allow)
                {
                    AllowDevice(driver, xpathCheckbox, wait);
                }

                if (block)
                {
                    BlockDevice(driver, xpathCheckbox, wait);
                }

                var status = driver.GetDeviceStatus(options.Device);
                Logger.Information($"Device {options.Device} is {status}");
            }
        }

        

        private void BlockDevice(FirefoxDriver driver, string xpathCheckbox, WebDriverWait wait)
        {
            var chk = driver.FindElementByXPath(xpathCheckbox);
            chk.Click();
            Logger.Information("Check");

            var btnBlock = driver.FindElementById("block");
            btnBlock.Click();
            Logger.Information("Block");

            var alert = wait.Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();
            Logger.Information("Confirm Block");

            wait.Until(ExpectedConditions.StalenessOf(chk));
        }

        private void AllowDevice(FirefoxDriver driver, string xpathCheckbox, WebDriverWait wait)
        {
            var chk = driver.FindElementByXPath(xpathCheckbox);
            chk.Click();
            Logger.Information("Check");

            var btnAllow = driver.FindElementById("allow");
            btnAllow.Click();
            Logger.Information("Allow");

            wait.Until(ExpectedConditions.StalenessOf(chk));
        }
    }
}
