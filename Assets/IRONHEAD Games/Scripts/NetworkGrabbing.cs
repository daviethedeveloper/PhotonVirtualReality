using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    private PhotonView m_photonView;
    private Rigidbody rb;
    private bool isBeingHeld = false;

    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            // is being held 
            rb.isKinematic = true;
            gameObject.layer = 11;
        }
        else
        {
            rb.isKinematic = false;
            gameObject.layer = 9;
        }
    }

    public void OnSelectEntered()
    {
        Debug.Log("Grabbed");
        m_photonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);
        
        // check if its already in isMine = true
        if(m_photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Already Owner of this - object");
        }
        else
        {
            TransferOwnership();
        }
    }

    private void TransferOwnership()
    {
        m_photonView.RequestOwnership();
    }

    public void OnSelectExited()
    {
        Debug.Log("Released");
        m_photonView.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
    }


    #region IPun Callbacks


    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != m_photonView)
        {
            return;
        }
        Debug.Log("Ownership requested for: " + targetView.name + "from " + requestingPlayer.NickName);
        m_photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("OnOwnership Transferred to " + targetView.name + "from " + previousOwner.NickName);
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        
    }

    #endregion

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        isBeingHeld = false;
    }

}
