using System.IO;

namespace FileCabinet.Bll.StorageServices
{
    public interface IFileStorageService
    {
        void Delete(string filepath);

        string Create(Stream dataStream, string filename);

        Stream Read(string filepath);

        FileInfo GetInfo(string filepath);
    }
}