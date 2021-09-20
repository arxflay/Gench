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
        protected Loader<ModelPartInfo> _partInfoLoader;
        protected Loader<ColorTarget> _colorTargetLoader;
        protected ModelPartSvgLoader _modelPartSvgLoader;
        protected ModelGeneratorValidator _validator;
        protected ModelSvgGluer _gluer;
        
        protected byte GenerateByte(byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return (byte)_prngGenerator.Next(min, max);
        }

        protected void RestoreAttributes(IEnumerable<ColorTarget> targets, 
                                         Dictionary<string, ColorTargetScannerMatch[]> sortedAttrByKey)
        {
            foreach (var target in targets)
            {
                if (!sortedAttrByKey.ContainsKey(target.OwnerGroup))
                    continue;

                foreach(var attr in sortedAttrByKey[target.OwnerGroup])
                {
                    attr.Attribute.Value = target.TargetColor;
                }
            }
        }
        
        public ModelGeneratorBase()
        {
            _prngGenerator = MT64Random.Create();
            _partInfoLoader = new Loader<ModelPartInfo>();
            _colorTargetLoader = new Loader<ColorTarget>();
            _modelPartSvgLoader = new ModelPartSvgLoader();
            _validator = new ModelGeneratorValidator();
            _gluer = new ModelSvgGluer();
        }
    }
}