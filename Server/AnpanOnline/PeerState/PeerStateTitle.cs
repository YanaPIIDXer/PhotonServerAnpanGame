using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using AnpanGameCommon;
using FirebaseAdmin;
using FirebaseAdmin.Auth;

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
			bool bVerify = false;
			try
			{
				// TODO:Singletonにした方がいいんじゃないか・・・？
				var app = FirebaseApp.Create(new AppOptions()
				{
					Credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile("bin/anpanonline-724bb-firebase-adminsdk-mwe42-e0ff379114.json")
				});
				FirebaseAuth auth = FirebaseAuth.GetAuth(app);
				var token = auth.VerifyIdTokenAsync((string)request.Parameters[EParamCode.AuthToken]).Result;
				bVerify = (token != null);
			}
			catch (Exception e)
			{
				Logger.Log.ErrorFormat("Verify Error:{0}", e.Message);
				Logger.Log.Error(e.StackTrace);
				bVerify = false;
			}

			return new OperationResponse()
			{
				OperationCode = (byte)EOpCode.LogIn,
				Parameters = new Dictionary<byte, object>() { { EParamCode.VerifyResult, bVerify } }
			};
		}
	}
}
