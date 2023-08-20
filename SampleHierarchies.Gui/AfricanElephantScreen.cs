using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Runtime.ConstrainedExecution;

namespace SampleHierarchies.Gui;

/// <summary>
/// African Elephant screen.
/// </summary>
public sealed class AfricanElephantScreen : Screen
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
    public AfricanElephantScreen(IDataService dataService, ScreenDefinionService settingsService)
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
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Choices, 0); // "Your available choices are:"
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Choices, 1); // "0. Exit"
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Choices, 2); // "1. List all Elephant's"
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Choices, 3); // "2. Create a new Elephant"
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Choices, 4); // "3. Delete existing Elephant" 
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Choices, 5); // "4. Modify existing Elephant"
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Choices, 6); // "Please enter your choice: "
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
                                ListAfricanElephants();
                                break;
                            case 3:
                                Console.Clear();
                                AddAfricanElephant();
                                break;
                            case 4:
                                Console.Clear();
                                DeleteAfricanElephant();
                                break;
                            case 5:
                                Console.Clear();
                                EditAfricanElephant();
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
    /// List all African Elephants.
    /// </summary>
    private void ListAfricanElephants()
    {
        if (_dataService?.Animals?.Mammals?.AfricanElephants is not null &&
            _dataService.Animals.Mammals.AfricanElephants.Count > 0)
        {
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.List, 0);
            int i = 1;
            foreach (AfricanElephant AfricanElephant in _dataService.Animals.Mammals.AfricanElephants)
            {
                _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.List, 1, i);
                AfricanElephant.Display();
                i++;
            }
        }
        else
        {
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.List, 2);
        }
        Console.ReadLine();

    }

    /// <summary>
    /// Add a African Elephant.
    /// </summary>
    private void AddAfricanElephant()
    {
        try
        {
            AfricanElephant AfricanElephant = AddEditAfricanElephant();
            _dataService?.Animals?.Mammals?.AfricanElephants?.Add(AfricanElephant);
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Add, 0, AfricanElephant.Name);
        }
        catch
        {
            _settingsService.Show(ScreensEnum.Default, LineEntryEnums.InvalidChoice, 0);
        }
        Console.ReadLine();

    }

    /// <summary>
    /// Delete a African Elephant.
    /// </summary>
    private void DeleteAfricanElephant()
    {
        try
        {
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Delete, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            AfricanElephant? AfricanElephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (AfricanElephant is not null)
            {
                _dataService?.Animals?.Mammals?.AfricanElephants?.Remove(AfricanElephant);
                _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Delete, 1, AfricanElephant.Name);
            }
            else
            {
                _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Delete, 2);
            }
        }
        catch
        {
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Delete, 3);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Edits an existing African Elephant after choice made.
    /// </summary>
    private void EditAfricanElephant()
    {
        try
        {
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Edit, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            AfricanElephant? AfricanElephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (AfricanElephant is not null)
            {
                AfricanElephant AfricanElephantEdited = AddEditAfricanElephant();
                AfricanElephant.Copy(AfricanElephantEdited);
                _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Edit, 1);
                AfricanElephant.Display();
            }
            else
            {
                _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Edit, 2);
            }
        }
        catch
        {
            _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.Edit, 3);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Add/edit specific African Elephant.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private AfricanElephant AddEditAfricanElephant()
    {
        _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.AddEdit, 0);
        string? name = Console.ReadLine();
        _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.AddEdit, 1);
        string? ageAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.AddEdit, 2);
        string? heightAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.AddEdit, 3);
        string? weightAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.AddEdit, 4);
        string? tuskLenghtAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.AddEdit, 5);
        string? longLifespanAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.AfricanElephantScreen, LineEntryEnums.AddEdit, 6);
        string? socialBehavior = Console.ReadLine();

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
        if (tuskLenghtAsString is null)
        {
            throw new ArgumentNullException(nameof(tuskLenghtAsString));
        }
        if (longLifespanAsString is null)
        {
            throw new ArgumentNullException(nameof(longLifespanAsString));
        }
        if (socialBehavior is null)
        {
            throw new ArgumentNullException(nameof(socialBehavior));
        }
        try
        {
            int age = int.Parse(ageAsString);
            float height = float.Parse(heightAsString);
            float weight = float.Parse(weightAsString);
            float tuskLenght = float.Parse(tuskLenghtAsString);
            int longLifespan = int.Parse(longLifespanAsString);
            AfricanElephant AfricanElephant = new(name, age, height, weight, tuskLenght, longLifespan, socialBehavior);
            return AfricanElephant;
        }
        catch (Exception)
        {
            throw new ArgumentNullException();
        }
    }
   
    #endregion // Private Methods
}
