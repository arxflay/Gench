using System;
using MersenneTwister;

namespace Gench.Business
{
    public class ModelGeneratorBase
    {
        protected Random _prngGenerator;
        protected ModelPartLoader _partLoader;
        protected ColorTargetLoader _colorTargetLoader;
        protected SvgLoader _svgLoader;
        protected const string MainTargetColorName = "MainTarget";

        public ModelGeneratorBase()
        {
            _partLoader = new ModelPartLoader();
            _colorTargetLoader = new ColorTargetLoader();
            _prngGenerator = MT64Random.Create();
            _svgLoader = new SvgLoader();
        }

        protected byte GenerateByte(byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return (byte)_prngGenerator.Next(min, max);
        }
    }
}