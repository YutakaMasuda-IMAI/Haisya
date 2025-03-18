using System;
using System.Runtime.Loader;

namespace SeikyuWeb.Infrastructure
{
    /// <summary>
    /// カスタムアセンブリロードコンテキストクラス
    /// </summary>
    internal class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// 非管理対象ライブラリをロードする関数
        /// </summary>
        /// <param name="absolutePath">DLLのパス</param>
        /// <returns>ネイティブライブラリのOSハンドル</returns>
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDllFromPath(absolutePath);
        }
    }
}
