using Common;

namespace Processor;

using NServiceBus;

public class StartCmdHandler : IHandleMessages<StartCmd>
{
    
    public async Task Handle(StartCmd message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Starting processing command, handling batch: {message.BatchId}");
        
        // Create a byte array with the specified size
        var attachmentData = new byte[170000];
        //var attachmentData = new byte[30000];
            
        new Random().NextBytes(attachmentData);
        
        await Task.WhenAll(new int[100].Select(id => new QueueEmailMessage() { Attachment =  attachmentData}).ToArray().Select(context.SendLocal));
        Console.WriteLine($"100 messages sent while handling batch: {message.BatchId}");
    }
}