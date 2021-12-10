using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
/*
 * Gestion du propriétaire (Ownership) de l'arme.
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 24 novembre 2021
 * 
*/
public class ProprietaireArme : MonoBehaviourPunCallbacks
{
    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            //Transf�rer le ownership au joueur local qui l'a touch�
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
