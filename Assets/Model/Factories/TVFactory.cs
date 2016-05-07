// <copyright file="TVFactory.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.Model.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// TV Factory create and holds all TV's
    /// </summary>
    public static class TVFactory
    {
        #region Fields

        /// <summary>
        /// all the created TV's
        /// </summary>
        private static List<TV> tvs = new List<TV>();

        /// <summary>
        /// The last TV identifier
        /// </summary>
        private static int lastTVId = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets all TVS.
        /// </summary>
        /// <value>
        /// All TVS.
        /// </value>
        public static IEnumerable<TV> AllTVS
        {
            get
            {
                return tvs;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the TV.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns>The created TV</returns>
        public static TV CreateTV(Transform transform)
        {
            TV tv = new TV(transform, lastTVId++);
            tvs.Add(tv);
            return tv;
        }

        #endregion Methods

        /// <summary>
        /// Gets the random TV.
        /// </summary>
        /// <returns>random TV</returns>
        public static TV GetRandomTV()
        {
            if (tvs.Count > 0)
            {
                return tvs[UnityEngine.Random.Range(0, tvs.Count - 1)];
            }

            return null;
        }
    }
}