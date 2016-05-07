// <copyright file="CarType.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// All possible car types
    /// </summary>
    public enum CarType
    {
        /// <summary>
        /// A normal car
        /// </summary>
        Car = 1,

        /// <summary>
        /// A police cruiser
        /// </summary>
        Police = 1,

        /// <summary>
        /// An Ambulance van
        /// </summary>
        Ambulance = 3,

        /// <summary>
        /// A fire truck
        /// </summary>
        FireTruck = 4,
    }
}