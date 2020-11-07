using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using ObjectPool;
using System;

namespace Tests
{
    public class PoolableObjectTests
    {
        private class TestComponent : MonoBehaviour
        {
            
        }

        private PoolableObject _poolableObject;

        private readonly Vector3 _expectedPosition = new Vector3(1,2,3);

        [OneTimeSetUp]
        public void InitialSetup()
        {
            var poolableGameObject = GameObject.Instantiate(
                    AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Cube.prefab",
                                                    typeof(GameObject))
            ) as GameObject;

            _poolableObject = poolableGameObject.GetComponent<PoolableObject>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            GameObject.Destroy(_poolableObject.gameObject);
        }

        [UnityTest]
        public IEnumerator ConfigurePosition_CustomPosition_PositionProperlySet()
        {
            _poolableObject.transform.position = new Vector3(0,0,0);

            _poolableObject.ConfigurePosition(_expectedPosition);

            Assert.AreEqual(_expectedPosition, _poolableObject.transform.position);
            
            yield return null;
        }

        [UnityTest]
        public IEnumerator GetComponent_CustomComponent_ExistingComponentReturned()
        {
            _poolableObject.gameObject.AddComponent<TestComponent>();
            Assert.NotNull(_poolableObject.GetComponent<TestComponent>());
            
            yield return null;
        }

        [UnityTest]
        public IEnumerator ConfigureReturnToPool_CustomAction_CustomActionExecutedOnDisable()
        {
            _poolableObject.ConfigureReturnToPool((a) => {
                Assert.IsFalse(a.gameObject.activeSelf);
            });

            _poolableObject.Disable();
            
            yield return null;
        }
    }
}
