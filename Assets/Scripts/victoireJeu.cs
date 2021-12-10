using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Gestion de la victoire
 * 
 * Par : Tristan Lapointe
 * 
 * Dernière modification : 9 décembre 2021
 * 
*/
public class victoireJeu : MonoBehaviour
{
    public GameObject[] particulesDeVictoire; //Référence aux particules de feux d'artifice
    public GameObject texteVictoire; //Référence au texte de victoire
    public static bool victoireActive; //Condition pour pas que la fonction se réexécute
    public static bool boule1Active; //Condition pour savoir si la boule 1 est déposée
    public static bool boule2Active; //Condition pour savoir si la boule 2 est déposée
      
    //Appeler cette fonction de victoire lorsque les deux socket ont été select par leur boule respecter. Ensuite, le socket principal avctive la victoire
    public void Victoire()
    {
        //Indiquer que le premier socket a été activé
        if (gameObject.name == "Socket")
        {
            boule1Active = true;
        }
        //Indiquer que le premier socket a été activé
        else if (gameObject.name == "Socket2")
        {
            boule2Active = true;
        }
    }

    public void Update()
    {
        //Si le jeu n'a pas encore été gagné et que toutes les conditions sont rencontrées pour la victoire
        if (victoireActive == false && boule1Active && boule2Active && gameObject.name == "Socket")
        {
            //Activer les particules de victoire en boucle
            InvokeRepeating("particulesVictoire", 0f, 0.15f);

            //Activer le texte de victoire
            texteVictoire.SetActive(true);

            //Indiquer que la victoire est faite
            victoireActive = true;
        }
    }

    //Fonction qui gère l'apparition aléatoire des particules de victoire
    public void particulesVictoire()
    {
        //Piger un nombre aléatoire, activer la particule
        int randomEnfantActiver = Random.Range(0,particulesDeVictoire.Length);

        //Activer une particule alétoire dans le tableau
        particulesDeVictoire[randomEnfantActiver].gameObject.SetActive(true);

        //Piger un nombre aléatoire, désactiver la particule
        int randomEnfantDesactiver = Random.Range(0, particulesDeVictoire.Length);

        particulesDeVictoire[randomEnfantDesactiver].gameObject.SetActive(false);
    }
}
