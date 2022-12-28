using BookPlayer.Models;
using System;
using System.Threading.Tasks;

namespace BookPlayer.Interfaces
{
    /// <summary>
    /// Defines functionality related to playing media files
    /// </summary>
    public interface IPlayerService
    {   
        void OpenBook(Book book);
        Task PlayOrPause();
        Task PlayPreviousFile();
        Task PlayNextFile();
        Task JumpBack();
        Task JumpForward();
        Task SeekTo(double targetProgress);
        bool IsPlaying { get; }
        TimeSpan Duration { get; }
        TimeSpan Elapsed { get; }
        double CurrentProgress { get; }
        double TotalProgress { get; }
        string BookCoverPath { get; }
        bool IsBookOpen { get; }
    }
}
