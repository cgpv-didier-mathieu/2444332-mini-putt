using UnityEngine;

public class Trou : MonoBehaviour
{
    [Header("Récompense & Réinitialisation")]
    [SerializeField, Tooltip("Point de départ où la balle sera téléportée")]
    private Transform pointDepart;

    [SerializeField, Tooltip("Distance à laquelle le drapeau disparaît")]
    private float distanceMasquerDrapeau = 3f;

    private Transform balleTransform;

    private Animator controleurAnimation;

    [Header("Gestion du Drapeau")]
    [SerializeField] private Animator animateurDrapeau;   // remplace le GameObject

    private bool balleProche = false;

    private void Update()
    {
        GérerVisibilitéDrapeau();
    }

    private void Awake()
    {
        controleurAnimation = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Détecter si l'objet qui entre dans le trigger est la balle
        if (other.CompareTag("Balle") || other.GetComponent<Rigidbody>() != null)
        {
            TeleporterBalle(other.gameObject);
        }
    }

    private void TeleporterBalle(GameObject balle)
    {
        // 2. Réinitialiser la vitesse physique de la balle
        Rigidbody rb = balle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 3. Téléporter la balle au point de départ
        if (pointDepart != null)
        {
            balle.transform.position = pointDepart.position;
            balle.transform.rotation = pointDepart.rotation;
        }

        // Réafficher le drapeau pour le prochain essai
        if (animateurDrapeau != null)
        {
            balleProche = false;
            animateurDrapeau.SetBool("BalleProche", false);
        }

        if (balle != null)
        {
            Balle scriptBalle = balle.GetComponent<Balle>();
            if (scriptBalle != null)
            {
                //scriptBalle.ReinitialiserTir();
            }
        }
    }

    private void GérerVisibilitéDrapeau()
    {
        if (animateurDrapeau == null)
            return;

        if (balleTransform == null)
        {
            GameObject balleObj = GameObject.FindWithTag("Balle");
            if (balleObj != null) balleTransform = balleObj.transform;
            return;
        }

        float distance = Vector3.Distance(transform.position, balleTransform.position);
        bool proche = distance <= distanceMasquerDrapeau;

        if (proche != balleProche)
        {
            balleProche = proche;
            animateurDrapeau.SetBool("BalleProche", proche);
        }
    }
}