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
     * /sensors
     * */
    public class WebServer
    {
        private readonly HttpListener listener = new HttpListener();
        private KomaHubForm komaHubForm;

        public WebServer(KomaHubForm komaHubForm)
        {
            this.komaHubForm = komaHubForm;

            listener.Prefixes.Add("http://127.0.0.1:6563/output/");
            listener.Prefixes.Add("http://127.0.0.1:6563/");
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
            if (context.Request.RawUrl == "/sensors")
            {
                reportSensors(context);
            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.ContentLength64 = 0;
                context.Response.OutputStream.Close();
            }
        }

        private void reportSensors(HttpListenerContext context)
        {
            /*
             * {
             *   "pth" : { "temperature": 3.5, "humidity": 70.5, "pressure": 1001.5, "dewpoint": -3.2 },
             *   "skyquality" : { "magnitude": 21.3, "frequency": 1005 },
             *   "skytemperature" : { "sky": -15.3, "ambient": 0.2 },
             *   "power" : { "inputvoltage": 12.5, "outputcurrent": [ 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 ] },
             *   "temperatures": [ 10.5, 0, 0, 0 ]
             * }
             */

            KomahubStatus status = komaHubForm.getUIState().Status;

            Dictionary<string, object> data = new Dictionary<string, object>();
            if (status.pthPresent)
            {
                Dictionary<string, float> pthdata = new Dictionary<string, float>();
                pthdata.Add("temperature", status.temperature);
                pthdata.Add("humidity", status.humidity);
                pthdata.Add("pressure", status.pressure);
                pthdata.Add("dewpoint", status.dewpoint);
                data.Add("pth", pthdata);
            }
            if (status.skyQualityPresent)
            {
                Dictionary<string, float> skyqualitydata = new Dictionary<string, float>();
                skyqualitydata.Add("magnitude", status.skyQuality);
                skyqualitydata.Add("frequency", status.skyQualityFreq);
                data.Add("skyquality", skyqualitydata);
            }
            if (status.skyTemperaturePresent)
            {
                Dictionary<string, float> skydata = new Dictionary<string, float>();
                skydata.Add("sky", status.skyTemperature);
                skydata.Add("ambient", status.skyTemperatureAmbient);
                data.Add("skytemperature", skydata);
            }
            if (status.numberOfExternalTemperatures > 0)
            {
                Dictionary<string, float[]> temperaturedata = new Dictionary<string, float[]>();
                float[] temps = new float[status.numberOfExternalTemperatures];
                for (int i = 0; i < status.numberOfExternalTemperatures; i++)
                    temps[i] = status.externalTemperatures[i];
                data.Add("temperatures", temps);
            }

            Dictionary<string, object> powerdata = new Dictionary<string, object>();
            powerdata.Add("inputvoltage", status.inputVoltage);
            float[] outputcurrents = new float[6];
            for (int i = 0; i < 6; i++)
                outputcurrents[i] = status.outputCurrent[i];
            powerdata.Add("outputcurrents", outputcurrents);
            data.Add("power", powerdata);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            context.Response.ContentLength64 = bytes.Length;
            context.Response.Headers.Add("Content-Type: application/json");
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.StatusCode = 200;
            context.Response.OutputStream.Close();
        }
    }
}
