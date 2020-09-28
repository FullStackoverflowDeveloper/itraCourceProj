using AppFour.Models.Collection;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AppFour.Globals
{
    public static class CollectionProcessing
    {
        private static Collection collectionResult;
        private static CollectionAction collectionView;

        public static Collection CollectionProcess(CollectionAction collection)
        {
            var uploadResult = collection.Image != null ? UploadFile(collection.Image) : null;
            if (uploadResult != null)
            {
                CloudAccount.DeleteFile(collection.ImageCloudId);
            }
            collectionResult = new Collection
            {
                Id = collection.Id ?? Guid.NewGuid().ToString(),
                UserId = collection.UserId,
                Title = collection.Title,
                Description = collection.Description,
                Topic = collection.Topic,
                ImgUrl = collection.Image != null ? GetImageCloudUrl(uploadResult, collection.Image.FileName) : collection.ImgUrl,
                ImageCloudId = collection.Image != null ? uploadResult.PublicId : collection.ImageCloudId
            };
            return collectionResult;
        }

        public static ImageUploadResult UploadFile(IFormFile image)
        {
            var stream = image.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, stream)
            };
            Cloudinary cloudinary = CloudAccount.Cloud();
            var uploadResult = cloudinary.Upload(uploadParams);
            stream.Close();
            return uploadResult;
        }

        public static string GetImageCloudUrl(ImageUploadResult uploadResult, string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            string url = "https://res.cloudinary.com/desbih9uq/image/upload/" + uploadResult.PublicId + fi.Extension;
            return url;
        }
        public static CollectionAction GetCollectionOnView(Collection collection)
        {
            collectionView = new CollectionAction
            {
                Id = collection.Id,
                UserId = collection.UserId,
                Title = collection.Title,
                Description = collection.Description,
                Topic = collection.Topic,
                ImgUrl = collection.ImgUrl,
                ImageCloudId = collection.ImageCloudId
            };
            return collectionView;
        }
    }
}