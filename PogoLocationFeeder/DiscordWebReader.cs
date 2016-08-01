using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace PogoLocationFeeder.DiscordWebReader
{
    public class DiscordWebReader
    {
        WebClient wc { get; set; }

        public DiscordWebReader()
        {
            InitialiseWebClient();
        }

        // When SSE (Server side event) occurs this fires
        private void OnOpenReadCompleted(object sender, OpenReadCompletedEventArgs args)
        {
            using (var streamReader = new StreamReader(args.Result, Encoding.UTF8))
            {
                var serverPayload = streamReader.ReadLine();
                var jsonPayload = serverPayload.Substring(5);

                Console.WriteLine("Raw message: {0}", serverPayload.ToString());
                var message = JsonConvert.DeserializeObject<Message>(jsonPayload);
                Console.WriteLine("Discord message received: {0}", message.content);

                // TODO: relayMessageToClients(message.content, "Discord");

                InitialiseWebClient();
            }
        }

        private void InitialiseWebClient()
        {
            wc = new WebClient();
            wc.OpenReadAsync(new Uri("http://138.68.22.176/messsages"));
            wc.OpenReadCompleted += OnOpenReadCompleted;
        }
    }

    public class Message
    {
        public string id = "";
        public string channel_id ="";
        public string author = "";
        public string content = "";
        public string timestamp = "";
        public string edited_timestamp = "";
        public string tts = "";
        public string mention_everyone = "";
        public string mentions = "";
        public string mention_roles = "";
        public string attachments = "";
        public string embeds = "";
    }
}