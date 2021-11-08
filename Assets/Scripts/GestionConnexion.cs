//**********************************************************************
//Gestion de la connexion du joueur avec le serveur PUN.
//@The Penguinz
//2021-11-07
//**********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GestionConnexion : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake(){
        //On connecte le joueur
        PhotonNetwork.ConnectUsingSettings();
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
		//print("Nom salle = " + PhotonNetwork.CurrentRoom.Name );
		//print("joueurs connectés = " + PhotonNetwork.CountOfPlayers );
        if(PhotonNetwork.CountOfPlayers == 2){
            //Ici on metterait une variable à true pour lancer le jeu
        }
        Invoke("SpawnPrefab", 0.1f);
	}
    public void SpawnPrefab(){
        PhotonNetwork.Instantiate("AvatarSinge_Prefab",new Vector3(Random.Range(0f, 1f),0f,Random.Range(0f, 1f)),Quaternion.identity, 0,null );
    }

    
}
