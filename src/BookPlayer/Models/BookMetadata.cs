using System;

namespace BookPlayer.Models
{
    public class BookMetadata
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Narrator { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string SubTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan TotalElapsedTime { get; set; }
    }
}
