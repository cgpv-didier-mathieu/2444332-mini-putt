public class EtatRoule : EtatBalle
{
    public override void OnEntrer(Balle balle)
    {
        GestionnaireCameraJeu.Instance.ActiverCameraSuiviBalle(balle.transform);
        balle.ReinitialiserDemandeChangement();
    }

    public override EtatBalle OnExecuter(Balle balle)
    {
        balle.ReinitialiserDemandeChangement();

        if (balle.BalleArretee)
        {
            balle.ReinitialiserTir();
            return new EtatParcourirTableau();
        }

        return this;
    }

    public override void OnSortir(Balle balle)
    {

    }
}