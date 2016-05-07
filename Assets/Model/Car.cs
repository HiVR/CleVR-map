// <copyright file="Car.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Car model broom broom!!
    /// </summary>
    public sealed class Car : BaseModel
    {
        #region Fields

        /// <summary>
        /// The turn siren off on moment in time
        /// </summary>
        private float turnSirenOffOn;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Car" /> class.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="carType">Type of the car.</param>
        public Car(Transform transform, int id, CarType carType)
            : base(transform, id, carType.ToString())
        {
            this.Type = carType;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the car type.
        /// </summary>
        public CarType Type { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has siren.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has siren; otherwise, <c>false</c>.
        /// </value>
        public bool HasSiren
        {
            get
            {
                return this.Type != CarType.Car;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [siren active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [siren active]; otherwise, <c>false</c>.
        /// </value>
        public bool SirenActive { get; private set; }

        /// <summary>
        /// Activates the siren.
        /// </summary>
        /// <param name="duration">The duration.</param>
        public void ActivateSiren(float duration)
        {
            this.turnSirenOffOn = Time.time + duration;
            this.SirenActive = true;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (this.SirenActive && this.turnSirenOffOn < Time.time)
            {
                this.SirenActive = false;
            }
        }

        #endregion Properties
    }
}