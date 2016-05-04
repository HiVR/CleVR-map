// <copyright file="TreeGardenFactory.cs" company="CleVR B.V.">
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
    /// TreeGarden Factory create and holds all tree gardens
    /// </summary>
    public static class TreeGardenFactory
    {
        #region Fields

        /// <summary>
        /// The trees
        /// </summary>
        private static List<TreeGarden> trees = new List<TreeGarden>();

        /// <summary>
        /// The last tree identifier
        /// </summary>
        private static int lastTreeId = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets all trees.
        /// </summary>
        /// <value>
        /// All trees.
        /// </value>
        public static IEnumerable<TreeGarden> AllTrees
        {
            get
            {
                return trees;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the tree.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns>The created tree garden</returns>
        public static TreeGarden CreateTreeGarden(Transform transform)
        {
            TreeGarden tree = new TreeGarden(transform, lastTreeId++);
            trees.Add(tree);
            return tree;
        }

        #endregion Methods
    }
}