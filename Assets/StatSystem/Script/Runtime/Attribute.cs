using StatSystem.Runtime;
using UnityEngine;
using System;

namespace StatSystem
{
    public class Attribute : Stat
    {
        protected int m_CurrentValue;
        public int currentValue => m_CurrentValue; // 생성자를 호출하는 순서가 부모의 생성자를 통해서 들어가기 때문에 이렇게 초기화 할 수 있다.        public event Action currentValueChanged;
        public event Action currentValueChanged;
        public event Action<StatModifier> applieModifier;

        // applymodfier를 하는 순간 값이 바뀐다(Stat은 modifier를 계속 축적시켜서 값을 바꾼다).
        public Attribute(StatDefinition definition) : base(definition) // 부모의 생성자를 부르는 부분, 자식 생성자에서 부모 생성자를 부르는 오버라이딩
        {
            m_CurrentValue = value; // base 생성자가 먼저 호출되고 본문 생성자로 가기 때문에 value가 초기화 된 이후다.
        }

        public virtual void ApplyModifier(StatModifier modifier)
        {
            int newValue = m_CurrentValue;

            switch (modifier.type)
            {
                case ModifierOperatonType.Override:
                    newValue = modifier.magnitude;
                    break;
                case ModifierOperatonType.Additive:
                    newValue += modifier.magnitude;
                    break;
                case ModifierOperatonType.Multiplicative:
                    newValue *= modifier.magnitude;
                    break;
            }

            newValue = Mathf.Clamp(newValue, 0, m_Value);
            
            if(currentValue != newValue)
            {
                m_CurrentValue = newValue;
                currentValueChanged?.Invoke();
                applieModifier?.Invoke(modifier);
            }

        }
    }
}