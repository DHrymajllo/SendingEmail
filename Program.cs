#pragma warning disable IDE0005
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace ElasticEmailClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            NameValueCollection values = new NameValueCollection();
            values.Add("apikey", "Your API Key");
            values.Add("from", "Your Email");
            values.Add("fromName", "Your Company Name");
            //values.Add("to", "xyz@xyz.com"); // If u want send to one person
            values.Add("subject", "Your Subject");
            values.Add("bodyText", "Hello, {firstname}");
            values.Add("bodyHtml", "<h1>Hello, {firstname} {lastname}</h1>");
            values.Add("mergesourcefilename", "mycontacts.csv"); // From csv file

            var filepath = "C:\\Users\\RyderX\\Desktop\\mycontacts.csv";
            var file = File.OpenRead(filepath);

            // multiple files can be sent using this method
            var filesStream = new Stream[] { file };
            var filenames = new string[] { "mycontacts.csv" };
            var URL = "https://api.elasticemail.com/v2/email/send";

            string result = Upload(URL, values, filesStream, filenames);

            Console.WriteLine(result);
        }

        public static string Upload(string actionUrl, NameValueCollection values, Stream[] paramFileStream = null, string[] filenames = null)
        {   
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                foreach (string key in values)
                {
                    HttpContent stringContent = new StringContent(values[key]);
                    formData.Add(stringContent, key);
                }

                for (int i = 0; i < paramFileStream.Length; i++)
                {
                    HttpContent fileStreamContent = new StreamContent(paramFileStream[i]);
                    formData.Add(fileStreamContent, "file" + i, filenames[i]);
                }

                var response = client.PostAsync(actionUrl, formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

                return response.Content.ReadAsStringAsync().Result;
            }
        }

    }
}
