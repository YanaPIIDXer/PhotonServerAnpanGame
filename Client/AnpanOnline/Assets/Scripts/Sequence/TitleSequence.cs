using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Title;
using UniRx;
using System;

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
                       .Subscribe(info => Debug.Log("Register Email:" + info.EMailAddress))
                       .AddTo(gameObject);

            titleScreen.OnLogIn
                       .Subscribe(info => Debug.Log("LogIn Email:" + info.EMailAddress))
                       .AddTo(gameObject);
        }
    }
}
