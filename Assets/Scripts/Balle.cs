using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
/*
 * Script associé à la balle permettant de la détruire
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 24 novembre 2021
 * 
*/

public class Balle : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //Détruire la balle après 5 secondes peu importe son trajet
        Invoke("DetruireBalle", 5f);
    }

    //Fonction pour détruire la balle
    public void DetruireBalle()
    {
        //Si c'est bien ma balle...
        if (photonView.IsMine)
        {
            //La détruire sur réseau
            PhotonNetwork.Destroy(gameObject);
        }
    }

    //Si elle touche quelque chose, la détruire presque instantanément. Laisser un délai pour que photon soit content
    private void OnCollisionEnter(Collision collision)
    {
        //La détruire après 0.1 secondes
        Invoke("DetruireBalle", 0.1f);
    }
}
