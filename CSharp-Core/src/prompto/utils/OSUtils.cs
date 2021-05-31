using System;
using System.Runtime.InteropServices;

namespace prompto.utils
{
    public enum OS
    {
        LINUX,
        WINDOWS,
        MACOSX
    }

    public abstract class OSUtils
    {

        public static OS CurrentOS
        {
            get {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return OS.LINUX;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return OS.MACOSX;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return OS.WINDOWS;
                else
                    throw new Exception("Unsupported OSPlatform: " + RuntimeInformation.OSDescription);
            }
        }
    }
}
