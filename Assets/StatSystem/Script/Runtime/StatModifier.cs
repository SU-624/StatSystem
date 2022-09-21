using UnityEngine;

namespace StatSystem.Runtime
{

    public enum ModifierOperatonType    // 해당 Modifer의 연산을 결정해주는 
    {
        Additive,
        Multiplicative,
        Override,
    }

    public class StatModifier
    {
        // =>랑 {get; set;} 모두 프로퍼티인데 =>는 getter만 가지고있는 프로퍼티
        public Object source { get; set; } // 이 moodifier를 발생, 적용시킨애
        public int magnitude { get; set; } // 이 스탯을 얼마나 바꿔줄것인지에 대한 숫자
        public ModifierOperatonType type { get; set; } // 이 magnitude만큼 스탯에 어떻게 변화를 줄지 정하는 것

    }

}