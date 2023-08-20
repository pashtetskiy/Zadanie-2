using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Services;
using System;
using System.Net.Http.Json;

namespace SampleHierarchies.Gui;

// Settings Screen

public sealed class SettingsScreen : Screen
{
    #region Properties And Ctor

    private readonly ScreenDefinionService _settingsService;
    private static int cursorX = 0;
    private static int cursorY = 1;
    public SettingsScreen(ScreenDefinionService settingsService)
    {
        _settingsService = settingsService;
    }

    /// <summary>
    /// Ctor
    /// </summary>

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {

        while (true)
        {
            Console.Clear();
            _settingsService.Show(ScreensEnum.SettingsScreen, LineEntryEnums.Choices, 0);
            _settingsService.Show(ScreensEnum.SettingsScreen, LineEntryEnums.Choices, 1);
            _settingsService.Show(ScreensEnum.SettingsScreen, LineEntryEnums.Choices, 2);
            _settingsService.Show(ScreensEnum.SettingsScreen, LineEntryEnums.Choices, 3);
            _settingsService.Show(ScreensEnum.SettingsScreen, LineEntryEnums.Choices, 4);
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
                                _settingsService.Show(ScreensEnum.Default, LineEntryEnums.Exit, 0); // Going back to parent menu.
                                Thread.Sleep(750);
                                return;
                            case 2:
                                Console.Clear();
                                File.WriteAllText("Settings.cfg", "Settings_Eng");
                                break;
                            case 3:
                                Console.Clear();
                                File.WriteAllText("Settings.cfg", "Settings_Pl");
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
}

    #endregion // Public Methods
