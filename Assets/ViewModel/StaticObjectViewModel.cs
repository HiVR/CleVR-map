// <copyright file="StaticObjectViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Static object view model for object that will never move during runtime.
    /// </summary>
    /// <typeparam name="T">Type of the model layer</typeparam>
    /// <seealso cref="Assets.ViewModel.BaseViewModel{T}" />
    public abstract class StaticObjectViewModel<T> : BaseViewModel<T>
    {
    }
}