namespace Gench.Business
{
    public struct PartPosition
    {
        PartPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X {get;}
        public double Y {get;}
    }
}