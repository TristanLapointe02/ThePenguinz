using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
/*
 * Gestion de l'avatar réseau (désactiver et activer certains componnent comme le body ou le XR rig).
 * Gestion du son aléatoire provenant du pinguin
 * 
 * Par : Jérémy Émond-Lapierre
 * 
 * Dernière modification : 20 novembre 2021
 * 
*/

public class GestionAvatarReseau : MonoBehaviourPunCallbacks
{
    public GameObject XRRig; //Référence au Rig
    public GameObject Avatar; //Référence au corps de l'avatar

    //Différents sons des pengouins
    public AudioClip pengouin1;
    public AudioClip pengouin2;
    public AudioClip pengouin3;

    void Start()
    {
        //Appeler la fonction jouant les sons aléatoires de pinguins après 6 secondes
        Invoke("JouerSonPengouin", 6f);

        if (photonView.IsMine)
        {
            XRRig.SetActive(true);  //XRRig controle l'avatar local
            Avatar.SetActive(false); //le modèle Avatar n'est pas visible pour le joueur local 
            //Correction de bug de téléportation, XRRig n'est pas dans la scène de début
            //Alors il faut l'identifier aux objets "Teleportation Area" par script
            TeleportationArea[] teleportPlacher = GameObject.FindObjectsOfType<TeleportationArea>();
            foreach (var plancher in teleportPlacher)
            {
                plancher.teleportationProvider = XRRig.GetComponent<TeleportationProvider>();
            }
        }
        else
        {
            //Sinon, désactiver le rig
            XRRig.SetActive(false);
        }
    }

    //Fonction qui joue un son aléatoire de pinguin
    void JouerSonPengouin(){
        if(photonView.IsMine){
            //Variable pour déclarer après combien de temps on re-invoke la fonction
            float randomTemps = Random.Range(6, 18);
            //Choisir le son aléatoire qui doit jouer
            float son = Random.Range(1, 3);
            //Premier son...
            if (PhotonNetwork.PlayerList.Length > 1)
            {
                if (son == 1)
                {
                    photonView.RPC("JouerSonPengouin1", RpcTarget.Others);
                    //GetComponent<AudioSource>().PlayOneShot(pengouin1, 0.7f);
                }
                //Deuxième son...
                else if (son == 2)
                {
                    photonView.RPC("JouerSonPengouin2", RpcTarget.Others);
                    //GetComponent<AudioSource>().PlayOneShot(pengouin2, 0.7f);
                }
                //Troisième son...
                else
                {
                    photonView.RPC("JouerSonPengouin3", RpcTarget.Others);
                    //GetComponent<AudioSource>().PlayOneShot(pengouin3, 0.7f);
                }
            }
            else
            {
                if (son == 1)
                {
                    photonView.RPC("JouerSonPengouin1", PhotonNetwork.LocalPlayer);
                    //GetComponent<AudioSource>().PlayOneShot(pengouin1, 0.7f);
                }
                //Deuxième son...
                else if (son == 2)
                {
                    photonView.RPC("JouerSonPengouin2", PhotonNetwork.LocalPlayer);
                    //GetComponent<AudioSource>().PlayOneShot(pengouin2, 0.7f);
                }
                //Troisième son...
                else
                {
                    photonView.RPC("JouerSonPengouin3", PhotonNetwork.LocalPlayer);
                    //GetComponent<AudioSource>().PlayOneShot(pengouin3, 0.7f);
                }
            }  
              
            //Réinvoker la fonction
            Invoke("JouerSonPengouin", randomTemps);
        }  
    }

    //Jouer le premier son
    [PunRPC]
    void JouerSonPengouin1()
     {
         GetComponent<AudioSource>().PlayOneShot(pengouin1, 0.7f);
     }

    //Jouer le deuxième son
    [PunRPC]
     void JouerSonPengouin2()
     {
         GetComponent<AudioSource>().PlayOneShot(pengouin2, 0.7f);
     }

    //Jouer le deuxième son
    [PunRPC]
     void JouerSonPengouin3()
     {
         GetComponent<AudioSource>().PlayOneShot(pengouin3, 0.7f);
     }
}


