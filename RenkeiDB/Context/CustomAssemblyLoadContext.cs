using System;
using System.Runtime.Loader;

namespace RenkeiDB.Context
{
    /// <summary>
    /// カスタムアセンブリロードコンテキスト
    /// </summary>
    internal class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Unmanagedライブラリのロード
        /// </summary>
        /// <param name="absolutePath">DLL パス</param>
        /// <returns>OSがロードをハンドルするネイティブライブラリ</returns>
        public IntPtr LoadUnmanagedLibrary(string absolutePath) => LoadUnmanagedDllFromPath(absolutePath);
    }
}
