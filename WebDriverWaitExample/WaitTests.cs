using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace WebDriverWaitExample
{
    public class WaitTests
    {
        private WebDriver driver;
        private WebDriverWait wait;

        [TearDown]

        public void ShutDown()
        {
            driver.Quit();
        }
            

        [Test]
        public void Test_Wait_ThreadSleep()
        {
            this.driver = new ChromeDriver();

            driver.Url = "http://www.uitestpractice.com/Students/Contact/";
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);

            var element = driver.FindElement(By.PartialLinkText("This is"));

            element.Click();

            Thread.Sleep(15000);

            var text_element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);
        }

        [Test]
        public void Test_Wait_ImplicitWait()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            driver.Url = "http://www.uitestpractice.com/Students/Contact/";
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);

            driver.FindElement(By.PartialLinkText("This is")).Click();

            var text_element = driver.FindElement(By.ClassName("ContactUs")).Text;
            Assert.IsNotEmpty(text_element);
        }

        [Test]
        public void Test_Wait_ExplicitWait()
        {
            this.driver = new ChromeDriver();
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            driver.Url = "http://www.uitestpractice.com/Students/Contact/";
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);

            driver.FindElement(By.PartialLinkText("This is")).Click();

            var text_element = this.wait.Until(d =>
            {
                return d.FindElement(By.PartialLinkText("This is")).Text;
            });
            Assert.IsNotEmpty(text_element);
        }

        [Test]
        public void Test_Wait_ExpectedCondition()
        {
            this.driver = new ChromeDriver();
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            driver.Url = "http://www.uitestpractice.com/Students/Contact/";
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);

            driver.FindElement(By.PartialLinkText("This is")).Click();

            var text_element = this.wait.Until(
                ExpectedConditions.ElementIsVisible(By.PartialLinkText("This is"))
                ).Text;

            Assert.IsNotEmpty(text_element);
        }
    }
}