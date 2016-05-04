// <copyright file="CarFactory.cs" company="CleVR B.V.">
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
    /// Car Factory Creates and holds all the cars even Tesla's
    /// </summary>
    public static class CarFactory
    {
        #region Fields

        /// <summary>
        /// The cars
        /// </summary>
        private static List<Car> cars = new List<Car>();

        /// <summary>
        /// The last car identifier
        /// </summary>
        private static int lastCarId = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets all cars.
        /// </summary>
        /// <value>
        /// All cars.
        /// </value>
        public static IEnumerable<Car> AllCars
        {
            get
            {
                return cars;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the car.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns>the created car</returns>
        public static Car CreateCar(Transform transform)
        {
            CarType type = CarType.Car;

            if (UnityEngine.Random.value > 0.8)
            {
                type = (CarType)UnityEngine.Random.Range(2, 5);
            }

            Car car = new Car(transform, lastCarId++, type);
            cars.Add(car);

            return car;
        }

        #endregion Methods
    }
}