namespace EcoBytes.Data;

public readonly struct Upgrade
{
    public readonly string Name;
    public readonly int Cost;
    public readonly int BuildTime;
    public readonly int ElecImpact;
    public readonly int GasImpact;
    public readonly string Description;

    public Upgrade(string name, int cost, int buildTime, int elecImpact, int gasImpact, string description)
    {
        Name = name;
        Cost = cost;
        BuildTime = buildTime;
        ElecImpact = elecImpact;
        GasImpact = gasImpact;
        Description = description;
    }
}