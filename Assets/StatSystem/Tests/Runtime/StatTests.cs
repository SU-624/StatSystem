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
            // 씬을 동시에 여러개 띄울 수 있는데 기존의 있는 씬을 내리지 않고 지금 로드할 씬을 올릴 수 있는게 additive. 
            // 있는 씬을 다 내리고 지금 로드할 씬만 올리는 게 single.
        }

        // Modifier가 잘 적용이 되는지 확인하는 함수
        [UnityTest]
        public IEnumerator Stat_WhenModifierApplied_ChangesValue()
        {
            yield return null;
            StatController statController = GameObject.FindObjectOfType<StatController>();
            Stat physicalAttack = statController.stats["PhysicalAttack"];
            Assert.AreEqual(0, physicalAttack.value); // 2번째(actual) 값이 1번째(expected) 값이랑 같으면 통과하고 false이면 테스트 실패
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
