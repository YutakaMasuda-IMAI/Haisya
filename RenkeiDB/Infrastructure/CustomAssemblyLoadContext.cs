using System;
using System.Runtime.Loader;

namespace RenkeiDB.Infrastructure
{
    internal class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Unmanagedライブラリのロード
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <returns></returns>
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDllFromPath(absolutePath);
        }
    }
}
