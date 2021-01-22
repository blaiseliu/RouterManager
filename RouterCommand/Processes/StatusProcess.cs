using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RouterCommand.Enums;
using RouterCommand.LoggerHelpers;
using RouterCommand.PageHelpers;
using RouterCommand.ParseOptions;
using SeleniumExtras.WaitHelpers;
using Serilog.Core;
using System;

namespace RouterCommand.Processes
{
    public class StatusProcess : ProcessBase<StatusOption>, IStatusProcess
    {
        #region DI
        private readonly ILogger<StatusProcess> _logger;
        private readonly IConfiguration _config;


        public StatusProcess(ILogger<StatusProcess> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        #endregion

        public override void Process(StatusOption options)
        {
            var username = _config.GetValue<string>("Router:Username");
            var password= _config.GetValue<string>("Router:Password");
            var shortDelay = TimeSpan.FromSeconds(_config.GetValue<int>("WebDriver:ShortDelay"));
            var longDelay = TimeSpan.FromSeconds(_config.GetValue<int>("WebDriver:LongDelay"));
            var startUrl = _config.GetValue<string>("Router:StartUrl");
            _logger.LogInformation($"Change Status of Device {options.Device} into {options.Status}");

            using (var driver = new FirefoxDriver())
            {
                _logger.LogInformation("WebDriver Initialized.");
                var wait = new WebDriverWait(driver, shortDelay);

                driver.Navigate().GoToUrl($"http://{username}:{password}@{startUrl}");
                _logger.LogInformation("Start");

                var tabAdvance = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("AdvanceTab")));
                tabAdvance.Click();
                _logger.LogInformation("Go to Advanced tab");

                var menuSecurity = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("security_header")));
                menuSecurity.Click();
                _logger.LogInformation("Expand the Security menu");

                var lnkAccessControl =
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("access_control")));
                lnkAccessControl.Click();
                _logger.LogInformation("Open Access Control page");

                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page2")));
                var iframe = driver.FindElementById("page2");
                driver.SwitchTo().Frame(iframe);

                _logger.LogInformation("In the Frame.");

                var summary = driver.GetDeviceSummary(options.Device, out var xpathCheckbox);

                Logger.DeviceStatusInformation(summary);

                var allow = options.Status == DeviceStatus.Allowed && summary.Status.Equals("Blocked");
                var block = options.Status == DeviceStatus.Blocked && summary.Status.Equals("Allowed");
                if (!(allow || block))
                {
                    _logger.LogInformation($"The device {options.Device} is {summary.Status}. No need to change.");
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
                _logger.LogInformation($"Device {options.Device} is {status}");
            }
        }

        private void BlockDevice(FirefoxDriver driver, string xpathCheckbox, WebDriverWait wait)
        {
            var chk = driver.FindElementByXPath(xpathCheckbox);
            chk.Click();
            _logger.LogInformation("Check");

            var btnBlock = driver.FindElementById("block");
            btnBlock.Click();
            _logger.LogInformation("Block");

            var alert = wait.Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();
            _logger.LogInformation("Confirm Block");

            wait.Until(ExpectedConditions.StalenessOf(chk));
        }

        private void AllowDevice(FirefoxDriver driver, string xpathCheckbox, WebDriverWait wait)
        {
            var chk = driver.FindElementByXPath(xpathCheckbox);
            chk.Click();
            _logger.LogInformation("Check");

            var btnAllow = driver.FindElementById("allow");
            btnAllow.Click();
            _logger.LogInformation("Allow");

            wait.Until(ExpectedConditions.StalenessOf(chk));
        }
    }
}
