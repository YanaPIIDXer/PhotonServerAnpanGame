using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using PhotonServerClient;
using ExitGames.Client.Photon;

/// <summary>
/// 実験用コード
/// 後で消す
/// </summary>
public class ConnectionTest : MonoBehaviour
{
    private PhotonClient client = new PhotonClient();

    void Awake()
    {
        var button = GetComponent<Button>();
        button.OnClickAsObservable()
              .Subscribe(async _ =>
              {
                  var token = this.GetCancellationTokenOnDestroy();
                  button.interactable = false;
                  try
                  {
                      await client.Connect("127.0.0.1:4530", "AnpanOnline", ConnectionProtocol.Tcp, token);
                      Debug.Log("Connection Success!");
                      await UniTask.Delay(1000);
                  }
                  catch (Exception e)
                  {
                      Debug.LogError(e.Message);
                  }
                  finally
                  {
                      client.Disconnect();
                      Debug.Log("Disconnect");
                      button.interactable = true;
                  }
              }).AddTo(gameObject);
    }
}
