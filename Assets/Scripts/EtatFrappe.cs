public class EtatFrappe : EtatBalle
{
    public override void OnEntrer(Balle balle)
    {

    }

    public override EtatBalle OnExecuter(Balle balle)
    {
        return this;
    }

    public override void OnSortir(Balle balle)
    {

    }
}