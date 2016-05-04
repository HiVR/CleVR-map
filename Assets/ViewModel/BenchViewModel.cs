// <copyright file="BenchViewModel.cs" company="CleVR B.V.">
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
    /// Bench view model
    /// </summary>
    /// <seealso cref="Assets.ViewModel.StaticObjectViewModel{Assets.Model.Bench}" />
    public class BenchViewModel : StaticObjectViewModel<Bench>
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
        protected override Bench CreateModel(Transform transform)
        {
            Bench bench = BenchFactory.CreateBench(transform);
            for (int index = 0; index < this.seats.Count; index++)
            {
                bench.AddSeat(this.seats[index].transform);
            }

            return bench;
        }

        #endregion Methods
    }
}