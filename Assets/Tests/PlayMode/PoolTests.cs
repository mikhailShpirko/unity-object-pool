using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ObjectPool;
using UnityEditor;
using System.Linq;

namespace Tests
{
    public class PoolTests
    {
        private Pool _objectPool;

        private const int _numberOfObjectsInPoolSetupped = 60;

        private bool IsNotPrefab(PoolableObject p)
        {
            return !EditorUtility.IsPersistent(p.transform.root.gameObject);
        }

        private bool IsInPool(PoolableObject p)
        {
            return !p.gameObject.activeSelf;
        }

        private IEnumerable<PoolableObject> GetPoolableObjects()
        {
            var poolableObjects = Resources.FindObjectsOfTypeAll<PoolableObject>();
            return poolableObjects.Where(p => IsNotPrefab(p));
        }

        [OneTimeSetUp]
        public void InitialSetup()
        {
            var poolableGameObject = GameObject.Instantiate(
                    AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tests/ObjectPoolTest.prefab",
                                                    typeof(GameObject))
            ) as GameObject;

            _objectPool = poolableGameObject.GetComponent<Pool>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            GameObject.Destroy(_objectPool.gameObject);
        }

        [UnityTest]
        public IEnumerator FillPool_PrefabSettings_CorrectNumberOfObjectsCreated()
        {  
            yield return null;

            Assert.AreEqual(_numberOfObjectsInPoolSetupped, GetPoolableObjects().Count());
        }

        [UnityTest]
        public IEnumerator FillPool_PrefabSettings_CorrectNumberOfObjectTypesCreated()
        {  
            yield return null;

            Assert.AreEqual(3, GetPoolableObjects().Select(p => p.Type).Distinct().Count());
        }

        [UnityTest]
        public IEnumerator GetFromPool_PoolableObject_ObjectReturnedFromPull()
        {
            yield return null;
            
            var pooledObject = _objectPool.GetFromPool<PoolableObject>(PoolObjectType.Cube, Vector3.zero);

            Assert.NotNull(pooledObject);

            Assert.AreEqual(_numberOfObjectsInPoolSetupped - 1, GetPoolableObjects().Count(p => IsInPool(p)));

            //return to pool
            pooledObject.Disable();
        }


        [UnityTest]
        public IEnumerator GetFromPool_NotSetupObjectType_NullReturnedFromPool()
        {
            yield return null;
            
            var pooledObject = _objectPool.GetFromPool<PoolableObject>(PoolObjectType.Sphere, Vector3.zero);

            Assert.Null(pooledObject);
        }


        [UnityTest]
        public IEnumerator ReturnToPool_PoolableObject_ReturnedToPoolWhenDisabled()
        {
            yield return null;
            
            var pooledObject = _objectPool.GetFromPool<PoolableObject>(PoolObjectType.Cube, Vector3.zero);

            pooledObject.Disable();
            
            Assert.AreEqual(_numberOfObjectsInPoolSetupped, GetPoolableObjects().Count(p => IsInPool(p)));
        }
    }
}
