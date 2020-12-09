using System;

namespace RouterCommand.Enums.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class MacAddressAttribute : Attribute
    {
        public MacAddressAttribute(string macAddress)
        {
            MacAddress = macAddress;
        }

        public string MacAddress { get; }
    }
}