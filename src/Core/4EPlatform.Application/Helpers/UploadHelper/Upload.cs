using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace UploadHelper.Helpers
{
    public static class Upload
    {
        public static async Task<string> UploadFileSmartAsync(IFormFile file, IWebHostEnvironment hosting, string folderName = "General")
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("الملف فارغ.");

            if (file.Length > 500 * 1024 * 1024)
                throw new InvalidOperationException("حجم الملف كبير.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".xlsx", ".txt" };
            var allowedMimeTypes = new[]
            {
        "image/jpeg", "image/png", "image/gif",
        "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "text/plain"
    };

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var contentType = file.ContentType;

            if (!allowedExtensions.Contains(extension) || !allowedMimeTypes.Contains(contentType))
                throw new InvalidOperationException("نوع الملف غير مسموح.");

            var uploadPath = Path.Combine(hosting.WebRootPath, "Uploads", folderName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // هذا هو المسار اللي تقدر توصله من المتصفح
            return Path.Combine("Uploads", folderName, fileName).Replace("\\", "/");
        }

        private static string GetRandomName()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            List<char> characters = new List<char>()
                {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B',
                'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'};

            string Name = "";
            Random rand = new Random();
            // run the loop till I get a string of 10 characters
            for (int i = 0; i < 25; i++)
            {
                // Get random numbers, to get either a character or a number...
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    // use a number
                    random = rand.Next(0, numbers.Count);
                    Name += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    Name += characters[random].ToString();
                }
            }
            return Name;
        }
    }
}
