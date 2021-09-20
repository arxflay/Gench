using System;

namespace Gench.Business
{
    public class ModelPartInfoCollisionExpection : Exception
    {
        public ModelPartInfoCollisionExpection(string text) : base(text)
        {

        }
    }
}