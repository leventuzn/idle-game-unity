using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private Queue<GameObject> pooledObjects;

        [SerializeField]
        private GameObject objectPrefab;

        [SerializeField]
        private int poolSize;

        private void Awake()
        {

            Initialize();
        }

        public void Initialize()
        {
            pooledObjects = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefab, gameObject.transform);
                obj.name = objectPrefab.name;
                obj.SetActive(false);

                pooledObjects.Enqueue(obj);
            }
        }

        public GameObject GetFromPool()
        {

            GameObject obj = pooledObjects.Dequeue();

            obj.SetActive(true);

            pooledObjects.Enqueue(obj);

            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);

            obj.transform.SetParent(transform);
        }
    }
}

