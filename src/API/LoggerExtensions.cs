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