using System;

namespace EcoBytes.Exceptions;

public class UpgradePurchasedException : Exception
{
    public UpgradePurchasedException(string name) : base($"The upgrade \"{name}\" has already been purchased!") { }
}