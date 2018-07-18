using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace KomaHub
{
    /**
     * 
     * /output/1/on
     * /output/1/off
     * */
    public class WebServer
    {
        private readonly HttpListener listener = new HttpListener();
        private KomaHubForm komaHubForm;

        public WebServer(KomaHubForm komaHubForm)
        {
            this.komaHubForm = komaHubForm;

            listener.Prefixes.Add("http://127.0.0.1:6563/output/");
        }

        public void start()
        {
            listener.Start();
            ThreadPool.QueueUserWorkItem((o) =>
            {
                while (listener.IsListening)
                {
                    try
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            HttpListenerContext ctx = c as HttpListenerContext;
                            if (ctx == null)
                                return;
                            switch (ctx.Request.HttpMethod) {
                                case "POST":
                                    handlePostRequest(ctx);
                                    break;
                                case "GET":
                                    handleGetRequest(ctx);
                                    break;
                            }
                        }, listener.GetContext());
                    } catch { }
                }
            });
        }

        public void stop()
        {
            listener.Stop();
        }

        public void handlePostRequest(HttpListenerContext context)
        {
            Match m = Regex.Match(context.Request.RawUrl, @"^/output/(\d+)/(on|off)$");
            if (m.Success)
            {
                int relay = Convert.ToInt32(m.Groups[1].Value) - 1;
                if (m.Groups[2].Value == "on")
                {
                    this.komaHubForm.enableRelay(relay);
                }
                else
                {
                    this.komaHubForm.disableRelay(relay);
                }

                context.Response.StatusCode = 200;
                context.Response.ContentLength64 = 0;
                context.Response.OutputStream.Close();
                return;
            }

            context.Response.StatusCode = 404;
            context.Response.ContentLength64 = 0;
            context.Response.OutputStream.Close();
        }

        public void handleGetRequest(HttpListenerContext context)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentLength64 = 0;
            context.Response.OutputStream.Close();
        }
    }
}
