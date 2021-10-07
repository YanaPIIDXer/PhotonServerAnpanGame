using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnpanGameCommon;
using Photon.SocketServer;

namespace AnpanOnline.PeerState
{
	/// <summary>
	/// PeerState基底クラス
	/// </summary>
	public abstract class PeerStateBase
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="parent">親</param>
		public PeerStateBase(GamePeer parent)
		{
			this.Parent = parent;
		}

		/// <summary>
		/// 親
		/// </summary>
		protected GamePeer Parent { get; private set; }

		/// <summary>
		/// オペレーションハンドラ
		/// </summary>
		private Dictionary<EOpCode, Func<OperationRequest, OperationResponse>> operationHandlers = new Dictionary<EOpCode, Func<OperationRequest, OperationResponse>>();

		/// <summary>
		/// オペレーションコードに対応したハンドラの追加
		/// ※ハンドラはオペレーションに対するレスポンスを返す
		/// 　レスポンスが無い場合はnullを返す事
		/// </summary>
		/// <param name="opCode">オペレーションコード</param>
		/// <param name="handler">ハンドラ</param>
		protected void AddOperationHandler(EOpCode opCode, Func<OperationRequest, OperationResponse> handler)
		{
			operationHandlers[opCode] = handler;
		}

		public void OnOperation(OperationRequest request)
		{
			EOpCode opCode = (EOpCode)request.OperationCode;
			if (!operationHandlers.ContainsKey(opCode))
			{
				Logger.Log.WarnFormat("OperationCode:{0} is not handled.", opCode.ToString());
				return;
			}

			OperationResponse response = operationHandlers[opCode](request);
			if (response != null)
			{
				Parent.SendOperationResponse(response, new SendParameters());
			}
		}
	}
}
