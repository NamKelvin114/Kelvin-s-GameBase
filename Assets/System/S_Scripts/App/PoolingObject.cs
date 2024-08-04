using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kelvin.Pool
{
    public class PoolingObject : Initialize
    {
        public static PoolingObject Instance;
        private readonly Dictionary<string, LinkedList<GameObject>> activePool = new();
        private readonly Dictionary<string, Queue<GameObject>> waitPool = new();
        private Guid _guid;

        public string newKey => GetKey();

        public override void Init(Action<bool> loadComplete)
        {
            Init();
            base.Init(loadComplete);
        }

        public void Init()
        {
            Instance = this;
            activePool.Clear();
            waitPool.Clear();
        }

        private string GetKey()
        {
            _guid = new Guid();
            return _guid.ToString();
        }

        public void PreSpawn(string keyPool, GameObject obj, int number, Transform parent, Action actionPreSpawn = null)
        {
            for (var i = 0; i < number; i++) SpawnNew(keyPool, obj, parent, false);
            actionPreSpawn?.Invoke();
        }

        private GameObject SpawnNew(string keyPool, GameObject obj, Transform parent, bool isUseNow,
            Action actionSpawn = null)
        {
            var spawnObj = Instantiate(obj, parent);
            if (!activePool.ContainsKey(keyPool))
            {
                activePool.Add(keyPool, new LinkedList<GameObject>());
                waitPool.Add(keyPool, new Queue<GameObject>());
            }

            activePool[keyPool].AddLast(spawnObj);
            actionSpawn?.Invoke();
            if (!isUseNow) DeSpawn(keyPool, spawnObj);
            return spawnObj;
        }

        public void DeSpawn(string keyPool, GameObject obj, bool isDestroy = false, Action actionDeSpawn = null)
        {
    
            var getObj = activePool[keyPool].Where(o => o == obj).FirstOrDefault();
            activePool[keyPool].Remove(getObj);
            if (isDestroy)
            {
                actionDeSpawn?.Invoke();
                Destroy(getObj);
            }
            else
            {
                getObj.SetActive(false);
                actionDeSpawn?.Invoke();
            }

            waitPool[keyPool].Enqueue(getObj);
        }

        public void DeSpawnAll(string keyPool, bool isDestroy, Action actionDeSpawn = null)
        {
            foreach (var getWaitObj in waitPool[keyPool]) DeSpawn(keyPool, getWaitObj, isDestroy, actionDeSpawn);
        }

        public GameObject Spawn(string keyPool, GameObject obj, Transform parent, Action actionSpawn)
        {
            GameObject spawnObj = null;
            bool existPool = waitPool.ContainsKey(keyPool);
            if (existPool)
            {
                bool hasObj = true;
                if (hasObj)
                {
                    var dequeue = waitPool[keyPool].Dequeue();
                    dequeue.SetActive(true);
                    actionSpawn?.Invoke();
                    activePool[keyPool].AddLast(dequeue);
                    spawnObj = dequeue;
                }
                else
                {
                    Debug.LogError($"Can not found Obj in {keyPool}"+"key pool");
                }
            }
            else
            {
                spawnObj = SpawnNew(keyPool, obj, parent, true, actionSpawn);
            }
            return spawnObj;
        }
    }
}
