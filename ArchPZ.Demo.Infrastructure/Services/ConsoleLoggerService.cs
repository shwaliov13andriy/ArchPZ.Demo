namespace ArchPZ.Demo.Infrastructure.Services;

using ArchPZ.Demo.Application.Contracts;

public class ConsoleLoggerService : ILoggerService
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"[WARN] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
    }

    public void LogError(string message, Exception? exception = null)
    {
        Console.WriteLine($"[ERROR] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message} {exception?.Message}");
    }
}
