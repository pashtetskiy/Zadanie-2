using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Platypus screen.
/// </summary>
public sealed class PlatypusScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private readonly ScreenDefinionService _settingsService;
    private static int cursorX = 0;
    private static int cursorY = 1;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="settings">Data service reference</param>
    public PlatypusScreen(IDataService dataService, ScreenDefinionService settingsService)
    {
        _dataService = dataService;
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
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Choices, 0); // "Your available choices are:"
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Choices, 1); // "0. Exit"
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Choices, 2); // "1. List all Chimpanzee"
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Choices, 3); // "2. Create a new Chimpanzee"
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Choices, 4); // "3. Delete existing Chimpanzee"
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Choices, 5); // "4. Modify existing Chimpanzee"
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Choices, 6); // "Please enter your choice: "
            Console.SetCursorPosition(cursorX, cursorY);
            Console.SetCursorPosition(cursorX, cursorY);
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
                        if (cursorY < 5)
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
                                ListPlatypus();
                                break;
                            case 3:
                                Console.Clear();
                                AddPlatypus();
                                break;
                            case 4:
                                Console.Clear();
                                DeletePlatypus();
                                break;
                            case 5:
                                Console.Clear();
                                EditPlatypus();
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
    /// List all Platypus.
    /// </summary>
    private void ListPlatypus()
    {
        if (_dataService?.Animals?.Mammals?.Platypus is not null &&
            _dataService.Animals.Mammals.Platypus.Count > 0)
        {
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.List, 0);
            int i = 1;
            foreach (Platypus Platypus in _dataService.Animals.Mammals.Platypus)
            {
                _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.List, 1, i);
                Platypus.Display();
                i++;
            }
        }
        else
        {
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.List, 2);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Add a Platypus.
    /// </summary>
    private void AddPlatypus()
    {
        try
        {
            Platypus Platypus = AddEditPlatypus();
            _dataService?.Animals?.Mammals?.Platypus?.Add(Platypus);
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Add, 0,Platypus.Name);
        }
        catch
        {
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Add, 1);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Delete a Platypus.
    /// </summary>
    private void DeletePlatypus()
    {
        try
        {
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Delete, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Platypus? Platypus = (Platypus?)(_dataService?.Animals?.Mammals?.Platypus
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (Platypus is not null)
            {
                _dataService?.Animals?.Mammals?.Platypus?.Remove(Platypus);
                _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Delete, 1, Platypus.Name);
            }
            else
            {
                _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Delete, 2);
            }
        }
        catch
        {
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Delete, 3);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Edits an existing Platypus after choice made.
    /// </summary>
    private void EditPlatypus()
    {
        try
        {
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Edit, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Platypus? Platypus = (Platypus?)(_dataService?.Animals?.Mammals?.Platypus
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (Platypus is not null)
            {
                Platypus PlatypusEdited = AddEditPlatypus();
                Platypus.Copy(PlatypusEdited);
                _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Edit, 1);
                Platypus.Display();
            }
            else
            {
                _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Edit, 2);
            }
        }
        catch
        {
            _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.Edit, 3);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Add/edit specific Platypus.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Platypus AddEditPlatypus()
    {
        _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.AddEdit, 0);
        string? name = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.AddEdit, 1);
        string? ageAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.AddEdit, 2);
        string? heightAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.AddEdit, 3);
        string? weightAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.AddEdit, 4);
        string? habitatAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.AddEdit, 5);
        bool isMale;
        if (Console.ReadLine()?.ToLower() == "y")
            isMale = true;
        else
            isMale = false;
        _settingsService.Show(ScreensEnum.Platypus, LineEntryEnums.AddEdit, 6);
        bool isVenomous;
        if (Console.ReadLine()?.ToLower() == "y")
            isVenomous = true;
        else
            isVenomous = false;

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (heightAsString is null)
        {
            throw new ArgumentNullException(nameof(heightAsString));
        }
        if (weightAsString is null)
        {
            throw new ArgumentNullException(nameof(weightAsString));
        }
        if (habitatAsString is null)
        {
            throw new ArgumentNullException(nameof(habitatAsString));
        }
        try
        {
            int age = int.Parse(ageAsString);
            float height = float.Parse(heightAsString);
            float weight = float.Parse(weightAsString);
            float habitat = float.Parse(habitatAsString);
            Platypus Platypus = new(name, age, height, weight, habitat, isMale, isVenomous);
            return Platypus;
        }
        catch (Exception)
        {
            throw new ArgumentNullException();
        }
    }
   
    #endregion // Private Methods
}
