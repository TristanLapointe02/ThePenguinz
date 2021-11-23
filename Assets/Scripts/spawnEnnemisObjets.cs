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
    public float tempsSpawnEnnemis; //Intervalle de temps à laquelle les ennemis vont spawn
    public bool ennemiPeutSpawn = true; //Vérifie si les ennemis peuvent spawn
    public bool bossPeutSpawn = true; //Vérifie si le boss peuvent spawn
    public int nbEnnemisATuer; //Noombre d'ennemis à tuer pour que le boss spawn
    
    void Start()
    {
        //Appel des ennemis faibles
        InvokeRepeating("CreationEnnemi", 0f, tempsSpawnEnnemis);

        //Appel des armes
        Invoke("CreationArmes", 2f);
    }

    void Update()
    {
        //Si les joueurs ont tués assez d'ennemis, arrêter de faire spawn les ennemis
        if (deplacementEnnemi.compteurMort >= nbEnnemisATuer)
        {
            ennemiPeutSpawn = false;
        }

        //...Et lorsque les ennemis peuvent plus spawn, faire spawn un boss
        if(ennemiPeutSpawn == false && bossPeutSpawn == true)
        {
            bossPeutSpawn = false;
            Invoke("CreationBoss", 1f);
        }
    }
    //Fonction qui crée un ennemi
    public void CreationEnnemi()
    {
        if (ennemiPeutSpawn)
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

    //Fonction qui crée les armes au début
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
