using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace HHUp
{
    class HHNavigator
    {
        private readonly IWebDriver driver;
        private readonly TimeSpan WaitEl = new TimeSpan(0, 0, 10);

        public HHNavigator(bool headless)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            if (headless)
            {
                options.AddArgument("headless");
                options.AddArgument("window-size=1920x1080");
            }

            driver = new ChromeDriver(driverService, options);
            driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 10);
        }

        private void SaveScreenAndSource(string path)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(path + "SeleniumTestingScreenshot.jpg");
            File.WriteAllText(path + "SeleniumPageSource.html", driver.PageSource);
        }

        public void LogIn(string userName, string password)
        {
            try
            {
                driver.Url = "https://hh.ru/";
                new WebDriverWait(driver, WaitEl)
                    .Until(x => x.FindElement(By.LinkText("Войти")))
                    .Click();                   

                new WebDriverWait(driver, WaitEl)
                    .Until(x => x.FindElement(By.CssSelector("input[name='username']")))
                    .SendKeys(userName);
                driver.FindElement(By.CssSelector("input[name='password']"))
                    .SendKeys(password + Keys.Enter);
            }
            catch (Exception)
            {
                SaveScreenAndSource("");
                throw;
            }           
        }

        public void NavigateMyResumes()
        {
            try
            {
                new WebDriverWait(driver, WaitEl)
                    .Until(x => x.FindElement(By.LinkText("Мои резюме")))
                    .Click();
            }
            catch (Exception)
            {
                SaveScreenAndSource("");
                throw;
            }
        }

        public void UpResume(int resumeNum)
        {
            try
            {
                new WebDriverWait(driver, WaitEl)
                    .Until(x => x.FindElements(By.LinkText("Поднять в поиске"))[resumeNum])
                    .Click();
            }
            catch (Exception)
            {
                SaveScreenAndSource("");
                throw;
            }
        }

        public void LogOut()
        {
            try
            {
                new WebDriverWait(driver, WaitEl)
                    .Until(x => x.FindElement(By.CssSelector("button[data-qa='mainmenu_applicantProfile']")))
                    .Click();
                new WebDriverWait(driver, WaitEl)
                    .Until(x => x.FindElement(By.LinkText("Выход")))
                    .Click();
            }
            catch (Exception)
            {
                SaveScreenAndSource("");
                throw;
            }
        }
    }
}
