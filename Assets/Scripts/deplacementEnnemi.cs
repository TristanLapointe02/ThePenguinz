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
    public AudioClip sonEpee; //Son de l'épée
    public bool frappeEpee; //Booléenne pour empêcher la son de l'épée qui joue en boucle
    public AudioClip sonBalle; //Son de la balle qui touche l'ennemi

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

            //Indiquer qu'il n'est plus en vie
            enVie = false;

            //Activer l'animation de mort
            GetComponent<Animator>().SetBool("Mort", true);

            //Appeler la fonction qui joue le son de mort en RPC pour tous
            //photonView.RPC("JoueSonMort", RpcTarget.All);

            //Augmenter le compteur de mort
            compteurMort += 1;

            //D�truire l'ennemi sur r�seau
            if (PhotonNetwork.IsMasterClient)
            {
                int pvID = gameObject.GetComponent<PhotonView>().ViewID;

                photonView.RPC("MortEnnemi", RpcTarget.MasterClient, pvID, 3);
            }

            //Arr�ter l'ennemi pour pas qu'il poursuit son chemin
            GetComponent<NavMeshAgent>().enabled = false;

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

            if (frappeEpee == false){
                //Appeler la fonction pour le son de l'épée qui touche l'ennemi
                photonView.RPC("JoueSonEpee", RpcTarget.All);

                //Remettre la variable à true après un petit délai
                Invoke("ActiverSonEpee", 1f);

                //Mettre la frappe à true
                frappeEpee = true;
            }
        }

        //Si l'ennemi se fait toucher par une balle
        if (collision.gameObject.tag == "Balle")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 50f;

            //Appeler la fonction pour le son de la balle qui touche l'ennemi
            photonView.RPC("JoueSonBalle", RpcTarget.All);
        }

        //Si l'ennemi se fait exploser fatalement
        if (collision.gameObject.name == "collisionGrenade")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 300f; 
        }
    }

    //Fonction qui permet de rappeler le son de l'épée
    public void ActiverSonEpee()
    {
        frappeEpee = false;
    }

    //Fonction qui détruit l'ennemi
    [PunRPC]
    IEnumerator MortEnnemi(int pvID, int delai)
    {
        if (enVie)
        {
            //Apr�s un petit d�lai
            yield return new WaitForSeconds(delai);

            //D�truire l'ennemi sur r�seau
            PhotonNetwork.Destroy(PhotonView.Find(pvID));         
        } 
    }

    [PunRPC]
    void JoueSonEpee()
    {
        GetComponent<AudioSource>().PlayOneShot(sonEpee);
    }

    [PunRPC]
    void JoueSonBalle()
    {
        GetComponent<AudioSource>().PlayOneShot(sonBalle);
    }
}