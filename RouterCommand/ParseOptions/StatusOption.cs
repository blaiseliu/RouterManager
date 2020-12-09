using CommandLine;
using RouterCommand.Enums;

namespace RouterCommand.ParseOptions
{
    [Verb("status", HelpText = "check / change status")]
    public class StatusOption:ParseOptionBase
    {
        [Option('d',"device", Required = false,HelpText = "Choose a device.")]
        public Device Device { get; set; }
        [Option('c', "change", Required = true, HelpText = "Target device status.")]
        public DeviceStatus Status { get; set; }
    }

    public abstract class ParseOptionBase:IParseOption
    {
    }

    public interface IParseOption
    {
    }
}
