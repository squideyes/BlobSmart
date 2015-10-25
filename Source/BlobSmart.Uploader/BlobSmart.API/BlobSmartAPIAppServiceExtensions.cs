using System;
using System.Net.Http;
using Microsoft.Azure.AppService;

namespace BlobSmart.Uploader
{
    public static class BlobSmartAPIAppServiceExtensions
    {
        public static BlobSmartAPI CreateBlobSmartAPI(this IAppServiceClient client)
        {
            return new BlobSmartAPI(client.CreateHandler());
        }

        public static BlobSmartAPI CreateBlobSmartAPI(this IAppServiceClient client, params DelegatingHandler[] handlers)
        {
            return new BlobSmartAPI(client.CreateHandler(handlers));
        }

        public static BlobSmartAPI CreateBlobSmartAPI(this IAppServiceClient client, Uri uri, params DelegatingHandler[] handlers)
        {
            return new BlobSmartAPI(uri, client.CreateHandler(handlers));
        }

        public static BlobSmartAPI CreateBlobSmartAPI(this IAppServiceClient client, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
        {
            return new BlobSmartAPI(rootHandler, client.CreateHandler(handlers));
        }
    }
}
