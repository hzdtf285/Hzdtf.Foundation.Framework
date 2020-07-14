using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// HTTP辅助类
    /// @ 黄振东
    /// </summary>
    public static class HttpUtil
    {
        /// <summary>
        /// 获取token回调
        /// </summary>
        public static Func<string> GetTokenFunc;

        #region Get

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>返回字符串</returns>
        public static string Get(string url)
        {
            using (var httpClient = CreateHttpClient())
            {
                return httpClient.Get(url);
            }
        }

        /// <summary>
        /// Get请求JSON
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <returns>返回字符串</returns>
        public static string Get(this HttpClient httpClient, string url)
        {
            return httpClient.RequestJson("Get", httpContent =>
            {
                return httpClient.GetAsync(url);
            });
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>任务</returns>
        public static Task<string> GetAsync(string url)
        {
            return Task<string>.Run(() => Get(url));
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <returns>任务</returns>
        public static Task<string> GetAsync(this HttpClient httpClient, string url)
        {
            return Task<string>.Run(() => httpClient.Get(url));
        }

        #endregion

        #region Post

        /// <summary>
        /// Post请求JSON
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>返回字符串</returns>
        public static string PostJson(string url, object data = null)
        {
            using (var httpClient = CreateHttpClient())
            {
                return httpClient.PostJson(url, data);
            }
        }

        /// <summary>
        /// Post请求JSON
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>返回字符串</returns>
        public static string PostJson(this HttpClient httpClient, string url, object data = null)
        {
            return httpClient.RequestJson("Post", httpContent =>
            {
                return httpClient.PostAsync(url, httpContent);
            });
        }

        /// <summary>
        /// Post请求JSON
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>任务</returns>
        public static Task<string> PostJsonAsync(string url, object data = null)
        {
            return Task<string>.Run(() => PostJson(url, data));
        }

        /// <summary>
        /// Post请求JSON
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>任务</returns>
        public static Task<string> PostJsonAsync(this HttpClient httpClient, string url, object data = null)
        {
            return Task<string>.Run(() => httpClient.PostJson(url, data));
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>返回字符串</returns>
        public static string Delete(string url)
        {
            using (var httpClient = CreateHttpClient())
            {
                return httpClient.Delete(url);
            }
        }

        /// <summary>
        /// Get请求JSON
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <returns>返回字符串</returns>
        public static string Delete(this HttpClient httpClient, string url)
        {
            return httpClient.RequestJson("Delete", httpContent =>
            {
                return httpClient.DeleteAsync(url);
            });
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>任务</returns>
        public static Task<string> DeleteAsync(string url)
        {
            return Task<string>.Run(() => Delete(url));
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <returns>任务</returns>
        public static Task<string> DeleteAsync(this HttpClient httpClient, string url)
        {
            return Task<string>.Run(() => httpClient.Delete(url));
        }

        #endregion

        #region Put

        /// <summary>
        /// Put请求JSON
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>返回字符串</returns>
        public static string PutJson(string url, object data = null)
        {
            using (var httpClient = CreateHttpClient())
            {
                return httpClient.PutJson(url, data);
            }
        }

        /// <summary>
        /// Put请求JSON
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>返回字符串</returns>
        public static string PutJson(this HttpClient httpClient, string url, object data = null)
        {
            return httpClient.RequestJson("Put", httpContent =>
            {
                return httpClient.PutAsync(url, httpContent);
            });
        }

        /// <summary>
        /// Put请求JSON
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>任务</returns>
        public static Task<string> PutJsonAsync(string url, object data = null)
        {
            return Task<string>.Run(() => PutJson(url, data));
        }

        /// <summary>
        /// Put请求JSON
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>任务</returns>
        public static Task<string> PutJsonAsync(this HttpClient httpClient, string url, object data = null)
        {
            return Task<string>.Run(() => httpClient.PutJson(url, data));
        }

        #endregion

        /// <summary>
        /// 添加token到头里
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="token">token</param>
        public static void AddBearerTokenToHeader(this HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        /// <summary>
        /// 创建http客户端
        /// </summary>
        /// <returns>http客户端</returns>
        public static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            if (GetTokenFunc != null)
            {
                httpClient.AddBearerTokenToHeader(GetTokenFunc());
            }

            return httpClient;
        }

        /// <summary>
        /// 请求请求JSON
        /// </summary>
        /// <param name="httpClient">http客户端</param>
        /// <param name="method">方法</param>
        /// <param name="callbackRequest">回调请求</param>
        /// <param name="data">数据</param>        
        /// <returns>返回字符串</returns>
        public static string RequestJson(this HttpClient httpClient, string method, Func<HttpContent, Task<HttpResponseMessage>> callbackRequest, object data = null)
        {
            string dataJson = data == null ? null : JsonUtil.SerializeIgnoreNull(data);
            HttpContent content = new StringContent(dataJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Task<HttpResponseMessage> task = null;
            httpClient.DefaultRequestHeaders.Add("Method", method);
            task = callbackRequest(content);
            task.Wait();

            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var readTask = task.Result.Content.ReadAsStringAsync();
                readTask.Wait();

                return readTask.Result;
            }
            else
            {
                throw new Exception(task.Result.StatusCode.ToString());
            }
        }
    }
}
