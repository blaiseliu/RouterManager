using RouterCommand.ViewModels;
using Serilog;

namespace RouterCommand.LoggerHelpers
{
    public static class LoggerExtensions
    {
        internal static void  DeviceStatusInformation(this ILogger logger,DeviceStatusSummary summary)
        {
            logger.Information($"Device: {summary.Device}");
            logger.Information($"Status: {summary.Status}");
            logger.Information($"Device Name: {summary.DeviceName}");
            logger.Information($"IP Address: {summary.IPAddress}");
            logger.Information($"MAC Address: {summary.MacAddress}");
            logger.Information($"Connection Type: {summary.ConnectionType}");
        }
    }
}