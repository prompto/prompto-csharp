using System;
using System.Collections.Generic;
using System.IO;
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
	}
}
