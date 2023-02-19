using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
        private static T m_ins;

        public static T Ins
        {
                get
                {
                        if (!m_ins)
                        {
                                m_ins = GameObject.FindObjectOfType<T>();
                                if (!m_ins)
                                {
                                        GameObject singleton = new GameObject(typeof(T).Name);
                                        m_ins = singleton.AddComponent<T>();
                                }
                        }

                        return m_ins;
                }
        }

        public virtual  void Start()
        {
                
        }

        public virtual void Update()
        {

        }

        public virtual  void Awake()
        {

        }
}