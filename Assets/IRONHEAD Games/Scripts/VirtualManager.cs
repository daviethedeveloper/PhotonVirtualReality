using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualManager : MonoBehaviourPunCallbacks
{
    #region Photon Callback Methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName +" joined to " +  "Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    
    #endregion
}
