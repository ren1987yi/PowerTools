using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Network
{
    public static class HttpRequestHelper
    {
        public static HttpRequestClient CreateClient(int timeout = 5000)
        {
            return new HttpRequestClient(timeout);
        }

        public static bool GetString(string url, out string responseText)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var response = client.GetAsync(url).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return true;
                    }
                    else
                    {
                        responseText = response.StatusCode.ToString();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                responseText = ex.Message;
                return false;
            }
        }

        public static bool GetJson<T>(string url, out T? responseData, out string error)
        {
            if (GetString(url, out var txt))
            {
                //content = new JsonSerializer.Deserialize<T>(txt);
                try
                {
                    error = string.Empty;
                    var result = JsonSerializer.Deserialize<T>(txt);
                    if (result != null)
                    {
                        responseData = result;
                    }
                    else
                    {
                        responseData = default;
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    responseData = default;
                    return false;
                }
            }
            else
            {
                error = txt;
                responseData = default;
                return false;
            }
        }

        public static HttpContent BuildJson(object data)
        {
            StringContent jsonContent = new(
               JsonSerializer.Serialize(data),
               Encoding.UTF8,
               "application/json");

            return jsonContent;
        }

        public static bool Post_ReturnString(HttpClient client, string url, HttpContent content, out string responseText)
        {
            responseText = string.Empty;

            HttpResponseMessage response = client.PostAsync(
                url,
                content).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                responseText = jsonResponse;
                return true;
            }

            return false;
        }

        public static bool Post_ReturnString(string url, HttpContent content, out string responseText)
        {
            responseText = string.Empty;

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync(
                    url,
                    content).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = response.Content.ReadAsStringAsync().Result;
                    responseText = jsonResponse;
                    return true;
                }
            }

            return false;
        }

        public static bool PostJson_ReturnString(HttpClient client, string url, object content, out string responseText)
        {
            responseText = string.Empty;

            HttpResponseMessage response = client.PostAsJsonAsync(
                url,
                content).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                responseText = jsonResponse;
                return true;
            }

            return false;
        }

        public static bool PostJson_ReturnString(string url, object content, out string responseText)
        {
            responseText = string.Empty;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync(
                    url,
                    content).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = response.Content.ReadAsStringAsync().Result;
                    responseText = jsonResponse;
                    return true;
                }
            }

            return false;
        }

        public static bool PostJson_ReturnObject<T>(HttpClient client, string url, object content, out T? responseData)
        {
            responseData = default;

            HttpResponseMessage response = client.PostAsJsonAsync(
                url,
                content).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonResponse = response.Content.ReadFromJsonAsync<T>().Result;
                responseData = jsonResponse;
                return true;
            }

            return false;
        }

        public static bool PostJson_ReturnObject<T>(string url, object content, out T? responseData)
        {
            responseData = default;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync(
                    url,
                    content).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = response.Content.ReadFromJsonAsync<T>().Result;
                    responseData = jsonResponse;
                    return true;
                }
            }

            return false;
        }
    }
}