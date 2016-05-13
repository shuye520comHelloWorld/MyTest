using NLog;

namespace IParty.Services.Common
{
    public class LogHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// log the info with debug level
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Logger.Debug(message);
        }

        /// <summary>
        /// log the info with Warn level
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            Logger.Warn(message);
        }
        /// <summary>
        /// log the info with Error level
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Logger.Error(message);
        }
        /// <summary>
        /// log the info with Fatal level
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            Logger.Fatal(message);
        }
    }
}
