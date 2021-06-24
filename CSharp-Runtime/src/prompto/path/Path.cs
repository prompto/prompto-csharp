using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace prompto.path
{
    public abstract class Path
    {
		public static List<String> listRoots()
		{
			return new List<String>(DriveInfo.GetDrives().Where(drive => drive.IsReady).Select(drive => drive.Name));
		}

		public static List<String> listChildren(String path)
        {
			return new List<String>(Directory.GetFiles(path));
		}

		public static bool pathExists(String path)
        {
			return File.Exists(path) || Directory.Exists(path);
        }

		public static bool pathIsFile(String path)
		{
			return File.Exists(path);
		}

		public static bool pathIsDirectory(String path)
		{
			return Directory.Exists(path);
		}

		public static bool pathIsLink(String path)
		{
			FileInfo info = new FileInfo(path);
			return info.Exists && info.Attributes.HasFlag(FileAttributes.ReparsePoint);
		}

		public static String compressToTempPath(String path)
        {
			FileInfo rawFile = new FileInfo(path);
			using (FileStream rawInput = rawFile.OpenRead())
            {
				String compressedPath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".gz");
				FileInfo compressedFile = new FileInfo(compressedPath);
				using (FileStream compressedOutput = compressedFile.OpenWrite())
                {
					using (GZipStream deflatingOutput = new GZipStream(compressedOutput, CompressionMode.Compress))
                    {
						rawInput.CopyTo(deflatingOutput);
						return compressedPath;
					}
                }
			}
		}

		public static String decompressToTempPath(String path)
		{
			FileInfo compressedFile = new FileInfo(path);
			using (FileStream compressedInput = compressedFile.OpenRead())
			{
				using (GZipStream inflatingInput = new GZipStream(compressedInput, CompressionMode.Decompress))
				{
					String rawPath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".raw");
					FileInfo rawFile = new FileInfo(rawPath);
					using (FileStream rawOutput = rawFile.OpenWrite())
					{
						inflatingInput.CopyTo(rawOutput);
						return rawPath;
					}
				}
			}
		}
	}
}
