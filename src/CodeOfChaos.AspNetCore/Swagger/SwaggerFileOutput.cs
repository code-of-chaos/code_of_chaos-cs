// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.AspNetCore.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace CodeOfChaos.AspNetCore.Swagger;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// The SwaggerFileOutput class is responsible for storing the Swagger document to a file.
/// </summary>
public static class SwaggerFileOutput {
    /// <summary>
    /// Writes the Swagger document to a file.
    /// </summary>
    /// <param name="environmentSwitcher"></param>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    public async static Task StoreToFile(EnvironmentSwitcher environmentSwitcher, WebApplication app) {
        if (!environmentSwitcher.IsRunningInDocker()) {
            try {
                OpenApiDocument? swaggerJson = app.Services.GetRequiredService<ISwaggerProvider>().GetSwagger("v1");
                string outputFilePath = Path.Combine(AppContext.BaseDirectory, "swagger.json");
                await File.WriteAllTextAsync(outputFilePath, JsonConvert.SerializeObject(swaggerJson));
            }
            catch (Exception ex) {
                var logger = app.Services.GetRequiredService<ILogger>();
                logger.Warning(ex, "swagger.json could not be generated.");
            }
        }
    }
}
