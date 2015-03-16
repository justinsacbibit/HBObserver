//!CompilerOption:AddRef:Newtonsoft.Json.dll
using Newtonsoft.Json;
using Observer.Commands;
using Observer.Events;
using Observer.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Observer.Channels.Pushbullet
{
    public class PushbulletChannel : Channel
    {
        private string _accessToken;

        public PushbulletChannel(string accessToken)
        {
            _accessToken = accessToken;
        }

        protected async override Task<Exception> SendEventBatch(EventBatch batch)
        {
            WebRequest request = WebRequest.CreateHttp("https://api.pushbullet.com/v2/pushes");
            request.Method = "POST";
            request.Headers["Authorization"] = string.Format("Bearer {0}", _accessToken);
            request.ContentType = "application/json";

            var postData = new 
            {
                type = "note",
                title = batch.Title,
                body = batch.Message
            };

            request.SetBody(postData);

            try
            {
                WebResponse response = await request.GetResponseAsync();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to send message \"{0}\"", e);
            }
        }
    }
}
