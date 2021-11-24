//**********************************************************************
//Gestion de l'avatar réseau (désactiver et activer certains componnent comme le body ou le XR rig)
//@The Penguinz
//2021-11-10
//**********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class GestionAvatarReseau : MonoBehaviourPunCallbacks
{
    public GameObject XRRig;
    public GameObject Avatar;
    //Différents sons des pengouins
    public AudioClip pengouin1;
    public AudioClip pengouin2;
    public AudioClip pengouin3;

    void Start()
    {
        Invoke("JouerSonPengouin", 6f);

        if (photonView.IsMine)
        {
            XRRig.SetActive(true);      //XRRig controle l'avatar local
            Avatar.SetActive(false);    //le model Avatar n'est pas visible pour le joueur local 
            //correction de bug de téléportation, XRRig n'est pas dans la scène de début
            //alors il faut l'identifier aux objets "Teleportation Area" par script
            TeleportationArea[] teleportPlacher = GameObject.FindObjectsOfType<TeleportationArea>();
            foreach (var plancher in teleportPlacher)
            {
                plancher.teleportationProvider = XRRig.GetComponent<TeleportationProvider>();
            }
        }
        else
        {
            XRRig.SetActive(false);
        }
    }

    void JouerSonPengouin(){
        if(photonView.IsMine){
            //Variable pour déclarer après combien de temps on re-invoke la fonction
            float randomTemps = Random.Range(6, 18);
            //Choisir le son aléatoire qui doit jouer
            float son = Random.Range(1, 3);
            //Premier son...
            if(son == 1){
                photonView.RPC("JouerSonPengouin1", RpcTarget.All);
            }
            //Deuxième son...
            else if(son == 2){
                photonView.RPC("JouerSonPengouin2", RpcTarget.All);
            }
            //Troisième son...
            else{
                photonView.RPC("JouerSonPengouin3", RpcTarget.All);
            }
            //Réinvoker la fonction
            Invoke("JouerSonPengouin", randomTemps);
        }
        
    }
    [PunRPC]
    void JouerSonPengouin1()
    {
        GetComponent<AudioSource>().PlayOneShot(pengouin1, 0.7f);
    }
    [PunRPC]
    void JouerSonPengouin2()
    {
        GetComponent<AudioSource>().PlayOneShot(pengouin2, 0.7f);
    }
    [PunRPC]
    void JouerSonPengouin3()
    {
        GetComponent<AudioSource>().PlayOneShot(pengouin3, 0.7f);
    }
}


