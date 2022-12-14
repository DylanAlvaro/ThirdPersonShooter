using System;
using System.Collections;

using ThirdPersonShooter.Entities.Player;

using UnityEngine;

using Random = UnityEngine.Random;

namespace ThirdPersonShooter.Entities.AI
{
	public class Spawner : MonoBehaviour
	{
		//private Vector3 HalfExtents => bounds.extents * .5f;
		
		[SerializeField] private Bounds bounds = new Bounds(Vector3.zero, Vector3.one);

		[Space, SerializeField, Min(1f)] private float spawnRate = 1f;
		[SerializeField] private GameObject[] enemyPrefab;

		private IEntity player;
		private bool canSpawn = true;

		public void Spawn()
		{
			Vector3 randPos = new Vector3()
			{
				x = Random.Range(-bounds.extents.x , bounds.extents.x),
				y = -bounds.extents.y,
				z = Random.Range(-bounds.extents.z, bounds.extents.z)
			};

			Vector3 boundPos = randPos + bounds.center;
			Vector3 spawnPos = transform.TransformPoint(boundPos);

			GameObject enemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
			Instantiate(enemy, spawnPos, transform.rotation, transform);
		}

		private void Start()
		{
			player = GameManager.IsValid() ? GameManager.Instance.Player : FindObjectOfType<PlayerEntity>();

			StartCoroutine(SpawnLoop_CR());
		}

		private IEnumerator SpawnLoop_CR()
		{
			while(canSpawn)
			{
				yield return new WaitForSeconds(spawnRate);
				
				//added safety check in case player died whilst waiting for a spawn
				if(canSpawn)
					Spawn();
			}
		}

		private void OnDrawGizmos()
		{
			Matrix4x4 defaultMat = Gizmos.matrix;

			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.color = new Color(0, .8f, 0, .8f);
			Gizmos.DrawWireCube(bounds.center, bounds.size);

			Gizmos.matrix = defaultMat;
		}
	}
}