using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Title;
using UniRx;
using System;
using Firebase.Auth;

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
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            try
            {
                var user = await auth.CreateUserWithEmailAndPasswordAsync(info.EMailAddress, info.Password);
                var token = await user.TokenAsync(true);
                Debug.Log("Register User Token:" + token);
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
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            try
            {
                var user = await auth.SignInWithEmailAndPasswordAsync(info.EMailAddress, info.Password);
                var token = await user.TokenAsync(true);
                Debug.Log("LogIn User Token:" + token);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                titleScreen.OnProcessError();
            }
        }
    }
}
