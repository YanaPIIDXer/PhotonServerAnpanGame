using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using AnpanGameCommon;

namespace AnpanOnline
{
	/// <summary>
	/// Peer
	/// </summary>
	public class GamePeer : ClientPeer
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="initRequest">初期化情報</param>
		public GamePeer(InitRequest initRequest)
			: base(initRequest)
		{
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
			switch((EOpCode)operationRequest.OperationCode)
			{
				case EOpCode.LogIn:
				default:

					{
						OperationResponse response = new OperationResponse
						{
							ReturnCode = 0,
							Parameters = new Dictionary<byte, object> { { 0, "OK!" } }
						};
						SendOperationResponse(response, sendParameters);
					}
					break;
			}
		}
	}
}
