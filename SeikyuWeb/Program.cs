using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SeikyuWeb
{
    /// <summary>
    /// Program クラス
    /// </summary>
    public class Program
    {
        /// <summary>
        /// アプリケーションのエントリポイント
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ホストビルダーを作成します。
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        /// <returns>ホストビルダー</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
