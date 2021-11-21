using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class spawnEnnemisObjets : MonoBehaviourPunCallbacks
{
    public GameObject[] ennemisFaibles; //Tableau des ennemis
    public GameObject boss; //Boss du niveau
    public Vector3[] positionsEnnemis; //Tableau des positions aléatoires où les ennemis peuvent spawn
    public GameObject[] armes; //Tableau contenant toutes les armes à instancier
    public GameObject[] positionsArmes; //Tableau faisant aux positions où il est possible d'instancier une arme
    public float tempsSpawn; //Intervalle de temps à laquelle les ennemis vont spawn
    public bool peutSpawn = true; //Vérifie si les ennemis peuvent spawn
    
    void Start()
    {
        //Ennemis faibles = Un nouveau à chaque deux secondes
        InvokeRepeating("CreationEnnemi", 5f, tempsSpawn);
        Invoke("CreationBoss", 20f);
        Invoke("CreationArmes", 2f);
    }

    //Fonction qui crée un ennemi
    public void CreationEnnemi()
    {
        if (peutSpawn)
        {
            //Définir une position aléatoire
            Vector3 positionAleatoire = positionsEnnemis[Random.Range(0, positionsEnnemis.Length)];

            //Générer cet ennemi sur réseau
             if(PhotonNetwork.IsMasterClient == true){
                 PhotonNetwork.InstantiateRoomObject("Ennemi", positionAleatoire, Quaternion.identity, 0, null);
             } 
        }
    }

    //Fonction qui crée un boss
    public void CreationBoss()
    {
        //Définir une position aléatoire
        Vector3 positionAleatoire = positionsEnnemis[Random.Range(0, positionsEnnemis.Length)];

        //Générer ce boss sur réseau
        if (PhotonNetwork.IsMasterClient == true){
            PhotonNetwork.InstantiateRoomObject("Boss", positionAleatoire, Quaternion.identity, 0, null);
        }
    }
    public void CreationArmes()
    {
        //Instancier les armes à des positions au hasard
        for (int i = 0; i < positionsArmes.Length; i++)
        {
            //Piger un nombre aléatoire pour savoir quelle arme instancier
            int armeAleatoire = Random.Range(0, armes.Length);

            //Définir la position suivante
            Vector3 positionSuivante = positionsArmes[i].transform.position;

            //Si c'est le Master Client  
            if (PhotonNetwork.IsMasterClient == true)
            {
                //Fait apparaître une arme aléatoire en suivant le tableau des positions sur réseau
                PhotonNetwork.InstantiateRoomObject(armes[armeAleatoire].gameObject.name, positionSuivante, Quaternion.identity, 1, null);
            }
        }

    }
}
