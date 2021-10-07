using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;

namespace AnpanOnline
{
	/// <summary>
	/// ログ出力クラス
	/// </summary>
	public class Logger
	{
		/// <summary>
		/// ロガー
		/// </summary>
		private ILogger logger { get; set; }

		public static ILogger Log
		{
			get { return instance.logger; }
			set { instance.logger = value; }
		}

		#region Singleton
		private Logger() { }
		private static Logger instance = new Logger();
		#endregion
	}
}
