using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnpanGameCommon;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Photon.SocketServer;

namespace AnpanOnline.Auth
{
	/// <summary>
	/// ログイン時のトークン検証
	/// </summary>
	public static class LogInTokenVerify
	{
		/// <summary>
		/// 認証オブジェクト
		/// </summary>
		private static FirebaseAuth auth = null;

		/// <summary>
		/// トークン検証
		/// </summary>
		/// <param name="token">トークン</param>
		/// <param name="peer">結果を返すPeer</param>
		public static void Verify(string token, GamePeer peer)
		{
			if (auth == null)
			{
				try
				{
					var app = FirebaseApp.Create(new AppOptions()
					{
						Credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile("bin/anpanonline-724bb-firebase-adminsdk-mwe42-e0ff379114.json")
					});
					auth = FirebaseAuth.GetAuth(app);
				}
				catch (Exception e)
				{
					Logger.Log.ErrorFormat("FirebaseAuth Create Failed:{0}", e.Message);
					SendResult(peer, false);
				}
			}

			VerifyAsync(token, peer);
		}

		/// <summary>
		/// 検証処理
		/// 非同期で行う
		/// </summary>
		/// <param name="token">トークン</param>
		/// <param name="peer">結果を返すPeer</param>
		private static async void VerifyAsync(string token, GamePeer peer)
		{
			try
			{
				var verifyToken = await auth.VerifyIdTokenAsync(token);
				SendResult(peer, true);
			}
			catch (Exception e)
			{
				SendResult(peer, false);
			}
		}

		/// <summary>
		/// 結果を送信
		/// </summary>
		/// <param name="peer">送信するPeer</param>
		/// <param name="bResult">結果</param>
		private static void SendResult(GamePeer peer, bool bResult)
		{
			var response = new OperationResponse()
			{
				OperationCode = (byte)EOpCode.LogIn,
				Parameters = new Dictionary<byte, object>() { { EParamCode.VerifyResult, bResult } }
			};
			peer.SendOperationResponse(response, new SendParameters());
		}
	}
}
