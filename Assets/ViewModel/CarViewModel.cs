// <copyright file="CarViewModel.cs" company="CleVR B.V.">
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
    /// Car ViewModel
    /// </summary>
    public sealed class CarViewModel : StaticObjectViewModel<Car>
    {
        #region Fields

        /// <summary>
        /// The car colors
        /// </summary>
        private static List<Color> carColors;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the car colors.
        /// </summary>
        /// <value>
        /// The car colors.
        /// </value>
        private static List<Color> CarColors
        {
            get
            {
                if (carColors == null)
                {
                    carColors = new List<Color>();
                    carColors.Add(Color.magenta);
                    carColors.Add(Color.blue);
                    carColors.Add(Color.yellow);
                    carColors.Add(Color.red);
                }

                return carColors;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the model layer.
        /// </summary>
        /// <param name="transform">The transform of the GameObject.</param>
        /// <returns>
        /// Created model
        /// </returns>
        protected override Car CreateModel(Transform transform)
        {
            return CarFactory.CreateCar(transform);
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Start()
        {
            Transform model = this.transform.FindChild("Model");

            if (model != null)
            {
                Renderer renderer = model.gameObject.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.material.color = CarColors[((int)this.Observed.Type) - 1];
                }
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            this.Observed.Update();
        }

        #endregion Methods
    }
}