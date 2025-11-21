using UnityEngine;

public class InfiniteTilemap : MonoBehaviour
{
    [Header("Player ref")]
    [SerializeField] private Transform _player;

    [Header("Chunk Settings")]
    [SerializeField] private float _chunkSize = 20f; // 청크 한 칸 크기
    [SerializeField] private int _chunksToMove = 3; // 한 번 이동할 칸 수

    private Vector2Int _index; // 이 청크의 현재 그리드 인덱스

    private void Start()
    {
        int indexX = Mathf.RoundToInt(transform.position.x / _chunkSize);
        int indexY = Mathf.RoundToInt(transform.position.y / _chunkSize);

        _index = new Vector2Int(indexX, indexY);
    }

    private void Update()
    {
        if(_player == null)
        {
            return;
        }

        // 플레이어가 현재 어느 인덱스에 있는지 계산
        int playerIndexX = Mathf.FloorToInt(_player.position.x / _chunkSize);
        int playerIndexY = Mathf.FloorToInt(_player.position.y / _chunkSize);

        Vector2Int playerIndex = new Vector2Int(playerIndexX, playerIndexY);

        // X 축 체크
        if(_index.x < playerIndex.x - 1)
        {
            // 너무 왼쪽에 있으면 오른쪽으로 이동
            _index.x += _chunksToMove;

            float moveDistance = _chunkSize * _chunksToMove;
            transform.position += new Vector3(moveDistance, 0f, 0f);
        }
        else if(_index.x > playerIndex.x + 1)
        {
            // 너무 오른쪽에 있으면 왼쪽으로 이동
            _index.x -= _chunksToMove;

            float moveDistance = _chunkSize * _chunksToMove;
            transform.position -= new Vector3(moveDistance, 0f, 0f);
        }

        // Y 축 체크
        if(_index.y < playerIndex.y - 1)
        {
            // 너무 아래에 있으면 위로 이동
            _index.y += _chunksToMove;

            float moveDistance = _chunkSize * _chunksToMove;
            transform.position += new Vector3(0f, moveDistance, 0f);
        }
        else if(_index.y > playerIndex.y + 1)
        {
            // 너무 위에 있으면 아래로 이동
            _index.y -= _chunksToMove;

            float moveDistance = _chunkSize * _chunksToMove;
            transform.position -= new Vector3(0f, moveDistance, 0f);
        }
    }
}
