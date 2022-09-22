using StatSystem.Runtime;
using UnityEngine;
using System;

namespace StatSystem
{
    public class Attribute : Stat
    {
        protected int m_CurrentValue;
        public int currentValue => m_CurrentValue; // �����ڸ� ȣ���ϴ� ������ �θ��� �����ڸ� ���ؼ� ���� ������ �̷��� �ʱ�ȭ �� �� �ִ�.        public event Action currentValueChanged;
        public event Action currentValueChanged;
        public event Action<StatModifier> applieModifier;

        // applymodfier�� �ϴ� ���� ���� �ٲ��(Stat�� modifier�� ��� �������Ѽ� ���� �ٲ۴�).
        public Attribute(StatDefinition definition) : base(definition) // �θ��� �����ڸ� �θ��� �κ�, �ڽ� �����ڿ��� �θ� �����ڸ� �θ��� �������̵�
        {
            m_CurrentValue = value; // base �����ڰ� ���� ȣ��ǰ� ���� �����ڷ� ���� ������ value�� �ʱ�ȭ �� ���Ĵ�.
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