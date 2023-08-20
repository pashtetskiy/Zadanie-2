using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Interfaces.Services;

public interface IScreenDefinionService
{
    #region Interface Members
    /// <summary>
    /// Interface describing an Settings Definion Service.
    /// 
    /// <summary>
    /// Show gui.
    /// </summary>

    public void Show(ScreensEnum screensEnum, LineEntryEnums lineEntryEnums, int id);

    #endregion // Interface Members
}
