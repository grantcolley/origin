//-----------------------------------------------------------------------
// <copyright file="LoggerFacade.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using log4net;
using Microsoft.Practices.Prism.Logging;

namespace DevelopmentInProgress.Origin.LoggerFacade
{
    /// <summary>
    /// The logger facade implementing a log4net logger.
    /// </summary>
    public class LoggerFacade : ILoggerFacade
    {
        /// <summary>
        /// Instance if the log4net logger.
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the LoggerFacade class that implements the log4net logger.
        /// </summary>
        public LoggerFacade()
        {
            // Log4net requires a call to configure the logger from 
            // the App.config file prior to calling GetLogger().
            log4net.Config.XmlConfigurator.Configure();
            logger = LogManager.GetLogger(typeof(LoggerFacade));
            logger.Info("*********************************************");
            logger.Info("*********************************************");
            logger.Info("Origin");
            logger.Info("Copyright © Development In Progress Ltd 2012");
            logger.Info("Start Application");
        }

        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="category">The message category.</param>
        /// <param name="priority">The log priority.</param>
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    logger.Debug(message);
                    break;
                case Category.Warn:
                    logger.Warn(message);
                    break;
                case Category.Exception:
                    logger.Error(message);
                    break;
                case Category.Info:
                    logger.Info(message);
                    break;
            }
        }
    }
}