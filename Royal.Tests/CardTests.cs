using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Royale.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;


namespace Royal.Tests
{
    public class CardTest
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var driver = new ChromeDriver();
            driver.Url="https://statsroyale.com";
        }
        [TearDown]
        public void AfterEach(){
            driver.Quit();
        }

        [Test]
        public void Ice_Spirit_is_on_Cards_Page()
        {
            var cardsPage = new CardsPage(driver);
            var iceSpirit = cardsPage.GoTo().GetCardByName("Ice Spirit");
            Assert.That(iceSpirit.Displayed);
        }
        [Test]
        public void Ice_Spirit_headers_are_correct_on_Card_Details_Page()
        {
            new CardsPage(driver).GoTo().GetCardByName("Ice Spirit").Click();

            var cardDetails = new CardDetailsPage(driver);
            var (category, arena) = cardDetails.GetCardCategory();
            var cardName = cardDetails.Map.CardName.Text;
            var cardRarity = cardDetails.Map.CardRarity.Text;

            Assert.AreEqual("Ice Spirit", cardName);
            Assert.AreEqual("Troop", category);
            Assert.AreEqual("Arena 8", arena);
            Assert.AreEqual("Common", cardRarity);
        }
    }
}