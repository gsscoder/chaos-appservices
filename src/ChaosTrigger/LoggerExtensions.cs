using System;
using System.Text;
using Microsoft.Extensions.Logging;

static class LoggerExtensions
{
    public static void LogException(this ILogger logger, Exception exception)
    {
        var builder = new StringBuilder(exception.Message.Length +
            (exception.InnerException != null ? exception.InnerException.Message.Length : 0) + 18);
        builder.Append(exception.Message);
        if (exception.InnerException != null) builder.Append(
            $"\nInner exception: {exception.InnerException.Message}");
        logger.LogCritical(exception, builder.ToString());
    }
}