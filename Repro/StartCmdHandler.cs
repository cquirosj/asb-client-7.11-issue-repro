using Common;

namespace Processor;

using NServiceBus;

public class StartCmdHandler : IHandleMessages<StartCmd>
{
    
    public async Task Handle(StartCmd message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Starting processing command, handling batch: {message.BatchId}");
        
        var attachmentData = new byte[170000];
        new Random().NextBytes(attachmentData);

        var numberOfMessages = 100;
        
        await Task.WhenAll(new int[numberOfMessages].Select(id => new QueueEmailMessage() { Attachment =  attachmentData}).ToArray().Select(context.SendLocal));
        Console.WriteLine($"{numberOfMessages} messages sent while handling batch: {message.BatchId}");
    }
}