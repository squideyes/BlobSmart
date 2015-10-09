using BlobSmart.Common;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace BlobSmart.Services.Controllers
{
    [RoutePrefix("api")]
    public class BlobSasController : ApiController
    {
        [HttpPost, Route("blob/sas")]
        public IHttpActionResult Post(SasRequest request)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var account = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["storageConnString"]);

            var client = account.CreateCloudBlobClient();

            // Assumes for the sake of efficiency that the container exists
            var container = client.GetContainerReference(
                ConfigurationManager.AppSettings["blobContainerName"]);

            var permissions = SharedAccessBlobPermissions.Read;

            if (request.ReadOrWrite == ReadOrWrite.Write)
                permissions |= SharedAccessBlobPermissions.Write;

            var policy = new SharedAccessBlobPolicy()
            {
                Permissions = permissions,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime =
                    DateTime.UtcNow.AddMinutes(request.Minutes)
            };

            var sas = container.GetSharedAccessSignature(policy);

            var results = new List<SasResult>();

            foreach (var guid in request.Guids.Select(g => Guid.Parse(g)))
            {
                var trimmedGuid = guid.ToString("N");

                var uriWithSas = new Uri(string.Format(
                    "{0}/{1}{2}", container.Uri, trimmedGuid, sas));

                results.Add(new SasResult { Guid = guid, UriWithSas = uriWithSas });
            }

            return Ok(results);
        }
    }
}
