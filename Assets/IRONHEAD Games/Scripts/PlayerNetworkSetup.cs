using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXRRigGameObject;

    public GameObject AvatarHeadGameObject;

    public GameObject AvatarBodyGameObject;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            // the instantiated player is me
            LocalXRRigGameObject.SetActive(true);
            SetLayerRecursively(AvatarBodyGameObject, 7);
            SetLayerRecursively(AvatarHeadGameObject, 6);
        }
        else
        {
            // the player is remote 
            LocalXRRigGameObject.SetActive(false);
            SetLayerRecursively(AvatarBodyGameObject, 0);
            SetLayerRecursively(AvatarHeadGameObject, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
