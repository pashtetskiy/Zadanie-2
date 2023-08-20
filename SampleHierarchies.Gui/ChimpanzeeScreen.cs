using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Formats.Asn1;

namespace SampleHierarchies.Gui;

/// <summary>
/// Chimpanzee screen.
/// </summary>
public sealed class ChimpanzeeScreen : Screen
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
    /// /// <param name="settings">Data service reference</param>
    public ChimpanzeeScreen(IDataService dataService, ScreenDefinionService settingsService)
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
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Choices, 0); // "Your available choices are:"
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Choices, 1); // "0. Exit" 
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Choices, 2); // "1. List all Chimpanzee"
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Choices, 3); // "2. Create a new Chimpanzee"
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Choices, 4); // "3. Delete existing Chimpanzee"
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Choices, 5); // "4. Modify existing Chimpanzee"
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Choices, 6); // "Please enter your choice: "

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
                                ListChimpanzee();
                                break;
                            case 3:
                                Console.Clear();
                                AddChimpanzee();
                                break;
                            case 4:
                                Console.Clear();
                                DeleteChimpanzee();
                                break;
                            case 5:
                                Console.Clear();
                                EditChimpanzee();
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
    /// List all Chimpanzee.
    /// </summary>
    private void ListChimpanzee()
    {
        if (_dataService?.Animals?.Mammals?.Chimpanzee is not null &&
            _dataService.Animals.Mammals.Chimpanzee.Count > 0)
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.List, 0);
            int i = 1;
            foreach (Chimpanzee Chimpanzee in _dataService.Animals.Mammals.Chimpanzee)
            {
                _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.List, 1,i);
                Chimpanzee.Display();
                i++;
            }
        }
        else
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.List, 2);

        }
        Console.ReadLine();
    }

    /// <summary>
    /// Add a Chimpanzee.
    /// </summary>
    private void AddChimpanzee()
    {
        try
        {
            Chimpanzee Chimpanzee = AddEditChimpanzee();
            _dataService?.Animals?.Mammals?.Chimpanzee?.Add(Chimpanzee);
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Add, 0, Chimpanzee.Name);
        }
        catch
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Add, 1);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Delete a Chimpanzee.
    /// </summary>
    private void DeleteChimpanzee()
    {
        try
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Delete, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Chimpanzee? Chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzee
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (Chimpanzee is not null)
            {
                _dataService?.Animals?.Mammals?.Chimpanzee?.Remove(Chimpanzee);
                _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Delete, 1, Chimpanzee.Name);
            }
            else
            {
                _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Delete, 2);
            }
        }
        catch
        {
                _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Delete, 3);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Edits an existing Chimpanzee after choice made.
    /// </summary>
    private void EditChimpanzee()
    {
        try
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Edit, 0);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Chimpanzee? Chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzee
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (Chimpanzee is not null)
            {
                Chimpanzee ChimpanzeeEdited = AddEditChimpanzee();
                Chimpanzee.Copy(ChimpanzeeEdited);
                _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Edit, 1);
                Chimpanzee.Display();
            }
            else
            {
                _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Edit, 2);
            }
        }
        catch
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.Edit, 3);
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Adds/edit specific Chimpanzee.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Chimpanzee AddEditChimpanzee()
    {
        _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 0);
        string? name = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 1);
        string? ageAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 2);
        bool iopposableThumbs;
        string? describeofopposableThumbs;
        if (Console.ReadLine()?.ToLower() == "y")
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 3);
            describeofopposableThumbs = Console.ReadLine();
            iopposableThumbs = true;
        }
        else
        {
            iopposableThumbs = false; describeofopposableThumbs = "This chimpanzee don't have opposable thumbs";
        };

        _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 4);
        string? complexSocialBehavior = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 5);
        bool itoolUse;
        string? descireofusetools;
        if (Console.ReadLine()?.ToLower() == "y")
        {
            _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 6);
            descireofusetools = Console.ReadLine();
            itoolUse = true;
        }
        else
        {
            itoolUse = false; descireofusetools = "This chimpanzee don't know how to use tools";
        };
        _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 7);
        string? highIntelligenceAsString = Console.ReadLine();
        _settingsService.Show(ScreensEnum.Chimpanzee, LineEntryEnums.AddEdit, 8);
        string? flexibleDiet = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (complexSocialBehavior is null)
        {
            throw new ArgumentNullException(nameof(complexSocialBehavior));
        }
        if (highIntelligenceAsString is null)
        {
            throw new ArgumentNullException(nameof(highIntelligenceAsString));
        }
        if (flexibleDiet is null)
        {
            throw new ArgumentNullException(nameof(flexibleDiet));
        }
        if (describeofopposableThumbs is null)
        {
            throw new ArgumentNullException(nameof(describeofopposableThumbs));
        }
        if (descireofusetools is null)
        {
            throw new ArgumentNullException(nameof(descireofusetools));
        }
        int age = int.Parse(ageAsString);
        int highIntelligence = int.Parse(highIntelligenceAsString);

        Chimpanzee Chimpanzee = new(name, age, iopposableThumbs, describeofopposableThumbs, complexSocialBehavior, itoolUse, descireofusetools, highIntelligence, flexibleDiet);

        return Chimpanzee;
    }

    #endregion // Private Methods
}
