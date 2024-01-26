using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SimplePool
{
	public class PoolComponent : MonoBehaviour
	{
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}