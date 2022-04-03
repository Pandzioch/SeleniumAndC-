using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Royale.Pages;

namespace Royal.Tests
{
    public class CardTest
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeEach()
        {
            driver = new ChromeDriver(Path.GetFullPath(@"../../../../"+ "drivers"));
        }
        [TearDown]
        public void AfterEach(){
            driver.Quit();
        }

        [Test]
        public void Ice_Spirit_is_on_Cards_Page()
        {
            //1.Go to stats royale
            driver.Url="https://statsroyale.com";
            //2.Click on the cards link
            // driver.FindElement(By.CssSelector("a[href='/cards']")).Click();
            var cardsPage = new CardsPage(driver);
            var icespirit = cardsPage.GoTo().GetCardByName("Ice Spirit");
            //3. Assert ice spirit is being displayed
            var iceSpirit = driver.FindElement(By.CssSelector("a[href*='Ice+Spirit']"));
            Assert.That(iceSpirit.Displayed);
        }
        [Test]
        public void Ice_Spirit_headers_are_correct_on_Card_Details_Page()
        {
            //1.Go to stats royale
            driver.Url="https://statsroyale.com";
            //2.Click on the cards link
            driver.FindElement(By.CssSelector("a[href='/cards']")).Click();
            //3. Assert ice spirit is being displayed
            driver.FindElement(By.CssSelector("a[href*='Ice+Spirit']")).Click();
            //4. Assert basic header stats
            var cardname = driver.FindElement(By.CssSelector("[class*='cardName']")).Text;
            var cardCategories = driver.FindElement(By.CssSelector(".card__rarity")).Text.Split(", ");
            var cardType = cardCategories[0];
            var cardArena = cardCategories[1];
            var cardRarity = driver.FindElement(By.CssSelector(".card__common")).Text;

            Assert.AreEqual("Ice Spirit", cardname);
            Assert.AreEqual("Troop", cardType);
            Assert.AreEqual("Arena 8", cardArena);
            Assert.AreEqual("Common", cardRarity);
        }
    }
}