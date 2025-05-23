# TyperacerFreeDataDownloader

TyperacerFreeDataDownloader is a C# console application that uses Selenium with Microsoft Edge to scrape and download a user's TypeRacer race history into a CSV file. This project is ideal for users who want to keep a local copy of their race data — especially for those who can't afford premium features.

## 🧠 Features

- Fetches and navigates through all available race history pages  
- Extracts race data: speed, accuracy, points, position, and date  
- Saves the collected data into a CSV file (`races.csv`)  
- Designed for users who want free access to their race history  

## 🛠️ Technologies Used

- C#  
- Selenium WebDriver  
- Microsoft Edge Driver  
- .NET  

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone git@github.com:sebas-gith/TyperacerFreeDataDownloader.git
cd TyperacerFreeDataDownloader
```
## 2. Open the Project

Open the project in **Visual Studio** or any IDE that supports .NET.

## 3. Install Dependencies

Make sure you have the following NuGet packages installed:

- `Selenium.WebDriver`
- `Selenium.WebDriver.MSEdgeDriver`
- `Selenium.Support`

To install them via NuGet Package Manager Console, use the following commands:

```bash
Install-Package Selenium.WebDriver
Install-Package Selenium.WebDriver.MSEdgeDriver
Install-Package Selenium.Support
```
## 4. Replace Username

In the `Main` method of `Program.cs`, replace the following line:

```csharp
string User = "alva_tipe";
```

## 📄 CSV Output Format

Each line in the `races.csv` file represents one race, with the following fields:

- **Race ID or Link**
- **Speed (WPM)**
- **Accuracy (%)**
- **Points**
- **Placement**
- **Date**

## 🧪 Notes

The script uses the visible Edge browser by default.  
To run it in headless mode, uncomment the following line in `Program.cs`:

```csharp
//option.AddArgument("--headless");
```
## 📜 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
