using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
/*
 * Script associ� � la balle permettant de la d�truire
 * 
 * Par : Tristan Lapointe
 * 
 * Derni�re modification : 24 novembre 2021
 * 
*/

public class Balle : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //D�truire la balle apr�s 5 secondes peu importe son trajet
        Invoke("DetruireBalle", 5f);
    }

    //Fonction pour d�truire la balle
    public void DetruireBalle()
    {
        //Si c'est bien ma balle...
        if (photonView.IsMine)
        {
            //La d�truire sur r�seau
            PhotonNetwork.Destroy(gameObject);
        }
    }

    //Si elle touche quelque chose, la d�truire presque instantan�ment. Laisser un d�lai pour que photon soit content
    private void OnCollisionEnter(Collision collision)
    {
        //La d�truire apr�s 0.1 secondes
        Invoke("DetruireBalle", 0.1f);
    }
}
