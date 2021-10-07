using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhotonServerClient;

namespace Network
{
    /// <summary>
    /// サーバコネクション
    /// ※PhotonClientをSingleton化するためのクラス
    /// </summary>
    public static class ServerConnection
    {
        private static PhotonClient instance = new PhotonClient();
        public static PhotonClient Instance { get { return instance; } }
    }
}
