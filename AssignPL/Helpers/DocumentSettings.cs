﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AssignPL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            string filePath = Path.Combine(folderPath, fileName);
            using var fileStr = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStr);
            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            if (fileName != null && folderName != null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }
    }
}
