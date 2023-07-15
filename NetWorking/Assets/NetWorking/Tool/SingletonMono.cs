using System;
using UnityEngine;

namespace NetWorking.Tool
{
    public class SingletonMono<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = FindObjectOfType<T>();
                }

                return instance;
            }
        }

        
    }
}