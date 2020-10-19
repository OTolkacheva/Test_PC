using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Test_PC.Service
{
    public class ApiService : IDisposable
    {
        private HttpClient Client { get; }
        public ApiService()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Result> Send<Result>(string requestUri)
        {
            List<Result> result = default;
            try
            {
                HttpResponseMessage response;
                response = Client.GetAsync(requestUri).Result;
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var strRes = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<List<Result>>(strRes);
                }
                response.EnsureSuccessStatusCode();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Client.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}