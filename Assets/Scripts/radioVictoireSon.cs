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
    public bool sonEnCours; //Bool pour ne pas que le son se joue en boucle

    void Update()
    {
        //Si la victoire n'est pas déjà activée et que les sockets des boules sont déclenchés
        if (sonEnCours == false && victoireJeu.boule1Active && victoireJeu.boule2Active)
        {
            //Jouer le son de victoire
            GetComponent<AudioSource>().PlayOneShot(sonVictoire, 3f);

            //Indiquer au son de ne pas rejouer avec une bool...
            sonEnCours = true;
        }
    }
}
