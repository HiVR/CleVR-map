// <copyright file="HeadGear.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Describes the character head gear
    /// </summary>
    [Flags]
    public enum HeadGear
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 1,

        /// <summary>
        /// The hoodie
        /// </summary>
        Hoodie = 2,

        /// <summary>
        /// The glasses
        /// </summary>
        Glasses = 4,

        /// <summary>
        /// The shades
        /// </summary>
        Shades = 8,

        /// <summary>
        /// The head scarf
        /// </summary>
        HeadScarf = 16,

        /// <summary>
        /// The cap
        /// </summary>
        Cap = 32,

        /// <summary>
        /// The hat
        /// </summary>
        Hat = 64
    }
}