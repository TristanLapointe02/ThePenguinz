using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class spawnEnnemis : MonoBehaviourPunCallbacks
{
    public GameObject[] ennemisFaibles; //Tableau des ennemis
    public GameObject boss; //Boss du niveau
    public Vector3[] positions; //Tableau des positions al�atoires o� les ennemis peuvent spawn
    public float tempsSpawn; //Intervalle de temps � laquelle les ennemis vont spawn
    public bool peutSpawn = true; //V�rifie si les ennemis peuvent spawn
    
    void Start()
    {
        //Ennemis faibles = Un nouveau � chaque deux secondes
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
            //D�f�nir une position al�atoire
            Vector3 positionAleatoire = positions[Random.Range(0, positions.Length)];

            //G�n�rer cet ennemi sur r�seau
             if(PhotonNetwork.IsMasterClient == true){
                 GameObject nouvelEnnemi = PhotonNetwork.InstantiateRoomObject("Ennemi", positionAleatoire, Quaternion.identity, 0, null);
             }
            
        }
    }

    public void CreationBoss()
    {
        Vector3 positionAleatoire = positions[Random.Range(0, positions.Length)];
        if(PhotonNetwork.IsMasterClient == true){
            GameObject nouvelEnnemi = PhotonNetwork.InstantiateRoomObject("Boss", positionAleatoire, Quaternion.identity, 0, null);
        }
    }
}
