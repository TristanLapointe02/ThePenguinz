using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class spawnEnnemis : MonoBehaviourPunCallbacks
{
    public GameObject[] ennemisFaibles; //Tableau des ennemis
    public GameObject boss; //Boss du niveau
    public Vector3[] positions; //Tableau des positions aléatoires où les ennemis peuvent spawn
    public float tempsSpawn; //Intervalle de temps à laquelle les ennemis vont spawn
    public bool peutSpawn = true; //Vérifie si les ennemis peuvent spawn
    
    void Start()
    {
        //Ennemis faibles = Un nouveau à chaque deux secondes
        InvokeRepeating("CreationEnnemi", 1f, tempsSpawn);
        Invoke("CreationBoss", 2f);
    }

    void Update()
    {
        
    }

    public void CreationEnnemi()
    {
        if (peutSpawn)
        {
            //Défénir une position aléatoire
            Vector3 positionAleatoire = positions[Random.Range(0, positions.Length)];

            //Générer cet ennemi sur réseau
            GameObject nouvelEnnemi = PhotonNetwork.InstantiateRoomObject("Ennemi", positionAleatoire, Quaternion.identity, 0, null);
        }
    }

    public void CreationBoss()
    {
        Vector3 positionAleatoire = positions[Random.Range(0, positions.Length)];
        GameObject nouvelEnnemi = PhotonNetwork.InstantiateRoomObject("Boss", positionAleatoire, Quaternion.identity, 0, null);
    }
}
