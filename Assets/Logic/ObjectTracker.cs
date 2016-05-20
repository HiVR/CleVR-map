// <copyright file="ObjectTracker.cs" company="HiVR">
//     HiVR All rights reserved.
// </copyright>
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Logic
{
    /// <summary>
    /// Static class which keeps track of all relevant objects in the Unity world.
    /// </summary>
    public static class ObjectTracker
    {
        #region Fields

        /// <summary>
        /// The list of static objects.
        /// </summary>
        private static List<MonoBehaviour> staticObjects = new List<MonoBehaviour>();

        /// <summary>
        /// The list of dynamic objects.
        /// </summary>
        private static List<MonoBehaviour> dynamicObjects = new List<MonoBehaviour>();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Adds a static object to the tracking list.
        /// </summary>
        public static void AddStaticObject(MonoBehaviour staticObject)
        {
            staticObjects.Add(staticObject);
        }

        /// <summary>
        /// Removes a static object from the tracking list.
        /// </summary>
        public static void RemoveStaticObject(MonoBehaviour staticObject)
        {
            staticObjects.Remove(staticObject);
        }

        /// <summary>
        /// Returns a list of all static objects.
        /// </summary>
        public static List<MonoBehaviour> GetStaticObjects()
        {
            return staticObjects;
        }

        /// <summary>
        /// Adds a dynamic object to the tracking list.
        /// </summary>
        public static void AddDynamicObject(MonoBehaviour dynamicObject)
        {
            dynamicObjects.Add(dynamicObject);
        }

        /// <summary>
        /// Removes a dynamic object from the tracking list.
        /// </summary>
        public static void RemoveDynamicObject(MonoBehaviour dynamicObject)
        {
            dynamicObjects.Remove(dynamicObject);
        }

        /// <summary>
        /// Returns a list of all dynamic objects.
        /// </summary>
        public static List<MonoBehaviour> GetDynamicObjects()
        {
            return dynamicObjects;
        }

        #endregion Methods
    }
}
