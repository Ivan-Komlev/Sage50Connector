/*
 * Sage50 - Customers API Server
 * This application runs as a webserver and provides the data from Sage50 database in JSON format.
 * @author Ivan komlev <ivankomlev@gmail.com>
 * @github https://github.com/Ivan-Komlev/Sage50Connector
 * @copyright Copyright(C) 2020.All Rights Reserved
 * @license GNU/GPL Version 2 or later - http://www.gnu.org/licenses/gpl-2.0.html
*/

using System;
using System.Text;
using System.Threading;

using System.IO;
using System.Security.Cryptography;

using System.Net.Sockets;
using System.Net;

namespace WebListener
{
    public class WebListener
    {
        public configuration config;
        public Sage50.Sage50 sage;

        private Thread _serverThread;
        private HttpListener _listener;

        public WebListener(configuration config_)
        {
            config = config_;
        }
        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

    private void Listen()
    {
        _listener = new HttpListener();
        
        _listener.Prefixes.Add(config.host + ":" + config.port.ToString() + "/");

        _listener.Start();
        while (true)
        {
            try
            {
                HttpListenerContext context = _listener.GetContext();

                Console.WriteLine(context.Request.Url.ToString());
                

                string apikey = context.Request.QueryString["apikey"];

                if (apikey == null)
                {
                        Respond(context, "{\"error\":\"API Key not provides\"}");
                        continue;
                }

                    if (apikey != config.APIKey)
                    {
                        //Implement some black list in the future.
                        Respond(context, "{\"error\":\"Wrong API Key\"}");
                        continue;
                    }

                    string query = context.Request.QueryString["query"];
                    string serverName = context.Request.QueryString["server"];
                    string databaseName = context.Request.QueryString["database"];
                    string companyName = context.Request.QueryString["name"];

                    bool encrypt = context.Request.QueryString["encrypt"] == "1"; //1 encrypt anything else - don't encrypt

                    switch (query)
                {
                        case "status":

                            Respond(context, sage.connectionStatus.ToString(), encrypt);

                            break;

                        case "company":
                            
                            if (serverName == null)
                                Respond(context, "{\"error\":\"Server name not set\"}");
                            else if(databaseName == null)
                                Respond(context, "{\"error\":\"Database name not set\"}");
                            else
                                Respond(context, sage.getCompany(serverName, databaseName), encrypt);

                            break;

                        case "findcompany":

                            if (companyName == null)
                                Respond(context, "{\"error\":\"Company name not set\"}");
                            else
                                Respond(context, sage.findCompany(companyName), encrypt);

                            break;

                        case "companies":

                            Respond(context, sage.getCompanies(), encrypt);

                            break;

                        case "customers":

                            int page = 0;
                            string page_str = context.Request.QueryString["page"];
                            if (page_str != null)
                            {
                                try
                                {
                                    page = int.Parse(page_str);
                                }
                                catch (Exception)
                                {
                                    Respond(context, "{\"error\":\"Page number is not a number\"}");
                                }
                            }

                            Respond(context, sage.getCustomers(serverName, databaseName, page), encrypt);
                            
                            break;

                        case "countcustomers":
                            Respond(context, sage.countCustomers(serverName, databaseName), encrypt);

                            break;

                        default:

                            Respond(context, "{\"error\":\"Unknown query\"}");

                            break;
                }
            }
            catch (Exception ex)
            {
                    Console.WriteLine(ex.ToString());
            }
        }
    }

        private static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        private void Respond(HttpListenerContext context, string response, bool encrypt = false)
        {
            byte[] buffer;

            if (encrypt && config.Password!="")
            {
                string encryptedString = EncryptString(config.Password, response);
                buffer = Encoding.ASCII.GetBytes(encryptedString);
            }
            else
                buffer = Encoding.ASCII.GetBytes(response);


        context.Response.ContentType = "application/json";
        context.Response.ContentLength64 = buffer.Length;
        context.Response.AddHeader("Date", DateTime.Now.ToString("r"));


        context.Response.OutputStream.Write(buffer, 0, buffer.Length);

        context.Response.StatusCode = (int)HttpStatusCode.OK;
        context.Response.OutputStream.Flush();
    }

    private void Initialize()
    {
        _serverThread = new Thread(this.Listen);
        _serverThread.Start();
    }

    public void startWebServer(Sage50.Sage50 sage_)
    {
            sage = sage_;
            
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize();
        }
}
}
