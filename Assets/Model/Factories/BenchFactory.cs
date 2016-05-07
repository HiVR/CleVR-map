// <copyright file="BenchFactory.cs" company="CleVR B.V.">
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
    /// Bench Factory creates and holds all Benches
    /// </summary>
    public static class BenchFactory
    {
        #region Fields

        /// <summary>
        /// all the created benches
        /// </summary>
        private static List<Bench> benches = new List<Bench>();

        /// <summary>
        /// The last bench identifier
        /// </summary>
        private static int lastBenchId = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets all benches.
        /// </summary>
        /// <value>
        /// All benches.
        /// </value>
        public static IEnumerable<Bench> AllBenches
        {
            get
            {
                return benches;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the bench.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns>The created bench</returns>
        public static Bench CreateBench(Transform transform)
        {
            Bench bench = new Bench(transform, lastBenchId++);
            benches.Add(bench);
            return bench;
        }

        #endregion Methods
    }
}