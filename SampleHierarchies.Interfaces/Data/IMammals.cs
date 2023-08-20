using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Mammals collection.
/// </summary>
public interface IMammals
{
    #region Interface Members

    /// <summary>
    /// Dogs, Elephants, Chimpanzee, Platypus collection.
    /// </summary>
    List<IDog> Dogs { get; set; }
    List<IAfricanElephant> AfricanElephants { get; set; }
    List<IChimpanzee> Chimpanzee { get; set; }
    List<IPlatypus> Platypus { get; set; }

    #endregion // Interface Members
}
