using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace PhotoSharingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("StorageAccount");
            string containerName = "photos";

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExists();

            string blobName = "docs-and-friends-selfie-stick";
            string fileName = "docs-and-friends-selfie-stick.png";
            BlobClient blobClient = container.GetBlobClient(blobName);
            blobClient.Upload(fileName, true);

            var blobs = container.GetBlobs();
            foreach (var blob in blobs)
            {
                Console.WriteLine($"{blob.Name} --> Created on: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}");
            }
        }
    }
}
