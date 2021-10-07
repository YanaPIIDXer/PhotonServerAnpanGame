using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhotonServerClient;
using UniRx;
using System;
using ExitGames.Client.Photon;
using Cysharp.Threading.Tasks;
using AnpanGameCommon;
using Game;

public class TestButton : MonoBehaviour
{
    void Awake()
    {
        var button = GetComponent<Button>();
        button.OnClickAsObservable()
              .Subscribe(async _ =>
              {
                  var client = new PhotonClient();
                  var token = this.GetCancellationTokenOnDestroy();
                  try
                  {
                      await client.Connect(GameConfigure.ServerHost, "AnpanOnline", ConnectionProtocol.Tcp, token);
                      Debug.Log("Connected!");

                      await UniTask.Delay(1000);

                      var response = await client.SendOperationRequest((byte)EOpCode.LogIn, new Dictionary<byte, object>(), (byte)EOpCode.LogIn, token);
                      Debug.Log("Response OK!");
                      Debug.Log("Message:" + response[0]);

                      await UniTask.Delay(1000);
                  }
                  catch (Exception e)
                  {
                      Debug.LogError(e.Message);
                  }
                  finally
                  {
                      client.Disconnect();
                      Debug.Log("Disconnect.");
                  }
              }).AddTo(gameObject);
    }
}
