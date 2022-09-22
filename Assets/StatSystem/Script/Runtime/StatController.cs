using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem.Runtime
{
    public class StatController : MonoBehaviour
    {
        [SerializeField] private StatDatabase m_statDatabase;
        protected Dictionary<string, Stat> m_Stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase); // Ű���� �̸����� ��ҹ��ڿ� �ΰ����� �ʰ� ���ֱ� ���� �ɼ� 
        public Dictionary<string, Stat> stats => m_Stats;

        private bool m_IsInitialized;
        public bool isIntialized => m_IsInitialized;
        public event Action initialized;
        public event Action willUninitialize;

        protected virtual void Awake()
        {
            if(!m_IsInitialized)
            {
                Initialized();
                m_IsInitialized = true;
                initialized?.Invoke();
            }
        }

        private void OnDestroy()
        {
            willUninitialize?.Invoke();
        }

        private void Initialized()
        {
            foreach(StatDefinition definition in m_statDatabase.stats)
            {
                m_Stats.Add(definition.name, new Stat(definition));
            }

            foreach (StatDefinition definition in m_statDatabase.attribute)
            {
                m_Stats.Add(definition.name, new Attribute(definition));
            }
        }
    }

}