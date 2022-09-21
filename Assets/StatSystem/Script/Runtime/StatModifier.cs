using UnityEngine;

namespace StatSystem.Runtime
{

    public enum ModifierOperatonType    // �ش� Modifer�� ������ �������ִ� 
    {
        Additive,
        Multiplicative,
        Override,
    }

    public class StatModifier
    {
        // =>�� {get; set;} ��� ������Ƽ�ε� =>�� getter�� �������ִ� ������Ƽ
        public Object source { get; set; } // �� moodifier�� �߻�, �����Ų��
        public int magnitude { get; set; } // �� ������ �󸶳� �ٲ��ٰ������� ���� ����
        public ModifierOperatonType type { get; set; } // �� magnitude��ŭ ���ȿ� ��� ��ȭ�� ���� ���ϴ� ��

    }

}