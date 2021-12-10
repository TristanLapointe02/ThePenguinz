using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
/*
 * Script aux ennemis permettant de contrôler leur mouvement et leurs vie.
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 20 novembre 2021
 * 
*/
public class deplacementEnnemi : MonoBehaviourPunCallbacks
{
    NavMeshAgent navAgent; //Raccourci pour la navmesh agent
    public GameObject totem; //Déclaration de  la référence du gameObject du collider du totem
    public float vieEnnemi; //Vie de l'ennemi
    public bool mort; //Variable d�tectant la mort de l'ennemi
    public static int compteurMort = 0; //Compteur de mort
    bool enVie = true; //Déterminer si l'ennemi est en vie
    public AudioClip sonEpee; //Son de l'épée
    public bool frappeEpee; //Booléenne pour empêcher la son de l'épée qui joue en boucle
    public AudioClip sonBalle; //Son de la balle qui touche l'ennemi
    public GameObject[] tentes; //Tentes dans le jeu
    public AudioClip sonHit; //Son lorsqu'un ennemi touche le totem

    void Start()
    {
        //Référence aux tentes du jeu. Sert pour plus tard...
        tentes = GameObject.FindGameObjectsWithTag("tentes");

        //Référence au totem du jeu. Sert pour plus tard...
        totem = GameObject.Find("TotemCentre");

        //Défénir la vie de l'ennemi et du boss
        if (gameObject.name == "Ennemi(Clone)")
        {
            vieEnnemi = 100f;
        }

        else if (gameObject.name == "Boss(Clone)")
        {
            vieEnnemi = 350f;
        }

        //Aller chercher le raccourci pour navmesh agent
        navAgent = GetComponent<NavMeshAgent>();

        //Diriger la destination de l'ennemi au Totem si le jeu n'est pas perdu
        if (TotemVie.defaite == false)
        {
            navAgent.SetDestination(totem.transform.position);
        }
    }

    void Update()
    {
        //MORT DE L'ENNEMI
        if (vieEnnemi <= 0 && enVie)
        {
            //Signaler qu'il est mort et signaler qu'il n'est plus en vie
            mort = true;
            enVie = false;

            //Activer l'animation de mort
            GetComponent<Animator>().SetBool("Mort", true);

            //Augmenter le compteur de mort
            compteurMort += 1;

            //D�truire l'ennemi sur r�seau
            if (PhotonNetwork.IsMasterClient)
            {
                //Trouver le photon view et l'envoyer au RPC pour indiquer à tous les joueurs que l'ennemi est détruit
                int pvID = gameObject.GetComponent<PhotonView>().ViewID;
                photonView.RPC("MortEnnemi", RpcTarget.MasterClient, pvID, 3);
            }

            //Il reste en place
            navAgent.SetDestination(transform.position);

            //..Si l'ennemi tué était le boss
            if (gameObject.name == "Boss(Clone)")
            {
                //Faire spawn les boules a neige sur réseau
                if (PhotonNetwork.IsMasterClient == true)
                {
                    //La première boule
                    PhotonNetwork.InstantiateRoomObject("boule", gameObject.transform.position + transform.up * 2, Quaternion.identity, 0, null);

                    //La deuxième boule
                    PhotonNetwork.InstantiateRoomObject("boule2", gameObject.transform.position + transform.up * 2, Quaternion.identity, 0, null);
                }
            }
        }

        //VICTOIRE DE L'ENNEMI
        if (TotemVie.defaite == true && enVie)
        {
            //Activer l'animation de victoire
            GetComponent<Animator>().SetBool("Victoire", true);
        }
    }

    //Vérifier les collisions...
    IEnumerator OnTriggerEnter(Collider collision)
    {
        //Si l'ennemi touche une épée
        if (collision.gameObject.name == "Sword(Clone)" && detectionEpee.epeePrise == true)
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 100f;

            //S'assurer, avec une booléenne, que le son se fait juste une fois
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
            vieEnnemi -= 45f;

            //Appeler la fonction pour le son de la balle qui touche l'ennemi
            photonView.RPC("JoueSonBalle", RpcTarget.All);
        }

        //Si l'ennemi se fait exploser par une grenade
        if (collision.gameObject.name == "collisionGrenade")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 175f; 
        }

        //Si l'ennemi se fait toucher par le feu de la baguette magique
        if (collision.gameObject.name == "ColliderFeu")
        {
            //Diminuer la vie de l'ennemi
            vieEnnemi -= 25f;

            //Appeler la fonction pour le son du feu qui touche l'ennemi
            photonView.RPC("JoueSonBalle", RpcTarget.All);
        }

        //Si l'ennemi touche un totem
        if (collision.gameObject.name == "TotemCentre" && enVie && mort==false)
        {
            //Après 10 secondes, retourner vers le totem
            Invoke("directionTotem", 10f);

            //Activer l'animation d'attaque
            GetComponent<Animator>().SetTrigger("Attaque");

            //Attendre un peu pour synchroniser le son avec le coup
            yield return new WaitForSeconds(0.5f);

            //Jouer le son de hit s'il est encore en vie
            if (enVie && mort == false)
            {
                GetComponent<AudioSource>().PlayOneShot(sonHit);
            }

            //Attendre un peu pour lui laisser le temps de finir son animation
            yield return new WaitForSeconds(1f);

            //Si il est encore en vie
            if (enVie && mort == false)
            {
                //Le rediriger vers une tente aléatoire
                navAgent.SetDestination(tentes[Random.Range(0, tentes.Length)].transform.position);
            } 
        }
    }

    //Fonction qui permet de rappeler le son de l'épée
    public void ActiverSonEpee()
    {
        frappeEpee = false;
    }

    //Fonction qui redirige l'ennemi vers le totem
    void directionTotem()
    {
        //S'il est en vie et que la partie est pas finie
        if (mort == false && enVie && TotemVie.defaite == false)
        {
            //Rediriger l'ennemi vers le totem
            navAgent.SetDestination(totem.transform.position);
        }
    }

    //Fonction qui détruit l'ennemi
    [PunRPC]
    IEnumerator MortEnnemi(int pvID, int delai)
    {
        //Apr�s un petit d�lai
        yield return new WaitForSeconds(delai);

        //D�truire l'ennemi sur r�seau
        PhotonNetwork.Destroy(PhotonView.Find(pvID));          
    }

    //Jouer le son de l'épée
    [PunRPC]
    void JoueSonEpee()
    {
        GetComponent<AudioSource>().PlayOneShot(sonEpee);
    }

    //Jouer le son de la balle (même que le feu)
    [PunRPC]
    void JoueSonBalle()
    {
        GetComponent<AudioSource>().PlayOneShot(sonBalle);
    }
}