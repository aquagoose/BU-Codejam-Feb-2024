using Newtonsoft.Json;

namespace EcoBytes.Data;

public readonly struct CarbonFactors
{
    /// <summary>
    /// The cost of grid electricity, in £ per KW/H.
    /// </summary>
    public readonly double ElecCost;
    
    /// <summary>
    /// The cost of gas, in £ per KW/H
    /// </summary>
    public readonly double GasCost;
    
    /// <summary>
    /// The carbon factor for electricity.
    /// </summary>
    public readonly double ElecFactor;
    
    /// <summary>
    /// The carbon factor for gas.
    /// </summary>
    public readonly double GasFactor;

    public CarbonFactors(double elecCost, double gasCost, double elecFactor, double gasFactor)
    {
        ElecCost = elecCost;
        GasCost = gasCost;
        ElecFactor = elecFactor;
        GasFactor = gasFactor;
    }

    public static CarbonFactors LoadedFactors;

    public static void LoadFactorsFromJson(string json)
    {
        LoadedFactors = JsonConvert.DeserializeObject<CarbonFactors>(json);
    }
}