using DG.Tweening;
using ProjectX.Battle.View.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectX.Battle.View.Action
{
    public class Move : MonoBehaviour
    {
        [SerializeField]
        private Material lineMat;
        [SerializeField]
        private float lineWidth = 0.5f;
        [SerializeField]
        private LayerMask gridLayer;
        [SerializeField]
        private GameObject hiRightObj;
        [SerializeField]
        private MoveCameraController CameraController;

        private Vector3 prepostion;
        private StudentBase student;
        private void Start()
        {
            // 確認用
            //var student = GetComponent<StudentBase>();
            //Act(student);
            Select(new List<Vector2Int>() { new Vector2Int(0,0),
            new Vector2Int(0,1),new Vector2Int(0,2),new Vector2Int(1,0),new Vector2Int(1,2), new Vector2Int(2, 0),
                new Vector2Int(2, 1),new Vector2Int(1, 1),new Vector2Int(2, 2)  });
        }

        private void Update()
        {
            //var currentVel = (transform.position - prepostion)/ Time.deltaTime;
            //student.SetSpeed(currentVel.magnitude);
            //prepostion = transform.position;
            //Debug.Log(currentVel.magnitude.ToString());
            var selectingGrid = HighlightGridUnderMouse();
            if (Mouse.current.rightButton.wasPressedThisFrame && selectingGrid.x != -1)
            {
                var student = GetComponent<StudentBase>();
                Act(student, selectingGrid);
            }
        }

        public void Act(StudentBase student,Vector2Int moveGrid)
        {
            CameraController.Changed(true);
            student.transform.DOMove(new Vector3(moveGrid.x * MapView.TileSize + MapView.TileSize / 2f,
                0f, moveGrid.y * MapView.TileSize + MapView.TileSize / 2f),3f)
                .SetEase(Ease.Unset)
                .OnComplete(() => { student.Idle(); CameraController.Changed(false); });
            student.Move();
            prepostion = transform.position;
            this.student = student;
        }

        public void Select(List<Vector2Int> movableGrids)
        {
            foreach (var grid in movableGrids)
            {
                Vector3 worldPos = MapView.GetWorldPositionFromGrid(grid);

                // 各方向を確認
                Vector2Int[] directions =
                {
                    new Vector2Int(0, -1),  // 下
                    new Vector2Int(0, 1),   // 上
                    new Vector2Int(-1, 0),  // 左
                    new Vector2Int(1, 0)    // 右
                };

                foreach (var direction in directions)
                {
                    Vector2Int adjacentGrid = grid + direction;

                    if (!movableGrids.Contains(adjacentGrid))
                    {
                        DrawLineForDirection(worldPos, direction); // この方向の辺に線を引く
                        Debug.Log($"DrowLine worldPos:{worldPos}adjacentGrid:{adjacentGrid}");
                    }
                }
            }
        }

        void DrawLineForDirection(Vector3 gridBottomLeft, Vector2Int direction)
        {
            float tileSize = MapView.TileSize;
            Vector3 start = gridBottomLeft;
            Vector3 end = gridBottomLeft;

            if (direction.x == 0)  // 上下の辺の場合
            {
                if (direction.y == 1) // 上の辺
                {
                    start += new Vector3(0, 0, tileSize);
                    end += new Vector3(tileSize, 0, tileSize);
                }
                else if (direction.y == -1) // 下の辺
                {
                    end += new Vector3(tileSize, 0, 0);
                }
            }
            else  // 左右の辺の場合
            {
                if (direction.x == 1) // 右の辺
                {
                    start += new Vector3(tileSize, 0, 0);
                    end += new Vector3(tileSize, 0, tileSize);
                }
                // direction.x == -1 の場合（左の辺）は変更なし
            }

            // ここでstartからendまでのLineRendererを用いた線の描画処理
            DrawLine(start, end);
        }

        void DrawLine(Vector3 start, Vector3 end)
        {
            // TODO:辺毎にLineRendererを生成するのではなく、一つのLineRendererで複数の辺を描画するようにする
            GameObject lineObj = new GameObject("Line");
            lineObj.transform.parent = this.transform; 
            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();

            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = 2;
            lineRenderer.material = lineMat;

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }

        private Vector2Int HighlightGridUnderMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // カメラからマウスの位置へのレイを作成
            RaycastHit hit;

            // レイがgridLayerで指定したLayerのオブジェクトと交差した場合
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridLayer))
            {
                Vector3 hitPoint = hit.point;  // 交点のワールド座標を取得

                // ワールド座標をグリッドの座標に変換
                int gridX = Mathf.FloorToInt(hitPoint.x / MapView.TileSize);
                int gridY = Mathf.FloorToInt(hitPoint.z / MapView.TileSize);  // Y軸方向ではなく、Z軸方向の座標を使用していることに注意

                Debug.Log($"Grid Position: ({gridX}, {gridY})");

                // ここでハイライト処理を実装（例えば、該当グリッドを青くするなど）
                HighlightGrid(gridX, gridY);

                return new Vector2Int(gridX, gridY);
            }
            return new Vector2Int(-1, -1);

        }

        void HighlightGrid(int x, int y)
        {
            Vector3 targetPosition = MapView.GetWorldPositionFromGrid(new Vector2Int(x, y));
            targetPosition.y = 0.01f;
            targetPosition.x += MapView.TileSize / 2f;
            targetPosition.z += MapView.TileSize / 2f;
            hiRightObj.transform.position = targetPosition;
        }

    }

}

