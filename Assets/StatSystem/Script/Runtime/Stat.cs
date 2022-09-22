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
        protected List<StatModifier> m_Modifiers = new List<StatModifier>(); // Serialize가 안돼서 new를 해줘야함
 
        public Stat(StatDefinition definition)
        {
            m_Definition = definition;
            CalculateValue(); // 여기서 CalculateValue를 해주지 않으면 Modifier가 추가 되거나 삭제되기 전까지는 m_Value의 값이 바뀌지 않는다.
        }

        public void AddModifier(StatModifier modifier)
        {
            m_Modifiers.Add(modifier);
            CalculateValue();
        }

        public void RemoveMoodifierFromSource(Object source)
        {
            m_Modifiers = m_Modifiers.Where(m => m.source.GetInstanceID() != source.GetInstanceID()).ToList(); // 중첩된 스탯들을 한번에 처리해주기 위해?
            CalculateValue();
        }

        protected void CalculateValue()
        {
            int newValue = baseValue;

            m_Modifiers.Sort((x, y) => x.type.CompareTo(y.type)); // enum 클래스의 선언 순서에 따라 연산의 순서가 정해짐

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