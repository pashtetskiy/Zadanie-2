using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using System.Runtime;

namespace SampleHierarchies.Data
{

    // Settings class
    public class Settings : ISettings
    {
        #region Properties And Ctor
        public Dictionary<ScreensEnum, ScreenDefinition> ScreenDefinitions { get; set; } = new Dictionary<ScreensEnum, ScreenDefinition>();

        /// <summary>
        /// Ctor.
        /// ScreenDefinitions
        /// </summary>

        #endregion Properties And Ctor

        #region Public Methods

        #endregion // Public Methods

        #region Private Methods
        /// <summary>
        /// Screens creator.
        /// </summary>

        #endregion // Private Methods
    }

}

