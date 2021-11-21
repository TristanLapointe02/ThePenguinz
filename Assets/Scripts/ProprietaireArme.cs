using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ProprietaireArme : MonoBehaviourPunCallbacks
{
    public void OnCollisionEnter(Collision collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            //Transférer le ownership au joueur local qui l'a touché
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);

        }
    }
}
