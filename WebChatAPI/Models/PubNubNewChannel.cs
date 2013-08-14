using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PubNubMessaging.Core;

namespace WebChatAPI.Models
{
    public static class PubNubNewChannel
    {
        private static Random random = new Random((int)DateTime.Now.Ticks); //thanks to McAden

        private static string RandomString()
        {
            var size = 20;
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string CreatenewChannel()
        {
            Pubnub pubnub = new Pubnub(
                    "pub-c-4331b990-8629-4f47-9669-51f0e2ee9c9d",               // PUBLISH_KEY
                    "sub-c-bfd2fbba-0428-11e3-91de-02ee2ddab7fe",               // SUBSCRIBE_KEY
                    "sec-c-NDExYTBlYjUtM2QyYS00YTJiLWExNDItM2Y5NDQ2ZjA1N2Uy",   // SECRET_KEY
                    "",                                                         // CIPHER_KEY
                    true                                                        // SSL_ON?
                );
            string channel = RandomString();

            // Publish a sample message to Pubnub
            pubnub.Publish<string>(channel, "", DisplayReturnMessage);

            return channel;
        }

        /// <summary>
        /// Callback method captures the response in JSON string format for all operations
        /// </summary>
        /// <param name="result"></param>
        static void DisplayReturnMessage(string result)
        {
            //Console.WriteLine("");
        }
    }
}