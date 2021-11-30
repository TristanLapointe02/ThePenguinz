using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ProprietaireArme : MonoBehaviourPunCallbacks
{
    public GameObject socket; //Référence au socket

    public void Start()
    {
        socket = GameObject.Find("Socket");
    }
    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            //Transférer le ownership au joueur local qui l'a touché
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);

        }
    }

    private void Update()
    {
        /*
        if(gameObject.name == "Boule")
        {
            //Désactiver le script sur le socket pour tous les joueurs qui sont pas owner de cet objet
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
        //Transférer le ownership au joueur local qui l'a touché
        photonView.TransferOwnership(PhotonNetwork.MasterClient);

    }*/
}
