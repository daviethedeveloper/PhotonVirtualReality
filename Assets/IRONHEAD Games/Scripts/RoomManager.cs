using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    public TextMeshProUGUI OccupanyRateText_ForSchool;
    public TextMeshProUGUI OccupanyRateText_ForOutdoor;
    

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
        // Check if connected to servers or not
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            // join default lobby
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    #region UI Callback Methods

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void OnEnteredButtonClicked_Outdoor()
    {
        mapType = MultiPlayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable expectedCustomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiPlayerVRConstants.MAP_TYPE_KEY, mapType }};
        PhotonNetwork.JoinRandomRoom(expectedCustomProperties, 0);
    }

    public void OnEnteredButtonClicked_School()
    {
        mapType = MultiPlayerVRConstants.MAP_TYPE_VALUE_SCHOOL;
        ExitGames.Client.Photon.Hashtable expectedCustomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiPlayerVRConstants.MAP_TYPE_KEY, mapType }};
        PhotonNetwork.JoinRandomRoom(expectedCustomProperties, 0);
    }

    #endregion

    #region Photon Callback Methods

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to servers again! ");
        PhotonNetwork.JoinLobby();
    }

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

                if ((string)mapType == MultiPlayerVRConstants.MAP_TYPE_VALUE_SCHOOL)
                {
                    // Load the School Scene
                    PhotonNetwork.LoadLevel("World_School");
                    
                }else if ((string)mapType == MultiPlayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
                {
                    // Load the Outdoor Scene
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName +" joined to " +  "Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            // There is no room at all 
            OccupanyRateText_ForOutdoor.text = 0 + "/" + 20;
            OccupanyRateText_ForSchool.text = 0 + "/" + 20;
        }

        foreach (RoomInfo room in roomList)
        {
            if (room.Name.Contains(MultiPlayerVRConstants.MAP_TYPE_VALUE_SCHOOL))
            {
                // if contains the school string in the name
                // then this is the school - Update school room occupancy field
                OccupanyRateText_ForSchool.text = room.PlayerCount + " / " + 20;
            }else if (room.Name.Contains(MultiPlayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                OccupanyRateText_ForOutdoor.text = room.PlayerCount + " / " + 20;
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined default lobby");
    }

    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + mapType + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiPlayerVRConstants.MAP_TYPE_KEY };
        /*
         * There are actually two different maps
         * 1. The school map = "school"
         * 2. The outdoor map  = "outdoor"
         */

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiPlayerVRConstants.MAP_TYPE_KEY, mapType}};
        
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;
        
        
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
        
    }

    #endregion
}
