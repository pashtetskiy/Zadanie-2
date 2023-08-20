using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    
    private readonly IDataService _dataService;
    private readonly ScreenDefinionService _settingsService;
    private static int cursorX = 0;
    private static int cursorY = 1;

    /// <summary>
    /// Animals screen.
    /// </summary>

    private readonly MammalsScreen _mammalsScreen;
    private readonly MainScreen _mainScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="mammalsScreen">Animals screen</param>
    /// <param name="settings">Animals screen</param>

    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        //MainScreen mainScreen,
        ScreenDefinionService settingsService
        )
    {
        //_mainScreen = mainScreen;
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _settingsService = settingsService;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    
    public override void Show()
    {
        while (true)
        {
            Console.Clear();
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.Choices,0);  // Your available choices are:
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.Choices, 1); // 0. Exit
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.Choices, 2); // 1. Mammals
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.Choices, 3); // 2. Save to file
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.Choices, 4); // 3. Read from file
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.Choices, 5); // Please enter your choice: 
            Console.SetCursorPosition(cursorX, cursorY);
            Console.SetCursorPosition(cursorX, cursorY);
            // Validate choice
            try
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    default:

                        break;
                    case ConsoleKey.UpArrow:
                        if (cursorY > 1)
                        {
                            cursorY--;
                            Console.SetCursorPosition(cursorX, cursorY);
                            Console.SetCursorPosition(cursorX, cursorY);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursorY < 4)
                        {
                            cursorY++;
                            Console.SetCursorPosition(cursorX, cursorY);
                            Console.SetCursorPosition(cursorX, cursorY);
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (cursorY)
                        {
                            case 1:
                                Console.Clear();
                                _settingsService.Show(ScreensEnum.Default, LineEntryEnums.Exit, 0); // Going back to parent menu.
                                Thread.Sleep(750);
                                return;
                            case 2:
                                Console.Clear();
                                _mammalsScreen.Show();
                                break;
                            case 3:
                                Console.Clear();
                                SaveToFile();
                                break;
                            case 4:
                                Console.Clear();
                                ReadFromFile();
                                break;
                            default:
                                break;
                        }
                        break;
                }

            }
            catch
            {
                Console.Clear();
                _settingsService.Show(ScreensEnum.Default, LineEntryEnums.InvalidChoice, 0); // Invalid choice. Try again.
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// Save to file.
    /// </summary>
    
    private void SaveToFile()
    {
        try
        {
            _settingsService.Show(ScreensEnum.AnimalsScreen,LineEntryEnums.SaveJson, 0); // "Save data to file: "
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.SaveJson, 1); // "Data saving was successful."
            Thread.Sleep(1000);
        }
        catch
        {
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.SaveJson, 2); // "Data saving was not successful."
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    
    private void ReadFromFile()
    {
        try
        {
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.ReadJson, 0); // "Read data from file: "
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Read(fileName);
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.ReadJson, 1); // "Data reading was successful."
            Thread.Sleep(1000);
        }
        catch
        {
            _settingsService.Show(ScreensEnum.AnimalsScreen, LineEntryEnums.ReadJson, 2); // "Data reading was not successful."
            Thread.Sleep(1000);
        }
    }

    #endregion // Private Methods
}
