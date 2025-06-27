namespace CSMapi.Helpers
{
    public static class FileHelper
    {
        private static readonly string avatarDirectory = @"C:\Users\James\Desktop\CSMS\Avatar";
        private static readonly string esignatureDirectory = @"D:\CISDEVO Repo\e-sign";
        private static readonly string receivingFormDirectory = @"D:\CISDEVO Repo\images";
        
        public static async Task<string> SaveAvatarAsync(IFormFile file, string username)
        {
            return await SaveFileAsync(file, avatarDirectory, username);
        }
       
        public static async Task<string> SaveEsignAsync(IFormFile file, string username)
        {
            return await SaveFileAsync( file, esignatureDirectory, username);
        }
       
        public static async Task<string> SaveReceivingFormAsync(IFormFile file, string username)
        {
            return await SaveFileAsync(file, receivingFormDirectory, username);
        }
      
        private static async Task<string> SaveFileAsync(IFormFile file, string directory, string username)
        {
            
            if (file == null)
                throw new ArgumentException("Invalid File!");
            
            Directory.CreateDirectory(directory);
           
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{username}_{Guid.NewGuid():N}{fileExtension}";
            
            string fullPath = Path.Combine(directory, fileName);
            
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            string relativePath = GetRelativePath(directory, fileName);

            return relativePath; 
        }

        private static string GetRelativePath(string directory, string fileName)
        {
            if (directory.Contains("Avatar"))
                return Path.Combine("/uploads/avatar/", fileName).Replace("\\", "/");
            if (directory.Contains("e-sign"))
                return Path.Combine("/uploads/esignature/", fileName).Replace("\\", "/");
            if (directory.Contains("images"))
                return Path.Combine("/uploads/receivingform/", fileName).Replace("\\", "/");
            return fileName;
        }
    }
}
