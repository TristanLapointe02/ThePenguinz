using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Balle : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //Détruire la balle après 5 secondes
        Invoke("DetruireBalle", 5f);
        //Jouer le son en RPC pour tous
        //photonView.RPC("JoueSon", RpcTarget.All);
    }

    //Fonction pour détruire la balle
    public void DetruireBalle()
    {
        //Si c'est bien ma balle...
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    //Si elle touche quelque chose, la détruire presque instantanément. Laisser un délai pour que photon soit content
    private void OnTriggerEnter(Collider collision)
    {
        Invoke("DetruireBalle", 0.1f);
    }
}
