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
    public GameObject joueurAleatoire; //Joueur qui sera choisi aléatoirement au début
    public float vieEnnemi = 100f; //Vie de l'ennemi
    public bool mort; //Variable détectant la mort de l'ennemi

    void Start()
    {
        //Aller chercher le raccourci pour navmesh agent
        navAgent = GetComponent<NavMeshAgent>();

        //Stocker tous les joueurs dans le tableau
        joueurs = GameObject.FindGameObjectsWithTag("Player");

        //Trouver un joueur aléaoire
        joueurAleatoire = joueurs[Random.Range(0, joueurs.Length)];
    }

    void Update()
    {
        //Dire à l'agent de se diriger vers le joueur choisi
        navAgent.SetDestination(joueurAleatoire.transform.position);

        //MORT DU ENNEMI
        if (vieEnnemi <= 0)
        {
            //Signaler qu'il est mort
            mort = true;

            //Activer l'animation de mort
            //GetComponent<Animator>().SetBool("Mort", true);

            //Appeler la fonction qui joue le son de mort en RPC pour tous
            //photonView.RPC("JoueSonMort", RpcTarget.All);

            //Détruire l'ennemi sur réseau
            int pvID = gameObject.GetComponent<PhotonView>().ViewID;

            photonView.RPC("MortEnnemi", RpcTarget.MasterClient, pvID);
        }

        //S'ASSURER QUE LA VIE RESTE DANS SES LIMITES
        if (vieEnnemi >= 100f)
        {
            vieEnnemi = 100f;
        }
        else if (vieEnnemi <= 0)
        {
            vieEnnemi = 0f;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        //Si un joueur touche une potion
        if (collision.gameObject.name == "Sword")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 25f;
        }

        //Si un joueur touche une potion
        if (collision.gameObject.tag == "Balle")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 50f;
        }
    }

    //Fonction qui détruit l'ennemi
    [PunRPC]
    public void MortEnnemi(int pvID)
    {
        PhotonNetwork.Destroy(PhotonView.Find(pvID));
    }
}