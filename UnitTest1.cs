using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium;


namespace TestProject1_Appium
{
    [TestClass]
    public class UnitTest1
    { 
        
        private static AppiumDriver<AndroidElement> driver;
        private static AppiumLocalService appiumLocalService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            appiumLocalService.Start();

            var appPath = @"C:\Users\harvey.decapia\Downloads\MAQS_Demo.apk";
            var driverOptions = new AppiumOptions();



            driverOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "10.0");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "emulator-5554");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.App, appPath);

            //driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions);

            driver = new AndroidDriver<AndroidElement>(appiumLocalService, driverOptions);
            driver.CloseApp();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            appiumLocalService.Dispose();
        }


        [TestInitialize]
        public void Setup()
        {
            driver?.LaunchApp();
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver?.CloseApp();
        }

        [TestMethod]
        public void ValidLogin()
        {
            AndroidElement userName = driver.FindElementById("com.magenic.appiumtesting.maqsregistrydemo:id/userNameField");
            AndroidElement password = driver.FindElementById("com.magenic.appiumtesting.maqsregistrydemo:id/passwordField");
            AndroidElement loginBtn = driver.FindElementById("com.magenic.appiumtesting.maqsregistrydemo:id/loginButton");

            userName.SendKeys("Ted");
            password.SendKeys("123");
            loginBtn.Click();

            AndroidElement welcomeLbl = driver.FindElementById("com.magenic.appiumtesting.maqsregistrydemo:id/welcomeLabel");

            ////Explicit Wait
            //WebDriverWait wait = new (driver, TimeSpan.FromSeconds(30));
            //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("com.magenic.appiumtesting.maqsregistrydemo:id/userNameField")));

            Assert.IsTrue(welcomeLbl.Displayed);
        }


        [TestMethod]
        public void InvalidLogin()
        {
            AndroidElement userName = driver.FindElementById("com.magenic.appiumtesting.maqsregistrydemo:id/userNameField");
            AndroidElement password = driver.FindElementById("com.magenic.appiumtesting.maqsregistrydemo:id/passwordField");
            AndroidElement loginBtn = driver.FindElementById("com.magenic.appiumtesting.maqsregistrydemo:id/loginButton");

            userName.SendKeys("InvalidTest");
            password.SendKeys("123456789");
            loginBtn.Click();

            AndroidElement alert = driver.FindElementById("android:id/alertTitle");

            Assert.IsTrue(alert.Displayed);
            Assert.AreEqual(alert.Text, "Invalid Credentials");
        }
    }
}