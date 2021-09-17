using System;
using MersenneTwister;
using System.Collections.Generic;
using System.Xml.Linq;
using Gench.Loaders;

namespace Gench.Business
{
    public class ModelGeneratorBase
    {
        protected Random _prngGenerator;
        protected Loader<ModelPart> _partLoader;
        protected Loader<ColorTarget> _colorTargetLoader;
        protected ModelSvgLoader _svgLoader;
        protected const string MainTargetColorName = "MainTarget";

        public ModelGeneratorBase()
        {
            _partLoader = new Loader<ModelPart>();
            _colorTargetLoader = new Loader<ColorTarget>();
            _prngGenerator = MT64Random.Create();
            _svgLoader = new ModelSvgLoader();
        }

        protected byte GenerateByte(byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return (byte)_prngGenerator.Next(min, max);
        }

        protected void RestoreDocumentAttributes(List<ColorTarget> targets, 
                                                 Dictionary<string, XAttribute[]> sortedAttrByKey)
        {
            foreach (var target in targets)
            {
                foreach(var attr in sortedAttrByKey[target.Name])
                {
                    attr.Value = target.Color;
                }
            }
        }
    }
}