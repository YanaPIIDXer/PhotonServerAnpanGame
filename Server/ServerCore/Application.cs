using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;

namespace AnpanOnline
{
	/// <summary>
	/// アプリケーションクラス
	/// </summary>
	public class Application : ApplicationBase
	{
		/// <summary>
		/// Peer生成
		/// </summary>
		/// <param name="initRequest">初期化情報</param>
		/// <returns>Peerインスタンス</returns>
		protected override PeerBase CreatePeer(InitRequest initRequest)
		{
			return new GamePeer(initRequest);
		}

		/// <summary>
		/// 起動処理
		/// </summary>
		protected override void Setup()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		protected override void TearDown()
		{
			throw new NotImplementedException();
		}
	}
}
