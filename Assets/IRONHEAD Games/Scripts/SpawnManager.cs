using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject GenericVRPlayerPrefab;

    public Vector3 spawnPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        // checks if connected to network and read to join/leave
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // instantiate player across the network
            PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name,spawnPosition,Quaternion.identity );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
