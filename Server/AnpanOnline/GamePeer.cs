using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using AnpanGameCommon;
using AnpanOnline.PeerState;

namespace AnpanOnline
{
	/// <summary>
	/// Peer
	/// </summary>
	public class GamePeer : ClientPeer
	{
		/// <summary>
		/// 現在のState
		/// </summary>
		private PeerStateBase currentState = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="initRequest">初期化情報</param>
		public GamePeer(InitRequest initRequest)
			: base(initRequest)
		{
			currentState = new PeerStateTitle(this);
		}

		/// <summary>
		/// 切断された
		/// </summary>
		/// <param name="reasonCode">切断理由</param>
		/// <param name="reasonDetail">詳細</param>
		protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
		{
		}

		/// <summary>
		/// オペレーションを受信した
		/// </summary>
		/// <param name="operationRequest">リクエスト</param>
		/// <param name="sendParameters">送信パラメータ</param>
		protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
		{
			currentState.OnOperation(operationRequest);
		}
	}
}
