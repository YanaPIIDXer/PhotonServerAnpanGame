using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Title;
using UniRx;
using System;
using Firebase.Auth;
using Cysharp.Threading.Tasks;
using Network;
using Game;
using AnpanGameCommon;

namespace Sequence
{
    /// <summary>
    /// タイトルシーケンス
    /// </summary>
    public class TitleSequence : MonoBehaviour
    {
        /// <summary>
        /// タイトル画面
        /// </summary>
        [SerializeField]
        private TitleScreen titleScreen = null;

        void Awake()
        {
            titleScreen.OnRegister
                       .Subscribe(info => Register(info))
                       .AddTo(gameObject);

            titleScreen.OnLogIn
                       .Subscribe(info => LogIn(info))
                       .AddTo(gameObject);
        }

        /// <summary>
        /// 新規登録
        /// </summary>
        /// <param name="info">認証情報</param>
        private async void Register(TitleScreen.LoginInfo info)
        {
            try
            {
                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                var user = await auth.CreateUserWithEmailAndPasswordAsync(info.EMailAddress, info.Password);
                var token = await user.TokenAsync(true);
                var result = await ConnectToServer(token);
                if (!result)
                {
                    titleScreen.OnProcessError();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                titleScreen.OnProcessError();
            }
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="info">認証情報</param>
        private async void LogIn(TitleScreen.LoginInfo info)
        {
            try
            {
                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                var user = await auth.SignInWithEmailAndPasswordAsync(info.EMailAddress, info.Password);
                var token = await user.TokenAsync(true);
                var result = await ConnectToServer(token);
                if (!result)
                {
                    titleScreen.OnProcessError();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                titleScreen.OnProcessError();
            }
        }

        /// <summary>
        /// サーバに接続
        /// </summary>
        /// <param name="Token">トークン</param>
        private async UniTask<bool> ConnectToServer(string token)
        {
            try
            {
                var cancelToken = this.GetCancellationTokenOnDestroy();
                await ServerConnection.Instance.Connect(GameConfigure.ServerHost, "AnpanOnline", ExitGames.Client.Photon.ConnectionProtocol.Tcp, cancelToken);
                Dictionary<byte, object> loginParams = new Dictionary<byte, object>() { { EParamCode.AuthToken, token } };
                var response = await ServerConnection.Instance.SendOperationRequest((byte)EOpCode.LogIn, loginParams, (byte)EOpCode.LogIn, cancelToken);
                if (!(bool)response.Parameters[EParamCode.VerifyResult])
                {
                    Debug.LogError("Verify Error...");
                    return false;
                }
                Debug.Log("LogIn Success!!");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                ServerConnection.Instance.Disconnect();
                return false;
            }
            return true;
        }
    }
}
