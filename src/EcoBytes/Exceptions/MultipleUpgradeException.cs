using System;

namespace EcoBytes.Exceptions;

public class MultipleUpgradeException : Exception
{
    public MultipleUpgradeException() : base("Cannot perform multiple upgrades at the same time.") { }
}