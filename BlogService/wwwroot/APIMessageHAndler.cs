﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlogService
{
    public class APIMessageHandler: DelegatingHandler
    {
        public string Key { get; set; }
        public APIMessageHandler()
        {
            this.Key = "abc";
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
        private bool ValidateKey(HttpRequestMessage message)
        {
            //var query = message.RequestUri.ParseQueryString();
            //string key = query["key"];
            //return (key == Key);
            return true;
        }
    }
}
