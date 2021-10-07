using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
