using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
/*
 * Gestion de la connexion du joueur avec le serveur PUN.
 * 
 * Par : Jérémy Émond-Lapierre
 * 
 * Dernière modification : 18 novembre 2021
 * 
*/

public class GestionConnexion : MonoBehaviourPunCallbacks
{
    private void Awake(){
        //On connecte le joueur
        PhotonNetwork.ConnectUsingSettings();

        //Permettre au master de lancer les scène pour tous les joueurs
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Quand on se connecte sur le serveur!
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    //Méthode (CallBack) appelée lorsque la connexion est interrompue
    public override void OnDisconnected(DisconnectCause cause)  
    {
        print("le joueur a déconnecté " + cause.ToString());
    }

    //Quand un joueur rentre dans la salle
    public override void OnJoinedRoom()
	{
        if(PhotonNetwork.CountOfPlayers == 2){
            //Ici on metterait une variable à true pour lancer le jeu 
        }
        PhotonNetwork.Instantiate("AvatarReseau",new Vector3(Random.Range(-5f, 5f),0f,Random.Range(-5f, 5f)),Quaternion.identity, 0,null );
    } 
}
