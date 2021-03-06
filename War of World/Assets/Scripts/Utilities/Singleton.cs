﻿using UnityEngine;
using System;
using System.Collections;

namespace Tanks.Utilities
{

	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		private static T s_instance;

		/// <summary>
		/// The static reference to the instance
		/// </summary>
		public static T s_Instance
		{
			get
			{
				return s_instance;
			}
			protected set
			{
				s_instance = value;
			}
		}

        public static T Get()
        {
            return s_instance;
        }

		/// <summary>
		/// Gets whether an instance of this singleton exists
		/// </summary>
		public static bool s_InstanceExists { get { return s_instance != null; } }

		/// <summary>
		/// Awake method to associate singleton with instance
		/// </summary>
		protected virtual void Awake()
		{
			if (s_instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				s_instance = (T)this;
			}
		}

		/// <summary>
		/// OnDestroy method to clear singleton association
		/// </summary>
		protected virtual void OnDestroy()
		{
			if (s_instance == this)
			{
				s_instance = null;
			}
		}
	}

    public class CustomSingleton<T>
    {
        class SingletonCreator
        {
            internal static readonly T instance = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// The static reference to the instance
        /// </summary>
        public static T Get()
        {
            return SingletonCreator.instance;
        }
    }
}
