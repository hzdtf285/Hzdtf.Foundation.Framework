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

        /// <summary>
        /// POST请求JSON
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>返回字符串</returns>
        public static string PostJson(string url, object data = null)
        {
            string dataJson = data == null ? null : JsonUtil.SerializeIgnoreNull(data);
            HttpContent content = new StringContent(dataJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Task<HttpResponseMessage> task = null;
            using (var httpClient = CreateHttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Method", "Post");
                task = httpClient.PostAsync(url, content);
                task.Wait();
            }

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

        /// <summary>
        /// POST请求JSON
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="data">数据</param>
        /// <returns>任务</returns>
        public static Task<string> PostJsonAsync(string url, object data = null)
        {
            return Task<string>.Run(() => PostJson(url, data));
        }

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
    }
}
