using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
/*
 * Gestion de la victoire
 * 
 * Par : Tristan Lapointe
 * 
 * Derni�re modification : 9 d�cembre 2021
 * 
*/
public class victoireJeu : MonoBehaviourPunCallbacks
{
    public GameObject[] particulesDeVictoire; //R�f�rence aux particules de feux d'artifice
    public GameObject texteVictoire; //R�f�rence au texte de victoire
    public static bool victoireActive; //Condition pour pas que la fonction se r�ex�cute
    public static bool boule1Active; //Condition pour savoir si la boule 1 est d�pos�e
    public static bool boule2Active; //Condition pour savoir si la boule 2 est d�pos�e
      
    //Appeler cette fonction de victoire lorsque les deux socket ont �t� select par leur boule respecter. Ensuite, le socket principal avctive la victoire
    public void Victoire()
    {
        //Indiquer que le premier socket a �t� activ�
        if (gameObject.name == "Socket")
        {
            boule1Active = true;
        }
        //Indiquer que le premier socket a �t� activ�
        else if (gameObject.name == "Socket2")
        {
            boule2Active = true;
        }
    }

    public void Update()
    {
        //Si le jeu n'a pas encore �t� gagn� et que toutes les conditions sont rencontr�es pour la victoire
        if (victoireActive == false && boule1Active && boule2Active && gameObject.name == "Socket")
        {
            //Activer les particules de victoire en boucle
            InvokeRepeating("particulesVictoire", 0f, 0.15f);

            //Activer le texte de victoire
            texteVictoire.SetActive(true);

            //Indiquer que la victoire est faite
            victoireActive = true;

            //Appeler la fonction pour téléporter le joueur
            Invoke("teleportationJoueur", 15f);
        }
    }

    //Fonction qui g�re l'apparition al�atoire des particules de victoire
    public void particulesVictoire()
    {
        //Piger un nombre al�atoire, activer la particule
        int randomEnfantActiver = Random.Range(0,particulesDeVictoire.Length);

        //Activer une particule al�toire dans le tableau
        particulesDeVictoire[randomEnfantActiver].gameObject.SetActive(true);

        //Piger un nombre al�atoire, d�sactiver la particule
        int randomEnfantDesactiver = Random.Range(0, particulesDeVictoire.Length);

        particulesDeVictoire[randomEnfantDesactiver].gameObject.SetActive(false);
    }

    //Fonction qui téléporte les joueur lors de la défaite
    public void teleportationJoueur()
    {
        PhotonNetwork.LoadLevel("Penguinz_Fin");
    }
}
