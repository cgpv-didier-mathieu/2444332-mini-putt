public class EtatFrappe : EtatBalle
{
    public override void OnEntrer(Balle balle)
    {
        GestionnaireCameraJeu.Instance.ActiverCameraSuiviBalle(balle.transform);
    }

    public override EtatBalle OnExecuter(Balle balle)
    {
        if (balle.BasculeDemandee)
        {
            balle.ReinitialiserDemandeChangement();
            return new EtatParcourirTableau();
        }

        balle.FrapperBalle();

        if (balle.BalleFrappee)
        {
            return new EtatRoule();
        }

        return this;
    }

    public override void OnSortir(Balle balle)
    {

    }
}