using CloudinaryDotNet;

namespace AppFour.Globals
{
    public static class CloudAccount
    {
        private static readonly string cloudName = "desbih9uq";
        private static readonly string apiKey = "211946961727266";
        private static readonly string apiSecret = "UgJs0hkhPZq9bZ3qdp2Ras3e2JQ";

        private static Cloudinary cloudinary;
        private static Account account;
        public static Cloudinary Cloud()
        {
           account = new Account(
                cloudName,
                apiKey,
                apiSecret);
            cloudinary = new Cloudinary(account);
            return cloudinary;
        }

        public static void DeleteFile(string imgCloudId)
        {
            Cloudinary cloudinary = Cloud();
            cloudinary.DeleteResources(imgCloudId);
        }
    }
}
