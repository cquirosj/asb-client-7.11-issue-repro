using NServiceBus;

namespace Common;

public class StartCmd: ICommand
{
    public string BatchId { get; set; } = string.Empty;
}