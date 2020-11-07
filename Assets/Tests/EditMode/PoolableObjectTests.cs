using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ObjectPool;
using System;

namespace Tests
{
    public class PoolableObjectTests
    {
        private PoolableObject _poolableObject;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            _poolableObject = new GameObject().AddComponent<PoolableObject>();
        }

        [Test]
        public void Enable_NotActiveGameObject_GameObjectSetActive()
        {
            _poolableObject.gameObject.SetActive(false);
            _poolableObject.Enable();
            Assert.IsTrue(_poolableObject.gameObject.activeSelf);
        }

        [Test]
        public void Disable_ActiveGameObject_GameObjectSetNotActive()
        {
            _poolableObject.gameObject.SetActive(true);
            _poolableObject.Disable();
            Assert.IsFalse(_poolableObject.gameObject.activeSelf);
        }
    }
}
