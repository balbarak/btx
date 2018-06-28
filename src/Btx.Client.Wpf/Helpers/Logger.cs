using Btx.Client.Wpf.AppEventArgs;
using Btx.Client.Wpf.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Wpf.Helpers
{
    public class BtxLoggerProvider : ILoggerProvider
    {
        public Logger CurrentLogger { get; private set; }

        public ILogger CreateLogger(string categoryName)
        {
            if (CurrentLogger == null)
                CurrentLogger = new Logger();

            return CurrentLogger;
        }

        public void Dispose()
        {

        }
    }

    public class Logger : ILogger
    {
        public event EventHandler OnWriteLog;

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var date = DateTime.Now;
            
            OnWriteLog?.Invoke(this,new LogEventArgs()
            {
                LogEntry = new LogEntry()
                {
                    LogLevel = logLevel,
                    Message = formatter(state,exception),
                    Date = date
                }
            });
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
