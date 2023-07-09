using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PoolSystem
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool instance;

        #region PoolTypes Class
        [Serializable]
        public class PoolTypes
        {
            public string PoolTypeName;
            public Pool[] TypedPool;

            public PoolTypes(string poolTypeName, Pool[] typedPool)
            {
                PoolTypeName = poolTypeName;
                TypedPool = typedPool;
            }
        }

        #endregion

        #region Pool Struct

        [Serializable]
        public struct Pool
        {
            public Queue<GameObject> PooledObjects;
            public GameObject Obj;
            public int PoolSize;
        }

        #endregion

        [SerializeField] private PoolTypes[] pools = null;

        private void Awake()
        {
            instance = this;
            SpawnObjects();


        }
        private void SpawnObjects()
        {
            for (int i = 0; i < pools.Length; i++)
            {
                for (int j = 0; j < pools[i].TypedPool.Length; j++)
                {
                    pools[i].TypedPool[j].PooledObjects = new Queue<GameObject>();
                    var gameObject = new GameObject(pools[i].PoolTypeName);
                    gameObject.transform.SetParent(transform);
                    for (int k = 0; k < pools[i].TypedPool[j].PoolSize; k++)
                    {
                        GameObject obj = Instantiate(pools[i].TypedPool[j].Obj, gameObject.transform);
                        obj.SetActive(false);
                        obj.GetComponent<IPooled>().PoolType = pools[i].PoolTypeName;
                        obj.GetComponent<IPooled>().PoolId = j;
                        pools[i].TypedPool[j].PooledObjects.Enqueue(obj);
                    }
                }
            }
        }

        public bool PoolObjectExist(string poolType,int poolId = 0)
        {
            var isExist = pools.Where(x => x.PoolTypeName == poolType).FirstOrDefault().TypedPool[poolId].PooledObjects.Count > 0;
            return isExist;
        }

        public GameObject GetObject(string poolType, int poolId = 0)
        {
            GameObject lastObj = null;
            var newPoolId = 0;
            if (poolId == 0)
            {
                newPoolId = Random.Range(0, pools.Where(x => x.PoolTypeName == poolType).FirstOrDefault().TypedPool.Length - 1);
            }
            else
            {
                newPoolId = poolId;
            }
            lastObj = pools.Where(x => x.PoolTypeName == poolType).FirstOrDefault().TypedPool[newPoolId].PooledObjects.Dequeue();
            lastObj.SetActive(true);
            return lastObj;
        }

        public void PutObject(string poolType, int poolId, object any)
        {
            var obj = (GameObject)any;
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pools.Where(x => x.PoolTypeName == poolType).FirstOrDefault().TypedPool[poolId].PooledObjects.Enqueue(obj);
        }
    }
}

