using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit; // accès aux objets du XR Interaction Toolkit
using UnityEngine.InputSystem; // pour utiliser le nouveau InputSyteme
using UnityEngine.Events;

public class tirFeuBaguette : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //Vérifie si le joueur peut faire enflammer la baguette
    public static GameObject particuleFeu; //Particule de feu
    public static GameObject colliderFeu; //Collider du feu. C'est ce qui va toucher à l'ennemi
    public static Transform boutBaguette; //Détermine le bout de la baguette
    public bool feuActif; //Détermine quand le feu est actif
    public AudioClip feuSon; //Son du feu

    [SerializeField]
    InputActionReference inputActionReference_ActiveTrigger;

    public override void OnEnable()
    {
        inputActionReference_ActiveTrigger.action.performed += ActiveTrigger;
    }

    public override void OnDisable()
    {
        // s'exécute lorsque le script devient inactif (disable)
        // retire la fonction qui sera appelée lorsque l'action sera effectuée
        inputActionReference_ActiveTrigger.action.performed -= ActiveTrigger;
    }

    void Update()
    {

    }
    private void ActiveTrigger(InputAction.CallbackContext obj)
    {
        // Bouton enfoncé, on active le rayon
        // ReadValue<float> permet de récuperé la valeur de type float contenu dans le paramètre obj
        if (obj.ReadValue<float>() == 1f && boutBaguette != null)
        {
            if (photonView.IsMine)
            {
                //Activer la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(true);

                //Indiquer que le joueur a activé le feu
                feuActif = true;

                //Commencer la coroutine de la baguette de feu
                IEnumerator coroutine = test(0.5f);
                StartCoroutine(coroutine);

                //Faire jouer le son sur réseau
                photonView.RPC("JoueSonFeu", RpcTarget.All);
            }
        }

        if (obj.ReadValue<float>() < 1f && boutBaguette != null)
        {           
            if (photonView.IsMine)
            {
                //Désactiver la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(false);

                //Désactiver le collider du feu
                colliderFeu.gameObject.SetActive(false);

                //Indiquer que le joueur a désactivé le feu
                feuActif = false;

                //Arrêter le son su réseau
                photonView.RPC("StopSonFeu", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void JoueSonFeu()
    {
        GetComponent<AudioSource>().PlayOneShot(feuSon);
    }

    [PunRPC]
    void StopSonFeu()
    {
        GetComponent<AudioSource>().Stop();
    }

    IEnumerator test(float waitTime)
    {
        //Quand le feu est actif
        while(feuActif == true && colliderFeu != null)
        {
            //Activer le collider de feu
            colliderFeu.SetActive(true);

            //Attendre un petit peu
            yield return new WaitForSeconds(waitTime);

            //Désactiver le collider de feu
            colliderFeu.SetActive(false);
        }
    }
}
