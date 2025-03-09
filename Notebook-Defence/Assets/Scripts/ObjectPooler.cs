using System;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler<T> where T : MonoBehaviour
{
    private ObjectPool<T> _pool;
    private T _prefab;
    private Transform _parent;
    private GameObject _poolContainer;

    public ObjectPooler(T prefab, Transform parent, int initialSize, int maxSize)
    {
        _prefab = prefab;
        _parent = parent;

        _poolContainer = new GameObject($"Pool - {_prefab.name}");

        _pool = new ObjectPool<T>(
            CreateObject,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPooledObject,
            true,
            initialSize,
            maxSize
        );

        PreloadObjects(initialSize);
    }

    private T CreateObject()
    {
        T instance = UnityEngine.Object.Instantiate(_prefab);
        instance.transform.SetParent(_poolContainer.transform); 
        return instance;
    }

    private void OnTakeFromPool(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(T obj)
    {
        UnityEngine.Object.Destroy(obj.gameObject);
    }

    private void PreloadObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            T intance = _pool.Get();
            _pool.Release(intance);
        }
    }

    public T GetObject()
    {
        return _pool.Get();
    }

    public void ReleaseObject(T obj)
    {
        _pool.Release(obj); 
    }
}
