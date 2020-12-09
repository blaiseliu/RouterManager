using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RouterCommand.ParseOptions;
using SeleniumExtras.WaitHelpers;

namespace RouterCommand.Processes
{
public class StatusProcess : ProcessBase<StatusOption>
    {
        public override void Process(StatusOption options)
        {
            Logger.Information("Status");
            
            using (var driver = new FirefoxDriver())
            {
                var wait = new WebDriverWait(driver, Configuration.ShortDelay);
                
                driver.Navigate().GoToUrl($"http://{Configuration.Username}:{Configuration.Password}@{Configuration.StartUrl}");

                var tabAdvance = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("AdvanceTab")));
                tabAdvance.Click();
                
            }
        }
    }
}
