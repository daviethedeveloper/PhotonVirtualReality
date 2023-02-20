using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoginUIManager : MonoBehaviour
{

    public GameObject ConnectOptionsPanelGameObject;

    public GameObject ConnectWithNamePanelGameObject;

    #region Unity Methods

    
    void Start()
    {
        ConnectOptionsPanelGameObject.SetActive(true);
        ConnectWithNamePanelGameObject.SetActive(false);
    }

    #endregion
}
