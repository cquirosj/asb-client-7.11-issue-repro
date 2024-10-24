using NServiceBus;

public class QueueEmailMessageHandler : IHandleMessages<QueueEmailMessage>
{
    public static int Processed = 0;
    
    public Task Handle(QueueEmailMessage message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Processed {++Processed} message(s) -  MessageId: {context.MessageId}");
        
        return Task.CompletedTask;
    }
}