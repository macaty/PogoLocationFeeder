using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PogoLocationFeeder.DiscordWebReader
{
    public class DiscordWebReader
    {
        WebClient wc { get; set; }

        public DiscordWebReader()
        {
            InitializeWebClient();
        }

        private void InitializeWebClient()
        {
            var request = WebRequest.Create(new Uri("http://138.68.22.176/messsages"));
            ((HttpWebRequest)request).AllowReadStreamBuffering = false;
            var response = request.GetResponse();
            var stream = response.GetResponseStream();

            ReadStreamForever(stream);
        }
        public static void ReadStreamForever(Stream stream)
        {
            var encoder = new UTF8Encoding();
            var buffer = new byte[2048];
            while (true)
            {
                //TODO: Better evented handling of the response stream

                if (stream.CanRead)
                {
                    int len = stream.Read(buffer, 0, 2048);
                    if (len > 0)
                    {
                        var serverPayload = encoder.GetString(buffer, 0, len);
                        //Console.WriteLine("text={0}", serverPayload);

                        try
                        {
                            var message = serverPayload.Split(new[] { '\r', '\n' })[2];
                            if (message.Length == 0) continue;

                            var jsonPayload = message.Substring(5);
                            //Console.WriteLine($"JSON: {jsonPayload}");

                            var result = JsonConvert.DeserializeObject<Message>(jsonPayload);
                            if (result != null)
                            {
                                Console.WriteLine("Discord message received: {0}", result.content);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception: {e.ToString()}\n\n\n");
                        }
                        

                        // TODO: relayMessageToClients(message.content, "Discord");
                        //Application.Push(text);
                    }
                }
                //System.Threading.Thread.Sleep(250);
            }
        }
    }
    public class AuthorStruct
    {
        public string username;
        public string id;
        public string discriminator;
        public string avatar;
    }
    public class Message
    {
        public string id = "";
        public string channel_id = "";
        //public List<AuthorStruct> author;
        public string content = "";
        public string timestamp = "";
        //public string edited_timestamp = null;
        public bool tts = false;
        //public bool mention_everyone = false;
        //public bool pinned = false;
        //public bool deleted = false;
        //public string nonce = "";
        //public string mentions = "";
        //public string mention_roles = "";
        //public xxx attachments = "";
        //public xxx embeds = "";
    }
}