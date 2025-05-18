using BookPlayer.Helpers;
using BookPlayer.Interfaces;
using BookPlayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using TagLib;
using TagLib.Id3v2;

namespace BookPlayer.Services
{
    public class FileHandlingService : IFileHandlingService
    {
        private readonly Dictionary<string, BookMetadata> _metadataCache 
            = new Dictionary<string, BookMetadata>();

        public IList<Book> GetBooks(string rootFolderPath)
        {
            if (rootFolderPath == null || !Directory.Exists(rootFolderPath))
            {
                return null;
            }

            var books = new List<Book>();
            var directoryPaths = Directory.GetDirectories(rootFolderPath);

            foreach (string directoryPath in directoryPaths)
            {
                // get book's metadata
                //BookMetadata bookMetaData = GetBookMainMetadata(directoryPath);
                MusicFileInfo bookMetaData = default;

                try
                {
                    bookMetaData = AnalyzeMusicFile(directoryPath);
                }
                catch { }

                // get coverPath
                string coverPath = Directory.EnumerateFiles(
                    directoryPath, "*.*", SearchOption.AllDirectories)
                        .FirstOrDefault(s => s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        s.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

                // form book name
                string bookName = directoryPath.Split(Path.DirectorySeparatorChar).LastOrDefault();

               

                // form the book object
                Book book = new Book
                {
                    CoverPath = coverPath == null ? default : coverPath,
                    Path = directoryPath,
                    Name = bookMetaData == null? bookName : bookMetaData.Title,
                    TotalTime = bookMetaData == null ? new TimeSpan(0,5,0) : bookMetaData.TotalTime,
                    Narrator = "Narrator",//bookMetaData == null ? "Narrator" : bookMetaData.Narrator,
                    Author = bookMetaData == null ? "Author" : bookMetaData.Author
                };

                books.Add(book);
            }

            return books;
        }


        private MusicFileInfo AnalyzeMusicFile(string folderPath)
        {
            foreach (string filePath in Directory.EnumerateFiles(folderPath, "*.mp3",
                SearchOption.AllDirectories))
            {
                using (var file = TagLib.File.Create(filePath))
                {
                    return new MusicFileInfo
                    {
                        FilePath = filePath,
                        Title = file.Tag.Title ?? Path.GetFileNameWithoutExtension(filePath),
                        TotalTime = file.Properties.Duration,
                        Author = file.Tag.FirstPerformer ?? "Unknown",
                        Narrator = "Narrator"
                    };
                }
            }
            return default;
        }

        

        public BookMetadata GetBookMetadata(string filePath)
        {
            if (_metadataCache.ContainsKey(filePath))
            {
                return _metadataCache[filePath];
            }

            string smilFilePath = filePath.Replace(".mp3", ".smil");

            if (!System.IO.File.Exists(smilFilePath))
            {
                return null;
            }

            var xmlReaderSettings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                XmlResolver = null
            };

            var metadata = new BookMetadata();

            using (var streamReader = new StreamReader(smilFilePath))
            {
                using (var xmlReader = XmlReader.Create(streamReader, xmlReaderSettings))
                {

                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name").Equals("dc:title"))
                            {
                                metadata.Title = xmlReader.GetAttribute("content");
                            }
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name").Equals("title"))
                            {
                                metadata.SubTitle = xmlReader.GetAttribute("content");
                            }
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name").Equals("ncc:timeInThisSmil"))
                            {
                                metadata.Duration = TimeSpan.Parse(xmlReader.GetAttribute("content"));
                            }
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name").Equals("ncc:totalElapsedTime"))
                            {
                                metadata.TotalElapsedTime = TimeSpanParser.Parse(xmlReader.GetAttribute("content"));
                            }
                            if (xmlReader.Name.Equals("body"))
                            {
                                break;
                            }
                        }
                    }
                }
            }


            _metadataCache.Add(filePath, metadata);

            return metadata;
        }

        public BookMetadata GetBookMainMetadata(string directoryPath)
        {
            var nccFilePath = Path.Combine(directoryPath, "ncc.html");
            if (!System.IO.File.Exists(nccFilePath))
            {
                return null;
            }

            var xmlReaderSettings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                XmlResolver = null
            };

            var metadata = new BookMetadata();

            using (var streamReader = new StreamReader(nccFilePath))
            {
                using (var xmlReader = XmlReader.Create(streamReader, xmlReaderSettings))
                {

                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name") != null && xmlReader.GetAttribute("name").Equals("dc:title"))
                            {
                                metadata.Title = xmlReader.GetAttribute("content");
                            }
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name") != null && xmlReader.GetAttribute("name").Equals("title"))
                            {
                                metadata.SubTitle = xmlReader.GetAttribute("content");
                            }
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name") != null && xmlReader.GetAttribute("name").Equals("ncc:narrator"))
                            {
                                metadata.Narrator = xmlReader.GetAttribute("content");
                            }
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name") != null && xmlReader.GetAttribute("name").Equals("dc:creator"))
                            {
                                metadata.Author= xmlReader.GetAttribute("content");
                            }                            
                            if (xmlReader.Name == "meta" && xmlReader.GetAttribute("name") != null && xmlReader.GetAttribute("name").Equals("ncc:totalTime"))
                            {
                                metadata.TotalTime = TimeSpanParser.Parse(xmlReader.GetAttribute("content"));
                            }
                            if (xmlReader.Name.Equals("body"))
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return metadata;
        }



    }//class end

    public class MusicFileInfo
    {
        public string FilePath { get; set; }
        public string Title { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string Author { get; set; }
        public string Narrator { get; set; }
    }
}
