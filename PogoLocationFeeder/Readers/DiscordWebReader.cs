using System;
using System.IO;
using System.Net;

namespace PogoLocationFeeder
{
    public class DiscordWebReader
    {
        WebClient wc { get; set; }

        public Stream stream = null;

        public DiscordWebReader()
        {
            InitializeWebClient();
        }

        private void InitializeWebClient()
        {
            var request = WebRequest.Create(new Uri("http://138.68.22.176/messsages"));
            ((HttpWebRequest)request).AllowReadStreamBuffering = false;
            var response = request.GetResponse();

            //TODO: Automatic retry on fail

            stream = response.GetResponseStream();
        }

       
        public class AuthorStruct
        {
            public string username;
            public string id;
            public string discriminator;
            public string avatar;
        }
        public class DiscordMessage
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
}