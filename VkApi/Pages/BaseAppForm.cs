using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VkApi
{
    public class BaseAppForm : Form
    {
        protected BaseAppForm(By locator, string name) : base(locator, name)
        {
        }
    }
}