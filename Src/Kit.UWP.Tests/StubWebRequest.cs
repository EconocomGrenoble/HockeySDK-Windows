﻿namespace Microsoft.HockeyApp.TestFramework
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
#if WINRT || NET45 || NET46 || WINDOWS_UWP
    using TaskEx = System.Threading.Tasks.Task;
#endif

    internal class StubWebRequest : WebRequest
    {
        public Func<string> OnGetContentType;
        public Action<string> OnSetContentType;
        public Func<WebHeaderCollection> OnGetHeaders;
        public Action<WebHeaderCollection> OnSetHeaders;
        public Func<string> OnGetMethod;
        public Action<string> OnSetMethod;
        public Func<Uri> OnGetRequestUri;

        public Action OnAbort;
        public Func<AsyncCallback, object, IAsyncResult> OnBeginGetRequestStream;
        public Func<IAsyncResult, Stream> OnEndGetRequestStream;
        public Func<AsyncCallback, object, IAsyncResult> OnBeginGetResponse;
        public Func<IAsyncResult, WebResponse> OnEndGetResponse;

#if WINRT || WINDOWS_UWP
        public Func<Task<Stream>> OnGetRequestStreamAsync;
        public Func<Task<WebResponse>> OnGetResponseAsync;
#endif

        private string contentType;
        private WebHeaderCollection headers;
        private string method;
        private StubStream requestStream;
        private WebResponse response;

        public StubWebRequest()
        {
            this.OnGetContentType = () => this.contentType;
            this.OnSetContentType = value => this.contentType = value;
            this.OnGetHeaders = () => this.headers = new WebHeaderCollection();
            this.OnSetHeaders = value => this.headers = value;
            this.OnGetMethod = () => this.method;
            this.OnSetMethod = value => this.method = value;
            this.OnGetRequestUri = () => default(Uri);

            this.OnAbort = () => { };
            this.OnBeginGetRequestStream = (callback, state) => TaskEx.FromResult<object>(null).AsAsyncResult(callback, this);
            this.OnEndGetRequestStream = asyncResult => this.requestStream = new StubStream();
            this.OnBeginGetResponse = (callback, state) => TaskEx.FromResult<object>(null).AsAsyncResult(callback, this);
            this.OnEndGetResponse = asyncResult => this.response = new StubWebResponse();
#if WINRT || WINDOWS_UWP
            this.OnGetRequestStreamAsync = () => Task.Factory.FromAsync(this.OnBeginGetRequestStream, this.OnEndGetRequestStream, null);
            this.OnGetResponseAsync = () => Task.Factory.FromAsync(this.OnBeginGetResponse, this.OnEndGetResponse, null);
#endif
        }

        public override string ContentType 
        {
            get { return this.OnGetContentType(); }
            set { this.OnSetContentType(value); }
        }

        public override WebHeaderCollection Headers 
        {
            get { return this.OnGetHeaders(); }
            set { this.OnSetHeaders(value); }
        }

        public override string Method 
        {
            get { return this.OnGetMethod(); }
            set { this.OnSetMethod(value); }
        }

        public override Uri RequestUri 
        {
            get { return this.OnGetRequestUri(); }
        }

        public override void Abort()
        {
            this.OnAbort();
        }

#if WINRT || WINDOWS_UWP
        public override Task<Stream> GetRequestStreamAsync()
        {
            return this.OnGetRequestStreamAsync();
        }

        public override Task<WebResponse> GetResponseAsync()
        {
            return this.OnGetResponseAsync();
        }
#endif

        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            return this.OnBeginGetRequestStream(callback, state);
        }

        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            return this.OnEndGetRequestStream(asyncResult);
        }

        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            return this.OnBeginGetResponse(callback, state);
        }

        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            return this.OnEndGetResponse(asyncResult);
        }
    }
}
