namespace Gench.Business
{
    public struct PartPosition
    {
        public PartPosition(double x = 0.0, double y = 0.0)
        {
            X = x;
            Y = y;
        }

        public double X {get;}
        public double Y {get;}
    }
}