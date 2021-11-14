using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public class deplacementEnnemi : MonoBehaviourPunCallbacks
{
    NavMeshAgent navAgent; //Raccourci pour la navmesh agent
    public GameObject[] joueurs; //Tableau contenant les joueurs
    public GameObject joueurAleatoire; //Joueur qui sera choisi al�atoirement au d�but

    void Start()
    {
        //Aller chercher le raccourci pour navmesh agent
        navAgent = GetComponent<NavMeshAgent>();

        //Stocker tous les joueurs dans le tableau
        joueurs = GameObject.FindGameObjectsWithTag("Player");

        //Trouver un joueur al�aoire
        joueurAleatoire = joueurs[Random.Range(0, joueurs.Length)]; 
    }

    void Update()
    {
        //Dire � l'agent de se diriger vers le joueur choisi
        navAgent.SetDestination(joueurAleatoire.transform.position);
    }
}
