namespace SampleHierarchies.Services.Tests
{
    public class UT_SettingsService
    {
        [Fact]
        public void MainScreen_Choices_0_YourAvailableChoicesAre()
        {
            // arrange
            ScreenDefinionService settingsService = new ScreenDefinionService();
            StringWriter stringWriter = new StringWriter();
            //act 
            Console.SetOut(stringWriter);

            settingsService.Show(Enums.ScreensEnum.MainScreen, Enums.LineEntryEnums.Choices, 0);

            string consoleOutput = stringWriter.ToString();
            string[] consoleLines = consoleOutput.Split(Environment.NewLine);
            string lastConsoleLine = consoleLines[consoleLines.Length - 2];

            //assert
            Assert.True(lastConsoleLine == "Your available choices are:");
        }
    }
}