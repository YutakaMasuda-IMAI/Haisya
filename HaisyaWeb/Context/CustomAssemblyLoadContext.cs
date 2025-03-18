using System;
using System.Runtime.Loader;

namespace HaisyaWeb.Context
{
    internal class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// 非管理対象ライブラリを読み込む関数
        /// </summary>
        /// <param name="absolutePath">DLLの絶対パス</param>
        /// <returns>ネイティブライブラリを読み込むOSハンドル</returns>
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDllFromPath(absolutePath);
        }
    }
}