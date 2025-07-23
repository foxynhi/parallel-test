using OpenQA.Selenium;

namespace ParalelTest.Base
{  public class BasePage
  {
    protected IWebDriver driver;

    public BasePage()
    {
      driver = DriverManager.GetDriver();
    }
    public IWebElement Find(By locator)
    {
      return driver.FindElement(locator);
    }
    public string GetText(By locator)
    {
      return Find(locator).Text;
    }
    public void Clear(By locator)
    {
      Find(locator).Clear();
    }
    public void Click(By locator)
    {
      Find(locator).Click();
    }
    public void SendKeys(By locator, string text)
    {
      Find(locator).SendKeys(text);
    }
  }
}
