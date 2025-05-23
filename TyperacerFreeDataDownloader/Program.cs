using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;


namespace TyperacerFreeDataDownloader
{
    class Program
    {

        private readonly EdgeDriver _driver;
        public List<Row> Table;
        public Program(string User)
        {
            Table = new List<Row>();
            EdgeOptions option = new EdgeOptions();
            // In order to exec the navigator in the background uncomment the below line
            //option.AddArgument("--headless"); 
            _driver = new EdgeDriver(option);
            _driver.Url = $"https://data.typeracer.com/pit/race_history?user={User}";
            _driver.Navigate().GoToUrl(_driver.Url);
        }
        public static void Main(string[] args)
        {
            //Change the blow line with your typeracer username 
            string User = "alva_tipe";

            Program program = new Program(User);

            while (program.IsNotLastPage())
            {
                program.GetTableData();
                program.GoToNextPage();
            }
            //Save the table of the last page
            program.GetTableData();

            program.SaveToCSV();
            program.Quit();
        }

        public void GetTableData()
        {
            try
            {
                //Search the html of the table
                IWebElement table = _driver.FindElement(By.ClassName("Scores__Table__Body"));

                //Make a list of all children of table
                IList<IWebElement> rows = table.FindElements(By.ClassName("Scores__Table__Row"));
               
                //We iterate among all the children
                foreach(IWebElement row in rows)
                {
                    //Save in a object "Row"
                    Row current = new Row()
                    {
                        Race = Utils.FormatRace(row.FindElement(By.ClassName("profileTableHeaderUniverse")).Text),
                        Speed = Utils.FormatSpeed(row.FindElements(By.ClassName("profileTableHeaderRaces"))[0].Text),
                        Accuracy = Utils.FormatAccuracy(row.FindElements(By.ClassName("profileTableHeaderRaces"))[1].Text),
                        Points = Utils.FormatPoints(row.FindElement(By.ClassName("profileTableHeaderAvg")).Text),
                        Place = row.FindElement(By.ClassName("profileTableHeaderPoints")).Text,
                        Date = row.FindElement(By.ClassName("profileTableHeaderDate")).Text
                    };
                    
                    Table.Add(current); 
                }

                
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                
            }
        }
        public void GoToNextPage()
        {
            try
            {
                IWebElement olderButton = _driver.FindElement(By.XPath("//*[contains(text(), 'load older results')]"));
                olderButton.Click();
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.ClassName("Scores__Table__Row")).Displayed);
            }catch(Exception) { 

            }
           
        }

        public bool IsNotLastPage()
        {
            try
            {
                _driver.FindElement(By.XPath("//*[contains(text(), 'load older results')]"));
                return true;
            } catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public void SaveToCSV()
        {
            string path = "races.csv";
            Table.Reverse();

            using (StreamWriter writer = new StreamWriter(path))
            {
               foreach (Row row in Table)
               {
                    writer.WriteLine(Utils.FormatRowToCSV(row));
               }
            }
            Console.WriteLine("The data was saved correctly");
        }
    }
}
