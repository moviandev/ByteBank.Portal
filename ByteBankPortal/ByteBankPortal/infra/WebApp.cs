using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.infra
{
    public class WebApp
    {
        private readonly string[] _prefix;

        public WebApp(string[] prefix)
        {
            if (prefix == null)
                throw new ArgumentNullException(nameof(prefix));

            _prefix = prefix;
        }

        public void Start()
        {
            var httpListener = new HttpListener();

            foreach (var item in _prefix)
                httpListener.Prefixes.Add(item);

            httpListener.Start();
            var context = httpListener.GetContext();
            var request = context.Request;
            var response = context.Response;

            var path = request.Url.AbsolutePath;
            var assembly = Assembly.GetExecutingAssembly();
            if (path == "/Assets/css/styles.css")
            {
                // return CSS file
                var resourceName = "ByteBank.Portal.Assets.css.styles.css";
                var resourceStream = assembly.GetManifestResourceStream(resourceName);
                var resourceBytes = new byte[resourceStream.Length];

                resourceStream.Read(resourceBytes, 0, (int)resourceStream.Length);

                response.ContentType = "text/css; charset=utf-8";
                response.StatusCode = 200;
                response.ContentLength64 = resourceStream.Length;

                response.OutputStream.Write(resourceBytes, 0, (int)resourceBytes.Length);

                response.OutputStream.Close();
                httpListener.Stop();
            }
            else if (path == "/Assets/js/main.css")
            {
                // return JS file
                var resourceName = "ByteBank.Portal.Assets.js.main.js";
                var resourceStream = assembly.GetManifestResourceStream(resourceName);
                var resourceBytes = new byte[resourceStream.Length];

                resourceStream.Read(resourceBytes, 0, (int)resourceStream.Length);

                response.ContentType = "application/js; charset=utf-8";
                response.StatusCode = 200;
                response.ContentLength64 = resourceStream.Length;

                response.OutputStream.Write(resourceBytes, 0, (int)resourceBytes.Length);

                response.OutputStream.Close();
                httpListener.Stop();
            }
        }
    }
}