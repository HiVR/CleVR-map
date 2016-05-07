// <copyright file="TreeGardenViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.ViewModel
{
    using Assets.Model;
    using Assets.Model.Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Tree Garden view model serializes the seats list
    /// </summary>
    /// <seealso cref="Assets.ViewModel.StaticObjectViewModel{Assets.Model.TreeGarden}" />
    public class TreeGardenViewModel : StaticObjectViewModel<Model.TreeGarden>
    {
        #region Fields

        /// <summary>
        /// The seats
        /// </summary>
        [SerializeField]
        private List<GameObject> seats = new List<GameObject>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Creates the model layer.
        /// </summary>
        /// <param name="transform">The transform of the GameObject.</param>
        /// <returns>
        /// Created model
        /// </returns>
        protected override TreeGarden CreateModel(Transform transform)
        {
            TreeGarden tree = TreeGardenFactory.CreateTreeGarden(transform);
            for (int index = 0; index < this.seats.Count; index++)
            {
                tree.AddSeat(this.seats[index].transform);
            }

            return tree;
        }

        #endregion Methods
    }
}