using NServiceBus;

public class QueueEmailMessage: ICommand
{
    public byte[] Attachment { get; set; } = Array.Empty<byte>();
}