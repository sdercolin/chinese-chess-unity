using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class MainBehavior : MonoBehaviour
{
    Game Game = new Game();

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

    List<GameObject> pieceObjects = new List<GameObject>();

    void Start()
    {
        Game.Reset();
        UpdatePieces();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
            }
            var piece = clickedGameObject.GetPieceBehavior()?.Piece;
            Debug.Log(piece?.Type);
        }
    }

    void OnClickPiece(GameObject pieceObject)
    {

    }

    void UpdatePieces()
    {
        pieceObjects.ForEach(x => Destroy(x));
        pieceObjects.Clear();
        foreach (var piece in Game.Pieces)
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
    }

    GameObject createPiece(GameObject prefab, Piece piece)
    {
        float rotation = 0f;
        if (piece.Color == Core.Color.Black)
        {
            rotation = 180f;
        }
        var position = piece.Position;
        var pieceObject = Instantiate(
            prefab,
            new Vector3((float)(-4 + position.X), (float)(4.43 - position.Y), Z_PIECES),
            Quaternion.Euler(0f, 0f, rotation)
        );
        pieceObject.GetPieceBehavior().Piece = piece;
        return pieceObject;
    }

    const int Z_PIECES = -5;
}
