using LiteDB;
using System;
using System.Collections.Generic;

namespace BookPlayer.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string CoverPath { get; set; }
        public string CurrentFile { get; set; }
        public TimeSpan CurrentProgress { get; set; }
        public TimeSpan TotalTime { get; set; }
        public TimeSpan TotalElapsedTime { get; set; }
        public double TotalProgress
        {
            get
            {
                if (TotalTime.TotalSeconds > 0)
                {
                    return (double)TotalElapsedTime.TotalSeconds / TotalTime.TotalSeconds;
                }
                return 0;
            }
        }
        public string Author { get; set; }
        public string Narrator { get; set; }

        [BsonIgnore]
        public IList<string> Files { get; set; }
    }
}
