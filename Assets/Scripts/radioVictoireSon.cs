using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Gestion du son de victoire, associé à la radio
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 9 décembre 2021
 * 
*/
public class radioVictoireSon : MonoBehaviour
{
    public AudioClip sonVictoire; //Son de victoire

    void Update()
    {
        //Si la victoire n'est pas déjà activée et que les sockets des boules sont déclenchés
        if (victoireJeu.boule1Active && victoireJeu.boule2Active)
        {
            //Jouer le son de victoire
            GetComponent<AudioSource>().PlayOneShot(sonVictoire, 3f);
        }
    }
}
