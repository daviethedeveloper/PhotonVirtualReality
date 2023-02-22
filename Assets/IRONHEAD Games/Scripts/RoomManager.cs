using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;
using UnityEngine.XR.Interaction.Toolkit;

public class RoomManager : MonoBehaviourPunCallbacks
{

    #region UI Callback Methods

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion

    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room has been created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The Local Player player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);

        object myMap;
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiPlayerVRConstants.MAP_TYPE_KEY))
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiPlayerVRConstants.MAP_TYPE_KEY,out myMap))
            {
                Debug.Log("Joined the room with map: " + (string)myMap);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName +" joined to " +  "Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiPlayerVRConstants.MAP_TYPE_KEY };
        /*
         * There are actually two different maps
         * 1. The school map = "school"
         * 2. The outdoor map  = "outdoor"
         */

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiPlayerVRConstants.MAP_TYPE_KEY, "school"}};
        
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;
        
        
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
        
    }

    #endregion
}
