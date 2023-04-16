namespace Dane
{
    public class DaneApi : DaneAbstractApi
    {
        //ustawiamy tutaj parametry
        public override int WysokoscPlanszy { get; } = 200;

        public override int SzerokoscPlanszy { get; } = 350;

        public override int SrednicaKuli { get; } = 20;
    }
}