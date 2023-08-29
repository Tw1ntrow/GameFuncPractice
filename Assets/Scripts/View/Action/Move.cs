using DG.Tweening;
using ProjectX.Battle.View.Unit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


        // 1�O���b�h������̈ړ�����
        private float speedPerGrid = 0.75f;
        private void Start()
        {
            //Select(new List<Vector2Int>() { new Vector2Int(0,0),
            //new Vector2Int(0,1),new Vector2Int(0,2),new Vector2Int(1,0),new Vector2Int(1,2), new Vector2Int(2, 0),
            //    new Vector2Int(2, 1),new Vector2Int(1, 1),new Vector2Int(2, 2)  });
        }

        private void Update()
        {
            var selectingGrid = HighlightGridUnderMouse();
            // �E�N���b�N�ňړ�
            if (Mouse.current.rightButton.wasPressedThisFrame && selectingGrid.x != -1)
            {
                var student = GetComponent<StudentBase>();
                Act(student, new Vector2Int[] { selectingGrid,new Vector2Int(1,3)});
            }
        }

        /// <summary>
        /// �ړ����郆�j�b�g��Transform�𐧌䂷��
        /// �E�N���b�N�����O���b�h�̌����ɐU��������ړ�
        /// </summary>
        /// <param name="student"></param>
        /// <param name="routeGrid"></param>
        public void Act(StudentBase student,Vector2Int[] routeGrid)
        {
            student.transform.DOLookAt(MapView.GetWorldPositionCenterFromGrid(routeGrid[0]),1).
                OnComplete(Move);
            student.Turn();

            void Move()
            {
                student.Move();
                List<Vector3> routePos = new List<Vector3>();

                foreach (var grid in routeGrid)
                {
                    routePos.Add(MapView.GetWorldPositionCenterFromGrid(grid));
                }

                CameraController.Changed(true);
                student.transform.DOPath(routePos.ToArray(), routePos.Count * speedPerGrid)
                    .SetLookAt(0.1f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => { student.Idle(); CameraController.Changed(false); });
            }
        }

        public void Select(List<Vector2Int> movableGrids)
        {
            foreach (var grid in movableGrids)
            {
                Vector3 worldPos = MapView.GetWorldPositionFromGrid(grid);

                // �e�������m�F
                Vector2Int[] directions =
                {
                    new Vector2Int(0, -1),  // ��
                    new Vector2Int(0, 1),   // ��
                    new Vector2Int(-1, 0),  // ��
                    new Vector2Int(1, 0)    // �E
                };

                foreach (var direction in directions)
                {
                    Vector2Int adjacentGrid = grid + direction;

                    if (!movableGrids.Contains(adjacentGrid))
                    {
                        DrawLineForDirection(worldPos, direction); // ���̕����̕ӂɐ�������
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

            if (direction.x == 0)  // �㉺�̕ӂ̏ꍇ
            {
                if (direction.y == 1) // ��̕�
                {
                    start += new Vector3(0, 0, tileSize);
                    end += new Vector3(tileSize, 0, tileSize);
                }
                else if (direction.y == -1) // ���̕�
                {
                    end += new Vector3(tileSize, 0, 0);
                }
            }
            else  // ���E�̕ӂ̏ꍇ
            {
                if (direction.x == 1) // �E�̕�
                {
                    start += new Vector3(tileSize, 0, 0);
                    end += new Vector3(tileSize, 0, tileSize);
                }
                // direction.x == -1 �̏ꍇ�i���̕Ӂj�͕ύX�Ȃ�
            }

            // ������start����end�܂ł�LineRenderer��p�������̕`�揈��
            DrawLine(start, end);
        }

        void DrawLine(Vector3 start, Vector3 end)
        {
            // TODO:�Ӗ���LineRenderer�𐶐�����̂ł͂Ȃ��A���LineRenderer�ŕ����̕ӂ�`�悷��悤�ɂ���
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // �J��������}�E�X�̈ʒu�ւ̃��C���쐬
            RaycastHit hit;

            // ���C��gridLayer�Ŏw�肵��Layer�̃I�u�W�F�N�g�ƌ��������ꍇ
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridLayer))
            {
                Vector3 hitPoint = hit.point;  // ��_�̃��[���h���W���擾

                // ���[���h���W���O���b�h�̍��W�ɕϊ�
                int gridX = Mathf.FloorToInt(hitPoint.x / MapView.TileSize);
                int gridY = Mathf.FloorToInt(hitPoint.z / MapView.TileSize);  // Y�������ł͂Ȃ��AZ�������̍��W���g�p���Ă��邱�Ƃɒ���

                // �����Ńn�C���C�g�����������i�Ⴆ�΁A�Y���O���b�h�������Ȃǁj
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

