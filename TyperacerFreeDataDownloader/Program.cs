using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.IO;


namespace TyperacerFreeDataDownloader
{
    class Program
    {

        private readonly EdgeDriver _driver;
        public List<Row> Table;
        public Program()
        {
            Table = new List<Row>();
            EdgeOptions option = new EdgeOptions();
            //option.AddArgument("--headless");
            _driver = new EdgeDriver(option);
            _driver.Url = "https://data.typeracer.com/pit/race_history?user=alva_tipe";
            _driver.Navigate().GoToUrl(_driver.Url);
        }
        public static void Main(string[] args)
        {

            Program program = new Program();

            
            while (program.IsNotLastPage())
            {
                program.GetTableData();
                program.GoToNextPage();
            }
            program.SaveToCSV();
            program.Quit();
        }

        public void GetTableData()
        {
            try
            {
                //Search for the html of table
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
            }catch(Exception ex) { 

            }
           
        }

        public bool IsNotLastPage()
        {
            try
            {
                _driver.FindElement(By.XPath("//*[contains(text(), 'load older results')]"));
                return true;
            } catch (NoSuchElementException ex)
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
            Console.WriteLine("The file saved correctly");
        }
    }
}
