// <copyright file="Bench.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.Model
{
    using Assets.Model.Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Bench model
    /// </summary>
    /// <seealso cref="Assets.Model.BaseModel" />
    public class Bench : BaseModel
    {
        #region Fields

        /// <summary>
        /// The seats
        /// </summary>
        private List<Seat> seats = new List<Seat>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bench" /> class.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="id">The unique  identifier.</param>
        public Bench(Transform transform, int id)
            : base(transform, id, "Bench")
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the seats.
        /// </summary>
        /// <value>
        /// The seats.
        /// </value>
        public IEnumerable<Seat> Seats
        {
            get
            {
                return this.seats;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the seat.
        /// </summary>
        /// <param name="transform">The transform.</param>
        public void AddSeat(Transform transform)
        {
            this.seats.Add(SeatFactory.CreateSeat(transform));
        }

        #endregion Methods
    }
}