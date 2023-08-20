using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Animals screen.
    /// </summary>

    private readonly DogsScreen _dogsScreen;
    private readonly AfricanElephantScreen _africanElephantScreen;
    private readonly ChimpanzeeScreen _chimpanzeeScreen;
    private readonly ScreenDefinionService _settingsService;
    private readonly PlatypusScreen _platypusScreen;
    private static int cursorX = 0;
    private static int cursorY = 1;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    public MammalsScreen(DogsScreen dogsScreen, ScreenDefinionService settingsService, AfricanElephantScreen africanElephantScreen, ChimpanzeeScreen chimpanzeeScreen, PlatypusScreen platypusScreen)
    {
        _dogsScreen = dogsScreen;
        _settingsService = settingsService;
        _africanElephantScreen = africanElephantScreen;
        _chimpanzeeScreen = chimpanzeeScreen;
        _platypusScreen = platypusScreen;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            Console.Clear();
            _settingsService.Show(ScreensEnum.MammalScreen, LineEntryEnums.Choices, 0); // "Your available choices are:"
            _settingsService.Show(ScreensEnum.MammalScreen, LineEntryEnums.Choices, 1); // "0. Exit"
            _settingsService.Show(ScreensEnum.MammalScreen, LineEntryEnums.Choices, 2); // "1. Dogs"
            _settingsService.Show(ScreensEnum.MammalScreen, LineEntryEnums.Choices, 3); // "2. African Elephant"
            _settingsService.Show(ScreensEnum.MammalScreen, LineEntryEnums.Choices, 4); // "3. Chimpanzee"
            _settingsService.Show(ScreensEnum.MammalScreen, LineEntryEnums.Choices, 5); // "4. Platypus"
            _settingsService.Show(ScreensEnum.MammalScreen, LineEntryEnums.Choices, 6); // "Please enter your choice: "
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
                                _dogsScreen.Show();
                                break;
                            case 3:
                                Console.Clear();
                                _africanElephantScreen.Show();
                                break;
                            case 4:
                                Console.Clear();
                                _chimpanzeeScreen.Show();
                                break;
                            case 5:
                                Console.Clear();
                                _platypusScreen.Show();
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
}
