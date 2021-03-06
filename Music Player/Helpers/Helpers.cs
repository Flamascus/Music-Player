using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Music_Player.Helpers {
  public static class Helpers {

    public static Stream GetStream(string path) {
      var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Helpers)).Assembly;
      return assembly.GetManifestResourceStream("Music_Player." + path);
    }

    /// <summary>
    /// use . instead of / for adressing folders (example: folder/file.txt => folder.file.txt)
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string[] ReadAllLines(string path) {
      var lines = new List<string>();
      string line;

      var stream = GetStream(path);
      using (var reader = new StreamReader(stream)) {
        while ((line = reader.ReadLine()) != null)
          lines.Add(line);
      }

      return lines.ToArray();
    }

    public static string ReadFile(string path) {
      using (var reader = new StreamReader(GetStream(path)))
        return reader.ReadToEnd();
    }

    public static void Write(string path, string content) {
      using (var writer = new StreamWriter(GetStream(path)))
        writer.Write(content);
    }


  }
}
