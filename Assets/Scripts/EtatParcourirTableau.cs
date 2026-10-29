public class EtatParcourirTableau : EtatBalle
{

    public override void OnEntrer(Balle balle)
    {
        GestionnaireCameraJeu.Instance.ActiverCameraLibre();
    }

    public override EtatBalle OnExecuter(Balle balle)
    {
        if (balle.BasculeDemandee)
        {
            balle.ReinitialiserDemandeChangement();
            return new EtatFrappe();
        }

        return this;
    }

    public override void OnSortir(Balle balle)
    {

    }
}