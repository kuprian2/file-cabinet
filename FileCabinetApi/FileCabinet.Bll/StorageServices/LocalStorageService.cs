using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileCabinet.Bll.StorageServices
{
    public class LocalStorageService : IFileStorageService
    {
        private static readonly int BufferSize = 4096;

        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }

        public string Create(Stream dataStream, string filename)
        {
            var filePath = FormFullPath(filename);

            using (var fileStream = File.OpenWrite(filePath))

            using (var streamReader = new BinaryReader(dataStream))
            {
                byte[] bytes;

                do
                {
                    bytes = streamReader.ReadBytes(BufferSize);
                    fileStream.Write(bytes, 0, bytes.Length);
                } while (bytes.Length >= BufferSize);
            }

            return filePath;
        }

        public async Task<string> CreateAsync(Stream dataStream, string filename)
        {
            var filePath = FormFullPath(filename);

            await Task.Run(() =>
            {
                using (var fileStream = File.OpenWrite(filePath))

                using (var streamReader = new BinaryReader(dataStream))
                {
                    byte[] bytes;

                    do
                    {
                        bytes = streamReader.ReadBytes(BufferSize);
                        fileStream.Write(bytes, 0, bytes.Length);
                    } while (bytes.Length >= BufferSize);
                }
            });

            return filePath;
        }

        public Stream Read(string filepath)
        {
            return new FileStream(filepath, FileMode.Open, FileAccess.Read);
        }

        public FileInfo GetInfo(string filepath)
        {
            return new FileInfo(filepath);
        }

        private string FormFullPath(string filename)
        {
            var normalizedFileName = GetAppropriateFileName(filename);

            return Path.Combine(
                AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                normalizedFileName);
        }

        private string GetAppropriateFileName(string filename)
        {
            var filenameChars =
                filename
                    .Trim()
                    .Replace(' ', '-')
                    .ToLowerInvariant()
                    .ToArray();

            return new string(filenameChars);
        }
    }
}