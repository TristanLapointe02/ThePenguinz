using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Gestion du son de victoire, associ� � la radio
 * 
 * Par : Tristan Lapointe
 * 
 * Derni�re modification : 9 d�cembre 2021
 * 
*/
public class radioVictoireSon : MonoBehaviour
{
    public AudioClip sonVictoire; //Son de victoire

    void Update()
    {
        //Si la victoire n'est pas d�j� activ�e et que les sockets des boules sont d�clench�s
        if (victoireJeu.boule1Active && victoireJeu.boule2Active)
        {
            //Jouer le son de victoire
            GetComponent<AudioSource>().PlayOneShot(sonVictoire, 3f);
        }
    }
}
