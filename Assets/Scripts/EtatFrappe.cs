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

        return this;
    }

    public override void OnSortir(Balle balle)
    {

    }
}