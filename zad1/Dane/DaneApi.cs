namespace Dane
{
    public class DaneApi : DaneAbstractApi
    {
        //ustawiamy tutaj parametry
        public override int WysokoscPlanszy { get; } = 200;

        public override int SzerokoscPlanszy { get; } = 350;

        public override int minSrednicaKuli { get; } = 12;

        public override int maxSrednicaKuli { get; } = 38;

        public override float predkosc { get; } = 20f;
    }
}