using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using AnpanGameCommon;
using AnpanOnline.Auth;

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
			LogInTokenVerify.Verify((string)request.Parameters[EParamCode.AuthToken], Parent);
			// 非同期処理で検証を行い、遅延して結果を返す
			return null;
		}
	}
}
