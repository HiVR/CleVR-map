// <copyright file="ConcurrentHashSet.cs" company="HiVR">
// Copyright (c) 2016 HiVR All Rights Reserved
// </copyright>

namespace Assets.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// ConcurrentHashSet implementation by Ben Mosher.
    /// http://stackoverflow.com/questions/4306936/how-to-implement-concurrenthashset-in-net
    /// </summary>
    /// <typeparam name="T">the element type of the ConcurrentHashSet</typeparam>
    public class ConcurrentHashSet<T> : IDisposable
    {
        #region Fields

        /// <summary>
        /// Contains the lock used for concurrency.
        /// </summary>
        private readonly ReaderWriterLockSlim @lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        /// <summary>
        /// Contains the HashSet that will be used in the entire class.
        /// </summary>
        private readonly HashSet<T> hashSet = new HashSet<T>();

        #endregion Fields

        #region Destructor

        /// <summary>
        /// Finalizes an instance of the ConcurrentHashSet class.
        /// </summary>
        ~ConcurrentHashSet()
        {
            this.Dispose(false);
        }

        #endregion Destructor

        #region Implementation of ICollection<T> ...ish

        /// <summary>
        /// Gets the amount of entries in the hashSet with read lock.
        /// </summary>
        public int Count
        {
            get
            {
                @lock.EnterReadLock();
                try
                {
                    return this.hashSet.Count;
                }
                finally
                {
                    if (@lock.IsReadLockHeld)
                    {
                        @lock.ExitReadLock();
                    }
                }
            }
        }

        /// <summary>
        /// Add an item to the hashSet with write lock.
        /// </summary>
        /// <param name="item">the items that is added</param>
        /// <returns>true if success, else false</returns>
        public bool Add(T item)
        {
            @lock.EnterWriteLock();
            try
            {
                return this.hashSet.Add(item);
            }
            finally
            {
                if (@lock.IsWriteLockHeld)
                {
                    @lock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Removes all elements from the HashSet with write lock.
        /// </summary>
        public void Clear()
        {
            @lock.EnterWriteLock();
            try
            {
                this.hashSet.Clear();
            }
            finally
            {
                if (@lock.IsWriteLockHeld)
                {
                    @lock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Determines whether the HashSet contains the element with read lock.
        /// </summary>
        /// <param name="item">element that is checked to exist</param>
        /// <returns>true if the hashSet contains the element, false otherwise</returns>
        public bool Contains(T item)
        {
            @lock.EnterReadLock();
            try
            {
                return this.hashSet.Contains(item);
            }
            finally
            {
                if (@lock.IsReadLockHeld)
                {
                    @lock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// Removes the specified element from the HashSet with write lock.
        /// </summary>
        /// <param name="item">item that is removed</param>
        /// <returns>true if success, false if failed</returns>
        public bool Remove(T item)
        {
            @lock.EnterWriteLock();
            try
            {
                return this.hashSet.Remove(item);
            }
            finally
            {
                if (@lock.IsWriteLockHeld)
                {
                    @lock.ExitWriteLock();
                }
            }
        }

        #endregion Implementation of ICollection<T> ...ish

        #region Dispose

        /// <summary>
        /// Dispose the object and let it be removed by the GC.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// While disposing check the locks.
        /// </summary>
        /// <param name="disposing">true is disposed, false if not disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (@lock != null)
                {
                    @lock.Dispose();
                }
            }
        }

        #endregion Dispose
    }
}