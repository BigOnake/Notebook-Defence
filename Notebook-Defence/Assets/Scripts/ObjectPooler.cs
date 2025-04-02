using System;
using Unity.VisualScripting;
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
        _poolContainer.transform.SetParent( parent );

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
        instance.transform.SetParent(_poolContainer.transform); //assign as child to its objectpooler gameobject
        instance.gameObject.SetActive(false); //ensure it is inactive at first
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
            T instance = CreateObject();
            _pool.Release(instance);
        }
    }

    public T GetObject()
    {
        return _pool.Get();
    }

    public void ReleaseObject(T obj)
    {
        if (obj.gameObject.activeSelf) //could maybe be removed as it may be redundant, untested though
        {
            _pool.Release(obj);
        }
    }

    public void ClearPool()
    {
        _pool.Clear();
        UnityEngine.Object.Destroy(_poolContainer);
    }
}
