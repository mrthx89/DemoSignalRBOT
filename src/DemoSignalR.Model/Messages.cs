using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DemoSignalR.Model
{
    public class Messages
    {
        [JsonProperty("Server")]
        public Server Server { get; set; }

        [JsonProperty("ServerId")]
        public String ServerId
        {
            get
            {
                if (Server is null)
                {
                    return "";
                } else
                {
                    return Server.ServerId;
                }
            }
        }

        [JsonProperty("Client")]
        public Client Client { get; set; }

        [JsonProperty("ClientId")]
        public String ClientId
        {
            get
            {
                if (Client is null)
                {
                    return "";
                }
                else
                {
                    return Client.ClientId;
                }
            }
        }

        [JsonProperty("NickName")]
        public string NickName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("File")]
        public string File { get; set; }
    }


    public class Server
    {
        [JsonProperty("ServerId")]
        public string ServerId { get; set; }

        [JsonProperty("NickName")]
        public string NickName { get; set; }
    }

    public class Client
    {
        [JsonProperty("ClientId")]
        public string ClientId { get; set; }

        [JsonProperty("NickName")]
        public string NickName { get; set; }
    }
}
