using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
	[SerializeField] private GameObject EnemiesSpawns;
	[SerializeField] private GameObject[] Enemies;

	// Start is called before the first frame update
	void Start()
	{
		// For every child in the EnemiesSpawns object
		// Instantiate 3 to 5 enemies from the Enemies array
		foreach (Transform child in EnemiesSpawns.transform)
		{
			int random = Random.Range(4, 7);

			for (int i = 0; i < random; i++)
			{
				int randomEnemy = Random.Range(0, Enemies.Length);
				Instantiate(Enemies[randomEnemy], child.position, Quaternion.identity);
			}
		}
	}
}
