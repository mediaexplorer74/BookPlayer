
namespace BookPlayer.Interfaces
{
    // 2 Interface declaration
    public interface IFileService
    {
        public string StorageFolderPath { get; }
        public string AppDataPath { get; }
    }
}
