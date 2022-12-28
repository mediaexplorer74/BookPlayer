
namespace BookPlayer.Interfaces
{
    public interface IFileService
    {
        string StorageFolderPath { get; }
        string AppDataPath { get; }
    }
}
