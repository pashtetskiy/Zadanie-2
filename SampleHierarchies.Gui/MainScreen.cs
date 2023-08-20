using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System.Runtime;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private readonly ScreenDefinionService _settingsService;
    /// <summary>
    /// Animals and settings screen.
    /// </summary>
    private readonly AnimalsScreen _animalsScreen;
    private readonly SettingsScreen _settingsScreen;
    private static int cursorX = 0;
    private static int cursorY = 1;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="animalsScreen">Animals screen</param>
    /// <param name="settingsScreen">Data service reference</param>
    /// <param name="settings">Animals screen</param>
    public MainScreen(
        AnimalsScreen animalsScreen,
        SettingsScreen settingsScreen,
        ScreenDefinionService settingsService
        )
    {
        _settingsService = settingsService;
        _animalsScreen = animalsScreen;
        _settingsScreen = settingsScreen;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            Console.Clear();
            _settingsService.Show(ScreensEnum.MainScreen, LineEntryEnums.Choices, 0); // "Your available choices are:"
            _settingsService.Show(ScreensEnum.MainScreen, LineEntryEnums.Choices, 1); // 0. Exit
            _settingsService.Show(ScreensEnum.MainScreen, LineEntryEnums.Choices, 2); // 1. Animals
            _settingsService.Show(ScreensEnum.MainScreen, LineEntryEnums.Choices, 3); // 2. Create a new settings
            _settingsService.Show(ScreensEnum.MainScreen, LineEntryEnums.Choices, 4); // Please enter your choice:
            Console.SetCursorPosition(cursorX, cursorY);
            Console.SetCursorPosition(cursorX, cursorY);
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
                            if (cursorY < 3)
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
                                    _settingsService.Show(ScreensEnum.MainScreen, LineEntryEnums.Exit, 0); // Goodbye
                            return;
                                case 2:
                                    _animalsScreen.Show();
                                    break;
                                case 3:
                                    _settingsScreen.Show();
                                    break;
                                default:
                                    break;
                                }
                            break;
                    }
                }
        }
    #endregion // Public Methods
    }