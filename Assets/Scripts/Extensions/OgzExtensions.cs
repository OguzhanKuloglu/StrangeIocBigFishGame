using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class OgzExtensions
    {
        public static void AddOrCreate<TKey, TValue>(this IDictionary<TKey, List<TValue>> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key].Add(value);
            }
            else
            {
                dict.Add(key, new List<TValue>() { value });
            }
        }
        public static void ListAllDisable(this List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].gameObject.SetActive(false);
            }
        }
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
        private static Transform _findInChildrenWithName(Transform trans, string name)
        {
            
            for (int i = 0; i < trans.childCount; i++)
            {
                string nameChild = trans.GetChild(i).name;
                nameChild = nameChild.Replace("(Clone)", ""); 
                if (nameChild == name)
                    return trans.GetChild(i);
            }

            return null;
        }

        public static Transform FindInChildrenWithName(this Transform trans, string name)
        {
            return _findInChildrenWithName(trans, name);
        }
        
        public static void DisableChildren(this Transform trans)
        {
            for (int i = 0; i < trans.childCount; i++)
            {
              trans.GetChild(i).gameObject.SetActive(false);
            }
        }
        public static void DestroyChildren(this Transform trans)
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                GameObject.Destroy(trans.GetChild(i).gameObject);
            }
        }
    }
}