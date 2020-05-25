using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class MainBehavior : MonoBehaviour
{
    public GameObject RedGeneralPrefab;
    public GameObject RedAdvisorPrefab;
    public GameObject RedElephantPrefab;
    public GameObject RedChariotPrefab;
    public GameObject RedHorsePrefab;
    public GameObject RedCannonPrefab;
    public GameObject RedSoldierPrefab;
    public GameObject BlackGeneralPrefab;
    public GameObject BlackAdvisorPrefab;
    public GameObject BlackElephantPrefab;
    public GameObject BlackChariotPrefab;
    public GameObject BlackHorsePrefab;
    public GameObject BlackCannonPrefab;
    public GameObject BlackSoldierPrefab;

    public GameObject CurrentColorRed;
    public GameObject CurrentColorBlack;

    Game game = new Game();

    public GameObject TargetPointPrefab;

    List<GameObject> pieceObjects = new List<GameObject>();

    GameObject clickedPieceObject = null;

    List<Position> availableTargetPoints = new List<Position>();

    List<GameObject> targetPointObjects = new List<GameObject>();

    void Start()
    {
        game.Reset();
        UpdatePieces();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (hit2d)
            {
                OnClickObject(hit2d.transform.gameObject);
            }
            else
            {
                OnClickNothing();
            }
        }
    }

    void OnClickObject(GameObject gameObject)
    {
        if (clickedPieceObject == null)
        {
            var piece = gameObject.GetPieceBehavior()?.Piece;
            if (piece != null)
            {
                Debug.Log(string.Format("Clicked piece at ({0},{1}).", piece.Position.X, piece.Position.Y));
                availableTargetPoints = game.GetTargetPositions(piece);
                if (availableTargetPoints.Count > 0)
                {
                    clickedPieceObject = gameObject;
                    UpdateTargetPoints();
                }
                return;
            }
        }
        else
        {
            var targetPosition = gameObject.GetTargetPointBehavior()?.Position;
            if (targetPosition != null)
            {
                var position = (Position)targetPosition;
                Debug.Log(string.Format("Clicked target point at ({0},{1}).", position.X, position.Y));
                game.Move(clickedPieceObject.GetPieceBehavior().Piece, position);
                UpdatePieces();
                return;
            }
        }
        OnClickNothing();
    }

    void OnClickNothing()
    {
        clickedPieceObject = null;
        availableTargetPoints.Clear();
        UpdateTargetPoints();
        Debug.Log("Clicked nothing.");
    }

    void UpdatePieces()
    {
        pieceObjects.ForEach(x => Destroy(x));
        pieceObjects.Clear();
        foreach (var piece in game.Pieces)
        {
            GameObject prefab;
            switch (piece.Type)
            {
                case PieceType.General:
                    if (piece.Color == Core.Color.Red)
                    {
                        prefab = RedGeneralPrefab;
                    }
                    else
                    {
                        prefab = BlackGeneralPrefab;
                    }
                    break;
                case PieceType.Advisor:
                    if (piece.Color == Core.Color.Red)
                    {
                        prefab = RedAdvisorPrefab;
                    }
                    else
                    {
                        prefab = BlackAdvisorPrefab;
                    }
                    break;
                case PieceType.Elephant:
                    if (piece.Color == Core.Color.Red)
                    {
                        prefab = RedElephantPrefab;
                    }
                    else
                    {
                        prefab = BlackElephantPrefab;
                    }
                    break;
                case PieceType.Chariot:
                    if (piece.Color == Core.Color.Red)
                    {
                        prefab = RedChariotPrefab;
                    }
                    else
                    {
                        prefab = BlackChariotPrefab;
                    }
                    break;
                case PieceType.Horse:
                    if (piece.Color == Core.Color.Red)
                    {
                        prefab = RedHorsePrefab;
                    }
                    else
                    {
                        prefab = BlackHorsePrefab;
                    }
                    break;
                case PieceType.Cannon:
                    if (piece.Color == Core.Color.Red)
                    {
                        prefab = RedCannonPrefab;
                    }
                    else
                    {
                        prefab = BlackCannonPrefab;
                    }
                    break;
                case PieceType.Soldier:
                    if (piece.Color == Core.Color.Red)
                    {
                        prefab = RedSoldierPrefab;
                    }
                    else
                    {
                        prefab = BlackSoldierPrefab;
                    }
                    break;
                default:
                    continue;
            }
            var pieceObject = createPiece(prefab, piece);
            pieceObjects.Add(pieceObject);
        }
        UpdateCurrentColor();
        clickedPieceObject = null;
        availableTargetPoints.Clear();
        UpdateTargetPoints();
    }

    void UpdateTargetPoints()
    {
        targetPointObjects.ForEach(x => Destroy(x));
        targetPointObjects.Clear();
        foreach (var point in availableTargetPoints)
        {
            targetPointObjects.Add(createTargetPoint(point));
            Debug.Log(string.Format("Created target point at ({0},{1}).", point.X, point.Y));
        }
    }

    void UpdateCurrentColor()
    {
        CurrentColorRed.SetActive(game.CurrentColor == Core.Color.Red);
        CurrentColorBlack.SetActive(game.CurrentColor == Core.Color.Black);
    }

    GameObject createPiece(GameObject prefab, Piece piece)
    {
        float rotation = 0f;
        if (piece.Color == Core.Color.Black)
        {
            rotation = 180f;
        }
        var pieceObject = Instantiate(
            prefab,
            ConvertPosition(piece.Position, Z_PIECES),
            Quaternion.Euler(0f, 0f, rotation)
        );
        pieceObject.GetPieceBehavior().Piece = piece;
        return pieceObject;
    }

    GameObject createTargetPoint(Position position)
    {
        var targetPointObject = Instantiate(
            TargetPointPrefab,
            ConvertPosition(position, Z_TARGET_POINTS),
            Quaternion.identity
        );
        targetPointObject.GetTargetPointBehavior().Position = position;
        return targetPointObject;
    }

    Vector3 ConvertPosition(Position position, int z)
    {
        return new Vector3((float)(4 - position.X), (float)(-4.53 + position.Y), z);
    }

    const int Z_PIECES = -5;
    const int Z_TARGET_POINTS = -6;
}
