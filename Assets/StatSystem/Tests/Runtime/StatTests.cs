using System.Collections;
//using System.Collections.Generic;
using NUnit.Framework;
using StatSystem.Runtime;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace StatSystem.Tests
{
    public class StatTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            EditorSceneManager.LoadSceneInPlayMode("Assets/StatSystem/Tests/Scenes/Tests.unity", new LoadSceneParameters(LoadSceneMode.Single)); 
            // ���� ���ÿ� ������ ��� �� �ִµ� ������ �ִ� ���� ������ �ʰ� ���� �ε��� ���� �ø� �� �ִ°� additive. 
            // �ִ� ���� �� ������ ���� �ε��� ���� �ø��� �� single.
        }

        // Modifier�� �� ������ �Ǵ��� Ȯ���ϴ� �Լ�
        [UnityTest]
        public IEnumerator Stat_WhenModifierApplied_ChangesValue()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            Stat physicalAttack = statController.stats["PhysicalAttack"];
            Assert.AreEqual(0, physicalAttack.value); // 2��°(actual) ���� 1��°(expected) ���̶� ������ ����ϰ� false�̸� �׽�Ʈ ����
            physicalAttack.AddModifier(new StatModifier
            {
                magnitude = 5,
                type = ModifierOperatonType.Additive
            });
            Assert.AreEqual(5, physicalAttack.value);
        }

        [UnityTest]
        public IEnumerator Stat_WhenModifierApplied_DoseNotExceedCap()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            Stat attackSpeed = statController.stats["AttackSpeed"];
            Assert.AreEqual(1, attackSpeed.value);
            attackSpeed.AddModifier(new StatModifier
            { 
                magnitude = 5,
                type = ModifierOperatonType.Additive
            });
            Assert.AreEqual(3, attackSpeed.value);
        }
    }
}
