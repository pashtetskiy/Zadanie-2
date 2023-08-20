using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Dog screen.
/// </summary>
public sealed class DogsScreen : Screen
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
    public DogsScreen(IDataService dataService,ScreenDefinionService settingsService)
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
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Choices, 0); // "Your available choices are:"
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Choices, 1); // "0. Exit"
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Choices, 2); // "1. List all dogs"
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Choices, 3); // "2. Create a new dog"
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Choices, 4); // "3. Delete existing dog"
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Choices, 5); // "4. Modify existing dog"
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Choices, 6); // "Please enter your choice: "
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
                                ListDogs();
                                break;
                            case 3:
                                Console.Clear();
                                AddDog();
                                break;
                            case 4:
                                Console.Clear();
                                DeleteDog();
                                break;
                            case 5:
                                Console.Clear();
                                EditDogMain();
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
    /// List all dogs.
    /// </summary>
    private void ListDogs()
    {
        if (_dataService?.Animals?.Mammals?.Dogs is not null &&
            _dataService.Animals.Mammals.Dogs.Count > 0)
        {
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.List, 0);
            int i = 1;
            foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
            {
                _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.List, 1, i);
                dog.Display();
                i++;
            }
        }
        else
        {
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.List, 2);
        }
        Console.ReadLine();
        Console.ResetColor();
    }

    /// <summary>
    /// Add a dog.
    /// </summary>
    private void AddDog()
    {
        try
        {
            Dog dog = AddEditDog();
            _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Add, 0, dog.Name);
        }
        catch
        {
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Add, 1);
        }
        Console.ReadLine();


    }

    /// <summary>
    /// Deletes a dog.
    /// </summary>
    private void DeleteDog()
    {
        try
        {
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Delete, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
                _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Delete, 1, dog.Name);
            }
            else
            {
                _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Delete, 2);
            }
        }
        catch
        {
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Delete, 3);
        }
        Console.ReadLine();

    }

    /// <summary>
    /// Edits an existing dog after choice made.
    /// </summary>
    private void EditDogMain()
    {
        try
        {
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Edit, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                Dog dogEdited = AddEditDog();
                dog.Copy(dogEdited);
                _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Edit, 1);
                dog.Display();
            }
            else
            {
                _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Edit, 2);
            }
        }
        catch
        {
            _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.Edit, 3);

        }
        Console.ResetColor();
        Console.ReadLine();

    }

    /// <summary>
    /// Adds/edit specific dog.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Dog AddEditDog()
    {
        _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.AddEdit, 0);
        string? name = Console.ReadLine();
        _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.AddEdit, 1);
        string? ageAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.DogsScreen, LineEntryEnums.AddEdit, 2);
        string? breed = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (breed is null)
        {
            throw new ArgumentNullException(nameof(breed));
        }
        int age = Int32.Parse(ageAsString);
        Dog dog = new(name, age, breed);

        return dog;
    }

    #endregion // Private Methods
}
