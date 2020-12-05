/*
 * Sage50 - Customers API Server
 * This application runs as a webserver and provides the data from Sage50 database in JSON format.
 * @author Ivan komlev <ivankomlev@gmail.com>
 * @github https://github.com/Ivan-Komlev/Sage50Connector
 * @copyright Copyright(C) 2020.All Rights Reserved
 * @license GNU/GPL Version 2 or later - http://www.gnu.org/licenses/gpl-2.0.html
*/

using System;
using System.IO;

namespace Connector2
{
        internal class Program
        {
            static void Main(string[] args)
            {
                configuration config = new configuration("config.ini");

                Sage50.Sage50 sage = new Sage50.Sage50(config);
                WebListener.WebListener listener = new WebListener.WebListener(config);
                listener.startWebServer(sage);

                sage.Connect2Sage50();
            }
        }
}
