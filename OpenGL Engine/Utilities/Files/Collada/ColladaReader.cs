using System;
using System.Collections.Generic;
using System.Xml;

namespace OpenEngine
{
    public static class ColladaReader
    {

        #region FIELDS

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public static void LoadCollada(string collada, int maxWeights = 3)
        {
            XmlFile file = new XmlFile(collada);
            SkinLoader skinLoader = new SkinLoader(file.GetNode("/library_controllers"), maxWeights);

            //SkeletonLoader jointsLoader = new SkeletonLoader(document.GetElementsByTagName("library_visual_scenes")[0]);

            //GeometryLoader g = new GeometryLoader(document.GetElementsByTagName("library_geometries")[0]);

        }

        #endregion

        #region PRIVATE METHODS

        #endregion

    }
}
