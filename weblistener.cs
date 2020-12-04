using System;
using System.Text;
using System.Threading;

using System.Net.Sockets;
using System.Net;

namespace WebListener
{
    public class WebListener
    {
        public Sage50.Sage50 sage;

        private Thread _serverThread;
        private HttpListener _listener;
        private int _port;

    public int Port
    {
        get { return _port; }
        private set { }
    }

    public void Stop()
    {
        _serverThread.Abort();
        _listener.Stop();
    }

    private void Listen()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add("http://localhost:8080/");
        //_listener.Prefixes.Add("http://localhost:" + _port.ToString() + "/");
        //_listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
        _listener.Start();
        while (true)
        {
            try
            {
                HttpListenerContext context = _listener.GetContext();

                Console.WriteLine(context.Request.Url.ToString());

                string query = context.Request.QueryString["query"];
                string serverName = context.Request.QueryString["server"];
                string databaseName = context.Request.QueryString["database"];
                string companyName = context.Request.QueryString["name"];

                switch (query)
                {
                        case "status":

                            Respond(context, sage.connectionStatus.ToString());

                            break;

                        case "company":
                            
                            if (serverName == null)
                                Respond(context, "{\"error\":\"Server name not set\"}");
                            else if(databaseName == null)
                                Respond(context, "{\"error\":\"Database name not set\"}");
                            else
                                Respond(context, sage.getCompany(serverName, databaseName));

                            break;

                        case "findcompany":

                            if (companyName == null)
                                Respond(context, "{\"error\":\"Company name not set\"}");
                            else
                                Respond(context, sage.findCompany(companyName));

                            break;

                        case "companies":

                            Respond(context, sage.getCompanies());

                            break;

                        case "customers":

                            Respond(context, sage.getCustomers(serverName, databaseName));

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

    private void Respond(HttpListenerContext context, string response)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(response);


        context.Response.ContentType = "application/json";
        context.Response.ContentLength64 = buffer.Length;
        context.Response.AddHeader("Date", DateTime.Now.ToString("r"));


        context.Response.OutputStream.Write(buffer, 0, buffer.Length);

        context.Response.StatusCode = (int)HttpStatusCode.OK;
        context.Response.OutputStream.Flush();
    }

    private void Initialize(int port)
    {
        this._port = port;
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
        this.Initialize(80);
    }
}
}
