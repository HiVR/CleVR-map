// <copyright file="Ethnicity.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Describes the ethnicity of a character
    /// </summary>
    [Flags]
    public enum Ethnicity
    {
        /// <summary>
        /// The western Europe
        /// </summary>
        WesternEurope = 1,

        /// <summary>
        /// The southern Europe
        /// </summary>
        SouthernEurope = 2,

        /// <summary>
        /// The eastern Europe
        /// </summary>
        EasternEurope = 4,

        /// <summary>
        /// The northern Africa
        /// </summary>
        NorthernAfrica = 8,

        /// <summary>
        /// The central Africa
        /// </summary>
        CentralAfrica = 16,

        /// <summary>
        /// The southern Africa
        /// </summary>
        SouthernAfrica = 32,

        /// <summary>
        /// The middle east
        /// </summary>
        MiddleEast = 64,

        /// <summary>
        /// The eastern Asia
        /// </summary>
        EasternAsia = 128,

        /// <summary>
        /// The southern Asia
        /// </summary>
        SouthernAsia = 256,

        /// <summary>
        /// The western Asia
        /// </summary>
        WesternAsia = 512,

        /// <summary>
        /// The American Indian
        /// </summary>
        AmericanIndian = 1024,

        /// <summary>
        /// The other
        /// </summary>
        Other = 2018
    }
}