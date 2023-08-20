using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using Newtonsoft;
using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;

namespace SampleHierarchies.Services;

/// <summary>
/// Settings service.
/// </summary>
/// 
public class ScreenDefinionService : IScreenDefinionService
{
    #region ISettings Implementation

    private Settings settings = new Settings();
   
    /// <inheritdoc/>

    #region Public Methods

    // Overloaded methods for showing lines on the screen

    public void Show(ScreensEnum screensEnum, LineEntryEnums lineEntryEnums, int id)
    {
        JsonLoad();
        Console.BackgroundColor = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].BackgroundColor;
        Console.ForegroundColor = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].ForegroundColor;
        Console.WriteLine(settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].Text);
        Console.ResetColor();
    }
    public void Show(ScreensEnum screensEnum, LineEntryEnums lineEntryEnums, int id, int arg)
    {
        JsonLoad();
        Console.BackgroundColor = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].BackgroundColor;
        Console.ForegroundColor = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].ForegroundColor;
        string line = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].Text;
        string  result = line.Replace("arg", arg.ToString());
        Console.WriteLine(result);
        Console.ResetColor();
    }
    public void Show(ScreensEnum screensEnum, LineEntryEnums lineEntryEnums, int id, string arg)
    {
        JsonLoad();
        Console.BackgroundColor = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].BackgroundColor;
        Console.ForegroundColor = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].ForegroundColor;
        string line = settings.ScreenDefinitions[screensEnum].Lines[lineEntryEnums].LineEntries[id].Text;
        string result = line.Replace("arg", arg.ToString());
        Console.WriteLine(result);
        Console.ResetColor();
    }
    public void JsonLoad()
    {
        try
        
        {
            string jsonLanguage = File.ReadAllText("Settings.cfg");
            string jsonSource = File.ReadAllText($"{jsonLanguage}.json");
            settings = JsonConvert.DeserializeObject<Settings>(jsonSource);
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to upload json");
        }

    }
    #endregion // Public Methods
    #endregion // ISettings Implementation

}