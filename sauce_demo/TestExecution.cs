using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using System.Configuration;
using sauce_demo.WebApp.Product;
using sauce_demo.WebApp.Your_Cart;
using OpenQA.Selenium.DevTools.V111.Emulation;
using sauce_demo.WebApp.CheckOut;


namespace sauce_demo
{
    [TestClass]
    public class TestExecution
    {

        public TestContext instance;

        public TestContext TestContext
        {
            set { instance = value; }
            get { return instance; }

        }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {

        }
        [ClassCleanup()]
        public static void ClassCleanup()
        {

        }
        [TestInitialize()]
        public void TestInit()
        {
            CorePage.SeleniumInit(ConfigurationManager.AppSettings["DeviceBrowser"]);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            CorePage.driver.Close();
        }
     


        LoginPage loginpage = new LoginPage();
        ProductPage productpage = new ProductPage();
        YourCart yourcart = new YourCart();
        CheckOut checkout = new CheckOut();
        

        [TestMethod]
        [TestCategory("Postive_TC")]
        public void LoginwithValidUserandValidPasswordTC001()
        {

            loginpage.login("https://www.saucedemo.com/", "standard_user", "secret_sauce");
            string actualtext = CorePage.driver.FindElement(By.ClassName("app_logo")).Text;
            Assert.AreEqual("Swag Labs", actualtext);

        }

        [TestMethod]
        [TestCategory("Negative_TC")]
        public void LoginwithValidUserandInValidPasswordTC002()
        {

            loginpage.login("https://www.saucedemo.com/", "problem_user", "secret_112");
            string actualtext = CorePage.driver.FindElement(By.CssSelector("#login_button_container > div > form > div.error-message-container.error")).Text;
            Assert.AreEqual("Epic sadface: Username and password do not match any user in this service", actualtext);

        }

        [TestMethod]
        [TestCategory("Negative_TC")]
        public void LoginwithInValidUserandValidPasswordTC003()
        {

            loginpage.login("https://www.saucedemo.com/", "talha", "secret_sauce");
            string actualtext = CorePage.driver.FindElement(By.CssSelector("#login_button_container > div > form > div.error-message-container.error")).Text;
            Assert.AreEqual("Epic sadface: Username and password do not match any user in this servic", actualtext);

        }

        [TestMethod]
        [TestCategory("Select_Product")]
        public void SelectProduct_TC004()
        {

            loginpage.login("https://www.saucedemo.com/", "standard_user", "secret_sauce");
            productpage.products();

        }

        [TestMethod]
        [TestCategory("Cart")]
        public void YourCart_TC005()
        {

            loginpage.login("https://www.saucedemo.com/", "standard_user", "secret_sauce");
            productpage.products();
            yourcart.Cart();

        }
        [TestMethod]
        [TestCategory("Check_Out")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML","LoginData.xml", "CheckoutWithAllValidDetails_TC006", DataAccessMethod.Sequential)]
        public void CheckoutWithAllValidDetails_TC006()
        {
    
            string url = TestContext.DataRow["url"].ToString();
            string user = TestContext.DataRow["username"].ToString();
            string pass = TestContext.DataRow["password"].ToString();


            loginpage.login(url,user,pass);
            productpage.products();
            yourcart.Cart();
            checkout.checkOut();
          
        }

        [TestMethod]
        [TestCategory("Check_Out_2")]
        public void CheckoutWithoutEnteringZipCode_TC007()
        {

            loginpage.login("https://www.saucedemo.com/", "standard_user", "secret_sauce");
            productpage.products();
            yourcart.Cart();
            checkout.checkOut2();
            string actualtext = CorePage.driver.FindElement(By.CssSelector("div.page_wrapper div.checkout_info_container div.checkout_info_wrapper form:nth-child(1) div.checkout_info > div.error-message-container.error:nth-child(4)")).Text;
            Assert.AreEqual("Error: Postal Code is required", actualtext);

        }


    }
}


