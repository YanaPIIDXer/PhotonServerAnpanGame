using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using AnpanGameCommon;

namespace AnpanOnline.PeerState
{
	public class PeerStateTitle : PeerStateBase
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="parent">親</param>
		public PeerStateTitle(GamePeer parent)
			: base(parent)
		{
			AddOperationHandler(EOpCode.LogIn, LogInHandler);
		}

		/// <summary>
		/// ログインオペレーションハンドラ
		/// </summary>
		/// <param name="request">リクエスト</param>
		/// <returns>レスポンス</returns>
		private OperationResponse LogInHandler(OperationRequest request)
		{
			return new OperationResponse()
			{
				OperationCode = (byte)EOpCode.LogIn,
				Parameters = new Dictionary<byte, object>() { { EParamCode.VerifyResult, true } }		// TODO:認証トークン検証処理の実装
			};
		}
	}
}
