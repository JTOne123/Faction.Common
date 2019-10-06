using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;

namespace Faction.Common.Utilities
{
    public class StorageClient
    {
        private MinioClient minioClient = new MinioClient("minio", 
            FactionSettings.MINIO_ACCESSKEY, 
            FactionSettings.MINIO_SECRETKEY)
            .WithSSL();

        public async Task CreateBucket(string name)
        {
            try
            {
                // Create bucket if it doesn't exist.
                bool found = await minioClient.BucketExistsAsync(name);
                if (found)
                {
                    Console.WriteLine($"{name} already exists");
                }
                else
                {
                    await minioClient.MakeBucketAsync(name);
                    Console.WriteLine($"{name} created successfully");
                }
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
            }
        }
        
        public List<Bucket> GetBuckets()
        {
            List<Bucket> results = new List<Bucket>();
            var getListBucketsTask = minioClient.ListBucketsAsync();
            foreach (Bucket bucket in getListBucketsTask.Result.Buckets)
            {
                results.Add(bucket);
            }
            return results;
        }

        public List<Item> ListBucketContents(string bucketName, string objectPrefix = null)
        {
            List<Item> results = new List<Item>();
            try
            {
                IObservable<Item> observable = minioClient.ListObjectsAsync(bucketName, objectPrefix, true);
                IDisposable subscription = observable.Subscribe(
                    item => results.Add(item),
                    ex => Console.WriteLine("OnError: {0}", ex.Message),
                    () => Console.WriteLine("OnComplete: {0}"));
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
            }
            return results;
        }
        

        public async Task DownloadBlob(string bucket, string objectName, string destination)
        {
            try
            {
                await minioClient.StatObjectAsync(bucket, objectName);
                await minioClient.GetObjectAsync(bucket, objectName, cb: (stream) =>
                {
                    FileStream fileStream = File.Create(destination);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                });
            }
            catch (MinioException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UploadBlob(string bucket, string path)
        {
            try
            {
                string fileName = Path.GetFileName(path);
                
                // Make a bucket on the server, if not already present.
                bool found = await minioClient.BucketExistsAsync(bucket);
                if (!found)
                {
                    await minioClient.MakeBucketAsync(bucket, "faction");
                }
                // Upload a file to bucket.
                await minioClient.PutObjectAsync(bucket, fileName, path, "application/octet-stream");
                Console.WriteLine("Successfully uploaded " + path );
            }
            catch (MinioException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}