using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace UI.Title
{
    /// <summary>
    /// タイトル画面
    /// </summary>
    public class TitleScreen : MonoBehaviour
    {
        /// <summary>
        /// ログイン情報
        /// </summary>
        public class LoginInfo
        {
            /// <summary>
            /// メールアドレス
            /// </summary>
            public string EMailAddress = "";

            /// <summary>
            /// パスワード
            /// </summary>
            public string Password = "";
        }

        /// <summary>
        /// メールアドレス入力欄
        /// </summary>
        [SerializeField]
        private InputField emailAddressInput = null;

        /// <summary>
        /// パスワード入力欄
        /// </summary>
        [SerializeField]
        private InputField passwordInput = null;

        /// <summary>
        /// 新規登録ボタン
        /// </summary>
        [SerializeField]
        private Button registerButton = null;

        /// <summary>
        /// ログインボタン
        /// </summary>
        [SerializeField]
        private Button logInButton = null;

        /// <summary>
        /// 新規登録ボタンが押された
        /// </summary>
        public IObservable<LoginInfo> OnRegister
        {
            get
            {
                return registerButton.OnClickAsObservable()
                                     .Select(_ => new LoginInfo
                                     {
                                         EMailAddress = emailAddressInput.text,
                                         Password = passwordInput.text
                                     });
            }
        }

        /// <summary>
        /// ログインボタンが押された
        /// </summary>
        public IObservable<LoginInfo> OnLogIn
        {
            get
            {
                return logInButton.OnClickAsObservable()
                                     .Select(_ => new LoginInfo
                                     {
                                         EMailAddress = emailAddressInput.text,
                                         Password = passwordInput.text
                                     });
            }
        }

        void Awake()
        {
            // アドレスとパスワードの両方を入力しないとボタンが押せない
            emailAddressInput.OnValueChangedAsObservable()
                             .Merge(passwordInput.OnValueChangedAsObservable())
                             .Subscribe(_ =>
                             {
                                 bool b = !string.IsNullOrEmpty(emailAddressInput.text) && !string.IsNullOrEmpty(passwordInput.text);
                                 registerButton.interactable = b;
                                 logInButton.interactable = b;
                             }).AddTo(gameObject);
        }
    }
}
