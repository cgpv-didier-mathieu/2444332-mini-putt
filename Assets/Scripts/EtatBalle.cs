/// <summary>
/// Classe de base pour définir un état de l'objet Arbre.
/// </summary>
public abstract class EtatBalle
{

    /// <summary>
    /// Effectue des traitements dans la première frame où l'objet arbre est dans cet état.
    /// </summary>
    /// <param name="arbre">L'arbre qui entre dans l'état.</param>
    public virtual void OnEntrer(Balle balle)
    {
        // Opérations pour mettre à jour d'arbre afin qu'il s'exécute dans l'état définit.
        // Par exemple : démarrer une animation, jouer un son, trouver la référence d'un autre objet, 
        // s'abonner à des événements...
    }

    /// <summary>
    /// Effectue des traitements pour chacune des frames où l'objet arbre est dans cet état.
    /// </summary>
    /// <param name="arbre">L'arbre qui est dans cet état.</param>
    /// <return>L'état de l'arbre à la prochaine frame.</return>
    public virtual EtatBalle OnExecuter(Balle balle)
    {
        // Opérations pour mettre à jour arbre afin qu'il s'exécute dans l'état définit.
        // Par exemple : mettre à jour des variables, déplacer l'objet, mettre à jour des références...

        // On vérifie aussi dans cette méthode si les conditions pour changer d'état sont rencontrées.
        // La méthode retourne l'état de l'objet lors de la prochaine frame. Si l'état ne change pas, 
        // il est important de retourner this.
        return this;
    }

    /// <summary>
    /// Effectue des traitements dans la dernière frame où l'arbre est dans cet état.
    /// </summary>
    /// <param name="arbre">L'arbre qui sort dans l'état.</param>
    public virtual void OnSortir(Balle balle)
    {
        // Opérations pour mettre à jour arbre afin qu'il cesse d'être dans l'état définit.
        // Par exemple : arrêter une animation, arrêter un son, se désabonner d'événements...
    }

}