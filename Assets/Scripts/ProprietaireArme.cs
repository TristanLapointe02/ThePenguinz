using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ProprietaireArme : MonoBehaviourPunCallbacks
{
    public GameObject socket; //R�f�rence au socket

    public void Start()
    {
        socket = GameObject.Find("Socket");
    }
    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            //Transf�rer le ownership au joueur local qui l'a touch�
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);

        }
    }

    private void Update()
    {
        /*
        if(gameObject.name == "Boule")
        {
            //D�sactiver le script sur le socket pour tous les joueurs qui sont pas owner de cet objet
            if (photonView.Owner != PhotonNetwork.LocalPlayer)
            {
                socket.SetActive(false);
            }
            else
            {
                socket.SetActive(true);
            }
        }*/
    }

    /*public void OnTriggerExit(Collider collision)
    {   
        //Print
        print("im out zooop");
        //Transf�rer le ownership au joueur local qui l'a touch�
        photonView.TransferOwnership(PhotonNetwork.MasterClient);

    }*/
}
