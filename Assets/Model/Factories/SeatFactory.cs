// <copyright file="SeatFactory.cs" company="CleVR B.V.">
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
    /// Seat factory creates and holds all seats
    /// </summary>
    public static class SeatFactory
    {
        #region Fields

        /// <summary>
        /// The seats
        /// </summary>
        private static List<Seat> seats = new List<Seat>();

        /// <summary>
        /// The last seat identifier
        /// </summary>
        private static int lastSeatId = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets all seats.
        /// </summary>
        /// <value>
        /// All seats.
        /// </value>
        public static IEnumerable<Seat> AllSeats
        {
            get
            {
                return seats;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the seat.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns>the created seat</returns>
        public static Seat CreateSeat(Transform transform)
        {
            Seat seat = new Seat(transform, lastSeatId++);
            seats.Add(seat);
            return seat;
        }

        #endregion Methods

        /// <summary>
        /// Gets a random empty seat.
        /// </summary>
        /// <returns>empty seat</returns>
        public static Seat GetRandomEmptySeat()
        {
            List<Seat> emptySeats = seats.Where(x => !x.Reserved).ToList();

            if (emptySeats.Count > 0)
            {
                return emptySeats[UnityEngine.Random.Range(0, emptySeats.Count)];
            }

            return null;
        }
    }
}