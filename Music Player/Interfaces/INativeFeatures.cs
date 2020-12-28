using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Music_Player.Interfaces {
  public interface INativeFeatures {

    string MusicLibaryPath { get; }

    IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);
    IEnumerable<FileInfo> EnumerateFiles2(string path);
    IEnumerable<Java.IO.File> EnumerateFiles3(string path);
    string[] ReadAllLines(string path);
    string[] ReadAllLinesAppFile(string fileName);
    string ReadAppFile(string fileName);
    void RequestPerimissions();
    void WriteAppFile(string fileName, string content);
    void SetStatusBarColor(Color color);
    void SetNavigationBarColor(Color color);
  }
}