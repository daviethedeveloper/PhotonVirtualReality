using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoginManager : MonoBehaviourPunCallbacks
{

    #region Unity Methods
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
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
        Debug.Log("Connected to Master Server");
    }
    
    #endregion
}
