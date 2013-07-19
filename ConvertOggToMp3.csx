using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

foreach (string path in Directory.GetFiles(".", "*.ogg"))
{
	var fileName = Path.GetFileNameWithoutExtension(path);
	Console.WriteLine("{0}.ogg => {0}.mp3", fileName);
	
	var process = Process.Start(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe", string.Format("-I dummy --quiet {0}.ogg --sout=#transcode{{acodec=mp3,channels=2}}:standard{{access=file,mux=raw,dst={0}.mp3}}", fileName));
	
	Thread.Sleep(15000);
	
	process.Kill();
}