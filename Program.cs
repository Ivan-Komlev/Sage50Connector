

namespace Connector2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sage50.Sage50 sage = new Sage50.Sage50();
            WebListener.WebListener listener = new WebListener.WebListener();
            listener.startWebServer(sage);

            sage.Connect2Sage50();
        }
    }
}
