using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.ObjectPooling
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        public Transform transform => parent.transform;

        T pooledPrefab;
        T pooledPrefabInstance;

        GameObject parent;
        List<T> pooledObjects;
        List<T> avaliableObjects;


        public ObjectPool(T prefab, string name = "")
        {
            pooledPrefab = prefab;
            prefab.gameObject.SetActive(false);

            pooledObjects = new List<T>();
            avaliableObjects = new List<T>();

            parent = new GameObject("ObjectPool: " +name);
            SetupPrefabInstance();
        }

        void SetupPrefabInstance()
        {
            if (pooledPrefabInstance != null)
                GameObject.Destroy(pooledPrefabInstance.gameObject);

            pooledPrefabInstance = GameObject.Instantiate(pooledPrefab);
            PooledObject pooledObjectComponent = pooledPrefabInstance.gameObject.AddComponent<PooledObject>();

            pooledPrefabInstance.name = pooledPrefab.name + " Prefab Instance";
            pooledPrefabInstance.transform.SetParent(parent.transform, false);
        }


        public T GetObject()
        {
            if (avaliableObjects.Count == 0)
                CreateObjectIntoPool();

            T obj = avaliableObjects[0];
            avaliableObjects.RemoveAt(0);
            return obj;
        }

        protected void CreateObjectIntoPool()
        {
            T obj = Object.Instantiate(pooledPrefabInstance);
            obj.name = $"{pooledPrefab.name} {pooledObjects.Count}";

            PooledObject pooledObject = obj.GetComponent<PooledObject>();

            pooledObject.SetDisableAction(
                () => { ReturnObjectToPool(obj); });

            pooledObject.SetDestroyAction(
                () => { RemoveFromPool(obj); });

            pooledObjects.Add(obj);
            ReturnObjectToPool(obj);
        }

        public void ReturnObjectToPool(T obj)
        {
            if (parent == null)
                return;
            obj.transform.parent = parent.transform;
            avaliableObjects.Add(obj);
        }


        public void MarkPoolForDeletion()
        {
            List<T> oldObjs = new List<T>(pooledObjects);
            //StartCoroutine(ClearPooledList(oldObjs));

            pooledObjects = new List<T>();
        }


        public void RemoveFromPool(T obj)
        {
            if (pooledObjects.Contains(obj))
                pooledObjects.Remove(obj);
            if (avaliableObjects.Contains(obj))
                avaliableObjects.Remove(obj);
        }

    }
}
