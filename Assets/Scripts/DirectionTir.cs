using UnityEngine;
using UnityEngine.InputSystem;


public class DirectionTir : MonoBehaviour
{
    [SerializeField, Tooltip("Le PlayerInput qui gère les actions de la personne qui joue")]
    private PlayerInput controles;

    [SerializeField, Tooltip("Vitesse de rotation de la flèche en degrés/seconde")]
    private float vitesseRotation = 90.0f;

    private float rotation;

    public Vector3 Direction => transform.right;

    private void Start()
    {
        InputAction actionOrienter = controles.actions.FindAction("Player/OrienterTir");
        actionOrienter.performed += CommencerRotation;
        actionOrienter.canceled += TerminerRotation;
    }

    private void OnDestroy()
    {
        if (controles == null || controles.actions == null)
            return;

        InputAction actionOrienter = controles.actions.FindAction("Player/OrienterTir");
        actionOrienter.performed -= CommencerRotation;
        actionOrienter.canceled -= TerminerRotation;
    }

    private void Update()
    {
        if (rotation != 0.0f)
        {
            transform.Rotate(Vector3.up, rotation * vitesseRotation * Time.deltaTime, Space.World);
        }
    }

    private void CommencerRotation(InputAction.CallbackContext contexte)
    {
        rotation = contexte.ReadValue<float>();
    }

    private void TerminerRotation(InputAction.CallbackContext contexte)
    {
        rotation = 0.0f;
    }
}