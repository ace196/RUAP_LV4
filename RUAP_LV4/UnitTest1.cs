using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace RUAP_LV4
{
    public class UnitTest1
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demowebshop.tricentis.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheExportloginTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Log in")));
            driver.FindElement(By.LinkText("Log in")).Click();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            driver.FindElement(By.Id("Email")).Clear();

            driver.FindElement(By.Id("Email")).SendKeys("m_dakovac@net.hr");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("asdfqwer");
            driver.FindElement(By.CssSelector("input.button-1.login-button")).Click();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//a[contains(text(),'Electronics')])[3]")));
            driver.FindElement(By.XPath("(//a[contains(text(),'Electronics')])[3]")).Click();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//a[contains(text(),'Cell phones')])[3]")));
            driver.FindElement(By.XPath("(//a[contains(text(),'Cell phones')])[3]")).Click();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input.button-2.product-box-add-to-cart-button")));
            driver.FindElement(By.CssSelector("input.button-2.product-box-add-to-cart-button")).Click();

            Thread.Sleep(4000);

            driver.FindElement(By.CssSelector("span.cart-label")).Click();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("termsofservice")));
            driver.FindElement(By.Id("termsofservice")).Click();

            driver.FindElement(By.Id("checkout")).Click();

        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
