using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatH5Helper
{
    public class LogHelper
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();



        /// <summary>

        /// log the info with debug level

        /// </summary>

        /// <param name="message"></param>

        public static void Debug(string message)
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


        /// <summary>

        /// log the info with debug and error level

        /// </summary>

        /// <param name="message"></param>

        public static void DebugErr(string message)
        {

            Logger.Debug(message);
            Logger.Error(message);

        }

    }
}
