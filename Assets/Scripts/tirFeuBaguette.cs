using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit; // acc�s aux objets du XR Interaction Toolkit
using UnityEngine.InputSystem; // pour utiliser le nouveau InputSyteme
using UnityEngine.Events;

public class tirFeuBaguette : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //V�rifie si le joueur peut faire enflammer la baguette
    public static GameObject particuleFeu; //Particule de feu
    public static GameObject colliderFeu; //Collider du feu. C'est ce qui va toucher � l'ennemi
    public static Transform boutBaguette; //D�termine le bout de la baguette
    public bool feuActif; //D�termine quand le feu est actif
    public AudioClip feuSon; //Son du feu

    [SerializeField]
    InputActionReference inputActionReference_ActiveTrigger;

    public override void OnEnable()
    {
        inputActionReference_ActiveTrigger.action.performed += ActiveTrigger;
    }

    public override void OnDisable()
    {
        // s'ex�cute lorsque le script devient inactif (disable)
        // retire la fonction qui sera appel�e lorsque l'action sera effectu�e
        inputActionReference_ActiveTrigger.action.performed -= ActiveTrigger;
    }

    void Update()
    {

    }
    private void ActiveTrigger(InputAction.CallbackContext obj)
    {
        // Bouton enfonc�, on active le rayon
        // ReadValue<float> permet de r�cuper� la valeur de type float contenu dans le param�tre obj
        if (obj.ReadValue<float>() == 1f && boutBaguette != null)
        {
            if (photonView.IsMine)
            {
                //Activer la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(true);

                //Indiquer que le joueur a activ� le feu
                feuActif = true;

                //Commencer la coroutine de la baguette de feu
                IEnumerator coroutine = test(0.5f);
                StartCoroutine(coroutine);

                //Faire jouer le son sur r�seau
                photonView.RPC("JoueSonFeu", RpcTarget.All);
            }
        }

        if (obj.ReadValue<float>() < 1f && boutBaguette != null)
        {           
            if (photonView.IsMine)
            {
                //D�sactiver la particule de feu dans la direction du joueur
                particuleFeu.gameObject.SetActive(false);

                //D�sactiver le collider du feu
                colliderFeu.gameObject.SetActive(false);

                //Indiquer que le joueur a d�sactiv� le feu
                feuActif = false;

                //Arr�ter le son su r�seau
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

            //D�sactiver le collider de feu
            colliderFeu.SetActive(false);
        }
    }
}
