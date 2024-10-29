using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public class HttpRequestClient
    {
        readonly HttpClient _client;
        public HttpRequestClient(int timeout = 5000)
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(timeout);
        }



        public async Task<string> GetStringAsync(string url)
        {
            return await _client.GetStringAsync(url);
            
        }


        public string GetString(string url)
        {
            var result = _client.GetStringAsync(url).Result;
            return result;        
        }



        public string? Post_ReturnString(string url,HttpContent content)
        {
            var success = HttpRequestHelper.Post_ReturnString(_client, url, content, out var rsp);
            if (success)
            {
                return rsp;

            }
            else
            {
                return null;
            }
        }

        public string? PostJson_ReturnString(string url, object content)
        {
            var success = HttpRequestHelper.PostJson_ReturnString(_client, url, content, out var rsp);
            if (success)
            {
                return rsp;

            }
            else
            {
                return null;
            }
        }

        public T? PostJson_ReturnObject<T>(string url,object content) 
        {
            var success = HttpRequestHelper.PostJson_ReturnObject<T>(_client,url,content,out var rsp);
            if(success)
            {
                return rsp;

            }else
            {
                return default;
            }
        }

    }
}
