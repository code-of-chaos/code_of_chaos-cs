// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CliArgsParser;
using Microsoft.Extensions.DependencyInjection;
using Workfloor.Commands;

namespace Workfloor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
internal static class Program {
    public async static Task Main(string[] _) {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<DataHolder>();
        
        serviceCollection.AddCliArgsParser(configuration =>
            configuration
                .SetConfig(new CliArgsParserConfig {
                    Overridable = true,
                    GenerateShortNames = true
                })
                .AddFromType<DoubleLinkedListCommands>()
        );

        ServiceProvider provider = serviceCollection.BuildServiceProvider();
        var cliParser = provider.GetRequiredService<ICliParser>();

        await cliParser.StartParsingAsync();
    }
}
