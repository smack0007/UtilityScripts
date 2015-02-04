using System;
using System.Net;
using HtmlAgilityPack;

Uri uri = new Uri(Env.ScriptArgs[0]);

WebClient webClient = new WebClient();
string source = webClient.DownloadString(uri);

HtmlDocument document = new HtmlDocument();
document.LoadHtml(source);

if (!Directory.Exists("Output"))
  Directory.CreateDirectory("Output");

var imageUrls = document.DocumentNode
  .Descendants("img")
  .Select(x => x.Attributes["src"])
  .Select(x => x.Value.ToString());

foreach(var imageUrl in imageUrls)
{
  Uri imageUri = new Uri(uri, imageUrl);
  Console.Write(imageUri + " => ");
  try
  {
    webClient.DownloadFile(imageUri, Path.Combine("Output", Path.GetFileName(imageUri.AbsolutePath)));
    Console.WriteLine("Success");
  }
  catch (Exception)
  {
    Console.WriteLine("Error");
  }
}
