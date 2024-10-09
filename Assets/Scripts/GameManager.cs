using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;

    private Transform[] _spawnPoints = new Transform[2];
    private Color[] _playerColors = { Color.blue, Color.red };

    void Awake()
    {
        Transform spawnParent = transform.Find("SpawnPoints");

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = spawnParent.GetChild(i);
        }
    }

    void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            GameObject player = Instantiate(_playerPrefab, _spawnPoints[i].position, Quaternion.identity);
            player.GetComponent<PlayerController>().ID = i + 1;
            player.GetComponent<SpriteRenderer>().color = _playerColors[i];

            player.gameObject.name = "Player " + (i + 1).ToString();
        }
    }
}
