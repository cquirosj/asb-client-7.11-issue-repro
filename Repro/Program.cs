// See https://aka.ms/new-console-template for more information

using Azure.Messaging.ServiceBus;
using Common;
using NServiceBus;

var cfg = new EndpointConfiguration("Repro");

const string connectionString = "[UPDATE CONNECTION STRING HERE]";
        
cfg.UseTransport<AzureServiceBusTransport>()
    .Transactions(TransportTransactionMode.SendsAtomicWithReceive)
    .CustomRetryPolicy(new ServiceBusRetryOptions
    
    {
    
        Mode = ServiceBusRetryMode.Fixed,
    
        Delay = TimeSpan.FromSeconds(2.0),
    
        MaxRetries = 30,
    
        MaxDelay = TimeSpan.FromSeconds(120),
    
        TryTimeout = TimeSpan.FromSeconds(35)
    
    })
    .ConnectionString(connectionString);

cfg.EnableInstallers();
cfg.Recoverability().Immediate(c => c.NumberOfRetries(1));
cfg.Recoverability().Delayed(c => c.NumberOfRetries(0));

var ep = await Endpoint.Start(cfg);

Console.WriteLine("(Q)uit, (S)end");

var running = true;

while (running)
{
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.Q:
            running = false;
            break;
        case ConsoleKey.S:

            await ep.SendLocal(new StartCmd(){ BatchId = Guid.NewGuid().ToString()});
            Console.WriteLine("Start command sent");
            break;
    }
}

Console.WriteLine("Stopping");
await ep.Stop();