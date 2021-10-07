using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;

namespace AnpanOnline
{
	/// <summary>
	/// アプリケーションクラス
	/// </summary>
	public class Application : ApplicationBase
	{
		/// <summary>
		/// Peerの生成
		/// </summary>
		/// <param name="initRequest">初期化情報</param>
		/// <returns>Peerのインスタンス</returns>
		protected override PeerBase CreatePeer(InitRequest initRequest)
		{
			return new GamePeer(initRequest);
		}

		/// <summary>
		/// 初期化処理
		/// </summary>
		protected override void Setup()
		{
			SetupLog();
			LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().Name).Debug("Anpan Online Server Setup OK.");
		}

		/// <summary>
		/// ログのセットアップ
		/// </summary>
		private void SetupLog()
		{
			log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
			var configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
			if (configFileInfo.Exists)
			{
				LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
				XmlConfigurator.ConfigureAndWatch(configFileInfo);
			}
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		protected override void TearDown()
		{
		}
	}
}
