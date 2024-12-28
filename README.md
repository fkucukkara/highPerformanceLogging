# High-Performance Logging in .NET: Source-Generated Logger

This repository demonstrates the use of **Source-Generated Logging** in .NET for high-performance and efficient logging. By leveraging the `LoggerMessage` attribute, this approach minimizes runtime overhead, reduces memory allocations, and ensures better log performance compared to traditional logging techniques.

## Features
- **Optimized Logging**: Use of `LoggerMessage` for compile-time generated logging methods.
- **Structured Log Messages**: Clear, consistent, and parameterized log messages.
- **Event Name and Event ID**: Adds context to log entries, making them easier to identify and filter.
- **Minimal API Integration**: Demonstrates integration of source-generated logging within a Minimal API project.

## Why Use Source-Generated Logging?
Traditional logging methods in .NET involve runtime string interpolation and object boxing, which can lead to performance overhead. Source-generated logging eliminates this by generating optimized code at compile time. This approach:

- **Reduces Runtime Overhead**: No runtime string formatting or boxing of parameters.
- **Improves Performance**: Ideal for high-throughput applications.
- **Enhances Readability**: Provides well-structured, consistent log messages.

For more details, see the official documentation: [Logger Message Generator](https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator).

## Project Structure

- **`Program.cs`**: Contains the Minimal API with a weather forecast endpoint that utilizes the source-generated logger.
- **`LoggerExtensions.cs`**: Defines the logger extensions using the `LoggerMessage` attribute.
- **`WeatherForecast.cs`**: Defines the model used in the weather forecast endpoint.

## Usage

### Logger Extensions
The source-generated logger is defined in the `LoggerExtensions.cs` file:

```csharp
namespace API;

internal static partial class LoggerExtensions
{
    private const string GetWeatherForecastEvent = $"{nameof(GetWeatherForecastEvent)}";
    private const string GetWeatherForecastEventTemplate = "Getting weather forecast, item count = {Count}";

    [LoggerMessage(
        EventName = GetWeatherForecastEvent,
        Level = LogLevel.Information,
        Message = GetWeatherForecastEventTemplate)]
    internal static partial void LogGetWeatherForecast(
        this ILogger<Program> logger,
        int count);
}
```

### Minimal API Integration
The logger is used in the `/weatherforecast` endpoint in `Program.cs`:

```csharp
app.MapGet("/weatherforecast", (ILogger<Program> logger) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    logger.LogGetWeatherForecast(forecast.Length);
    return forecast;
});
```

### Running the Application
1. Clone the repository:

   ```bash
   git clone https://github.com/fkucukkara/highPerformanceLogging.git
   cd high-performance-logging
   ```

2. Build and run the application:

   ```bash
   dotnet run
   ```

3. Access the weather forecast endpoint:

   ```
   http://localhost:5000/weatherforecast
   ```

Logs will display in the console, including the event name, level, and structured message.

## Example Log Output

```
info: API.Program[0]
      Getting weather forecast, item count = 5
```

### Note
By default, the event ID and event name are not visible in the output with the default configuration. This is intentional for simplicity. To view them, you can provide additional logging configurations.

## Benefits Highlighted
- **Event Name**: `GetWeatherForecastEvent`
- **Message Template**: "Getting weather forecast, item count = {Count}"
- **Optimized Performance**: No runtime formatting or boxing.

## References
- Official Documentation: [Logger Message Generator](https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator)

## License
[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This project is licensed under the MIT License, which allows you to freely use, modify, and distribute the code. See the [`LICENSE`](LICENSE) file for full details.