using AmazingBank.Infrastructure.AzureStorage;
using SistemaFotografa.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SistemaFotografa.DomainService.Services
{
    public class FileServerService : IFileServerService
    {
        private readonly AzureBlobRepository _blob;

        public FileServerService()
        {
            _blob = new AzureBlobRepository();
        }

        public void DeleteFile(String blobContainerName, String fileName)
        {
            _blob.DeleteFile(blobContainerName, fileName);
        }
        public string UploadFile(string appendFileName, Stream fileContent, string blobContainerName, string contentType, string Directory)
        {
            return _blob.UploadFile(appendFileName, fileContent, blobContainerName, contentType, Directory);
        }
    }
}
