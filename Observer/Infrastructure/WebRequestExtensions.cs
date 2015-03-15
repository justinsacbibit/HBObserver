using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Infrastructure
{
    internal static class WebRequestExtensions
    {
        internal static void SetBody(this WebRequest request, object data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteData, 0, byteData.Length);
                requestStream.Close();
            }
        }
    }
}
