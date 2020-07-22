using System.Collections;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;

    void Awake()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;

    public CameraShake cameraShake;

    private void Start()
    {
        if(cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }
    }

    public IEnumerator _RespawnPlayer()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("TODO: Add waiting for spawn sound");
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(gm.playerPrefab, gm.spawnPoint.position, gm.spawnPoint.rotation).GetComponent<GameObject>();
        Destroy(clone, 3f);
    }

    // to pass the GameMaster to everywhere
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm._RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy(Enemy _enemy)
    {
        GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity).gameObject;
        Destroy(_clone, 5f);
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}
