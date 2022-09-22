using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace StatSystem.Runtime
{
    public class Stat
    {
        protected StatDefinition m_Definition;
        protected int m_Value;
        public int value => m_Value;
        public virtual int baseValue => m_Definition.baseValue;
        public event Action valueChanged;
        protected List<StatModifier> m_Modifiers = new List<StatModifier>(); // Serialize�� �ȵż� new�� �������
 
        public Stat(StatDefinition definition)
        {
            m_Definition = definition;
            CalculateValue(); // ���⼭ CalculateValue�� ������ ������ Modifier�� �߰� �ǰų� �����Ǳ� �������� m_Value�� ���� �ٲ��� �ʴ´�.
        }

        public void AddModifier(StatModifier modifier)
        {
            m_Modifiers.Add(modifier);
            CalculateValue();
        }

        public void RemoveMoodifierFromSource(Object source)
        {
            m_Modifiers = m_Modifiers.Where(m => m.source.GetInstanceID() != source.GetInstanceID()).ToList(); // ��ø�� ���ȵ��� �ѹ��� ó�����ֱ� ����?
            CalculateValue();
        }

        protected void CalculateValue()
        {
            int newValue = baseValue;

            m_Modifiers.Sort((x, y) => x.type.CompareTo(y.type)); // enum Ŭ������ ���� ������ ���� ������ ������ ������

            for(int i  = 0; i < m_Modifiers.Count; i++)
            {
                StatModifier modifier = m_Modifiers[i];

                if(modifier.type == ModifierOperatonType.Additive)
                {
                    newValue += modifier.magnitude;
                }
                else if(modifier.type == ModifierOperatonType.Multiplicative)
                {
                    newValue *= modifier.magnitude;
                }
            }
        
            if(m_Definition.cap >= 0)
            {
                newValue = Mathf.Min(newValue, m_Definition.cap);
            }

            if(m_Value != newValue)
            {
                m_Value = newValue;
                valueChanged?.Invoke();
            }
        }
    }

}