# BookPlayer

My fork of BookPlayer project for micro-RnD (XamarinMediaManager Windows compatibility questions, etc.)

## About (words of the author of original)
Quickly made audio book player mobile app for Android. It uses [BaseFlow/XamarinMediaManager](https://github.com/Baseflow/XamarinMediaManager) under the hood and has simple UI for the playing files and showing library content. 

I have no plans to develop this further, but maybe this can be a useful draft project for someone to see how XamarinMediaManager can be easily used.

## Features
- Made with .NET (Visual Studio, Xamarin.Forms)
- Android app is functional. Also iOS project draft exists
- Two main views: player and "bookshelf". In addition there's simple settings page to specify book library folder path
- Light/dark theme
- Lock screen and quick access media playing controls provided by XamarinMediaManager
- Currently supports and parses only books with its metadata in [SMIL](https://en.wikipedia.org/wiki/Synchronized_Multimedia_Integration_Language) format. All files (.html, .mp3 and .smil) related to a specific book needs to be in one folder.

## Screenshots
![](Images/Player.png)
![](Images/Bookshelf.png)

## My 2 cents
- UWP "workload" added only (no solution tuning)
- "Common library" switched from standard 2.1 to 2.0.
- Xamarin Forms downgraded from 5.0 to 4.8.


## License
Licensed under the MIT license.

## References
- https://github.com/ilpork/BookPlayer oroginal BookPlayer project ( for Android, and iOS ? ) 
- https://github.com/ilpork ilpork, cool c# developer

## ..
As is. No support. RnD only.

## .
[M][E] 2025
