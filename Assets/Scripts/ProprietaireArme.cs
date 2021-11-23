using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ProprietaireArme : MonoBehaviourPunCallbacks
{
    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            //Print
            print("playerrr heyhey");
            //Transf�rer le ownership au joueur local qui l'a touch�
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);

        }
    }

    /*public void OnTriggerExit(Collider collision)
    {   
        //Print
        print("im out zooop");
        //Transf�rer le ownership au joueur local qui l'a touch�
        photonView.TransferOwnership(PhotonNetwork.MasterClient);

    }*/
}
