using UnityEngine;

public class WarehouseGameManager : MonoBehaviour
{
    private Vector2Int playerPosition;

    private Vector2Int boxPosition;

    private Vector2Int goalPosition;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayer(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayer(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayer(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayer(Vector2Int.right);
        }

        // �S�[������
        CheckGoalReached();
    }

    void InitializeGame()
    {
        playerPosition = new Vector2Int(0, 0);
        boxPosition = new Vector2Int(1, 0);
        goalPosition = new Vector2Int(2, 0);

        DebugLogGameState();
    }

    void MovePlayer(Vector2Int direction)
    {
        Vector2Int newPosition = playerPosition + direction;

        // �ړ��悪�n�ʓ����{�b�N�X���Ȃ��ꍇ
        if (IsWithinGround(newPosition) && newPosition != boxPosition)
        {
            playerPosition = newPosition;
        }
        // �ړ���Ƀ{�b�N�X������ꍇ
        else if (newPosition == boxPosition)
        {
            Vector2Int newBoxPosition = boxPosition + direction;

            // �{�b�N�X�̈ړ��悪�n�ʓ����{�b�N�X���Ȃ��ꍇ
            if (IsWithinGround(newBoxPosition) && newBoxPosition != playerPosition)
            {
                boxPosition = newBoxPosition;
                playerPosition = newPosition;
            }
        }

        DebugLogGameState();
    }

    void CheckGoalReached()
    {
        if (playerPosition == goalPosition)
        {
            Debug.Log("Goal reached! Game Over.");
        }
    }

    bool IsWithinGround(Vector2Int position)
    {
        // ���Őݒ�
        int groundSizeX = 5;
        int groundSizeZ = 5;

        return Mathf.Abs(position.x) < groundSizeX / 2 && Mathf.Abs(position.y) < groundSizeZ / 2;
    }

    void DebugLogGameState()
    {
        Debug.Log($"Player Position: {playerPosition}, Box Position: {boxPosition}, Goal Position: {goalPosition}");
    }
}