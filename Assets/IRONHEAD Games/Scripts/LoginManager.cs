using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_Input;

    #region Unity Methods
    
    void Start()
    {
    }
    
    
    #endregion


    #region UI Callback Methods

    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if (PlayerName_Input != null)
        {
            PhotonNetwork.NickName = PlayerName_Input.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #endregion

    #region Photon Callback Methods

    // connection is established
    public override void OnConnected()
    {
       Debug.Log("On Connected method is called - Server is available");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server with name - " + PlayerName_Input.text);
        PhotonNetwork.LoadLevel("HomeScene");
    }
    
    #endregion
}
