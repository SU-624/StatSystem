using UnityEngine;

namespace StatSystem.Runtime
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "StatSystem/StatDefiniton", order = 0)]

    public class StatDefinition : ScriptableObject
    {
        [SerializeField] private int m_BaseValue;
        [SerializeField] private int m_Cap = -1;

        public int baseValue => m_BaseValue;
        public int cap => m_Cap;
    }
}