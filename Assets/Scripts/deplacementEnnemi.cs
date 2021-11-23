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
    public float vieEnnemi; //Vie de l'ennemi
    public bool mort; //Variable d�tectant la mort de l'ennemi
    public static int compteurMort = 0; //Compteur de mort
    bool enVie = true; //Déterminer si l'ennemi est en vie


    void Start()
    {
        //D�f�nir la vie de l'ennemi et du boss
        if(gameObject.name == "Ennemi(Clone)")
        {
            vieEnnemi = 100f;
        }

        else if (gameObject.name == "Boss(Clone)")
        {
            vieEnnemi = 100f;
        }

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
        if(enVie){
            navAgent.SetDestination(joueurAleatoire.transform.position);
        }
        

        //MORT DU ENNEMI
        if (vieEnnemi <= 0 && enVie)
        {
            //Signaler qu'il est mort
            mort = true;

            //Activer l'animation de mort
            GetComponent<Animator>().SetBool("Mort", true);

            //Appeler la fonction qui joue le son de mort en RPC pour tous
            //photonView.RPC("JoueSonMort", RpcTarget.All);

            //D�truire l'ennemi sur r�seau
            int pvID = gameObject.GetComponent<PhotonView>().ViewID;

            photonView.RPC("MortEnnemi", RpcTarget.MasterClient, pvID, 3);

            //Arr�ter l'ennemi pour pas qu'il poursuit son chemin
            GetComponent<NavMeshAgent>().enabled = false;

            //Enlever le collider
            //GetComponent<Collider>().enabled = false;

            //Freeze le rigidbody
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            //Si c'�tait le boss
            if (gameObject.name == "Boss(Clone)")
            {
                //Faire spawn la boule � neige sur r�seau
                if (PhotonNetwork.IsMasterClient == true)
                {
                    PhotonNetwork.InstantiateRoomObject("boule", gameObject.transform.position + transform.up * 2, Quaternion.identity, 0, null);
                }
            }
            enVie = false;
            
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
        //Si l'ennemi touche une épée
        if (collision.gameObject.name == "Sword(Clone)")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 100f;
        }

        //Si l'ennemi se fait toucher par une balle
        if (collision.gameObject.tag == "Balle")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 50f;
        }

        //Si l'ennemi se fait exploser fatalement
        if (collision.gameObject.name == "collisionGrenade")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 300f;
            //GetComponent<Rigidbody>().AddForce(transform.up * 10f, ForceMode.Impulse);
            
        }
    }

    //Fonction qui d�truit l'ennemi
    [PunRPC]
    IEnumerator MortEnnemi(int pvID, int delai)
    {
        //Apr�s un petit d�lai
        yield return new WaitForSeconds(delai);

        //D�truire l'ennemi sur r�seau
        PhotonNetwork.Destroy(PhotonView.Find(pvID));

        //Augmenter le compteur de mort
        compteurMort += 1;
    }
}