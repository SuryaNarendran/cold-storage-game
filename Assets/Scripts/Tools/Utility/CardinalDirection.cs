using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct CardinalDirection
{
    [System.Serializable]
    private enum CardinalDirectionEnum { North, East, South, West }

    [SerializeField] CardinalDirectionEnum directionID;

    public static CardinalDirection north { get => new CardinalDirection(CardinalDirectionEnum.North); }
    public static CardinalDirection east { get => new CardinalDirection(CardinalDirectionEnum.East); }
    public static CardinalDirection south { get => new CardinalDirection(CardinalDirectionEnum.South); }
    public static CardinalDirection west { get => new CardinalDirection(CardinalDirectionEnum.West); }

    private CardinalDirection(CardinalDirectionEnum id)
    {
        directionID = id;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null || !GetType().Equals(obj.GetType()))
            return false;
        CardinalDirection casted = (CardinalDirection)obj;
        return directionID == casted.directionID;
    }

    public override int GetHashCode()
    {
        return (int)directionID;
    }

    public static bool operator == (CardinalDirection a, CardinalDirection b)
    {
        return a.Equals(b);
    }

    public static bool operator != (CardinalDirection a, CardinalDirection b)
    {
        return !(a == b);
    }

    public static implicit operator Vector2Int(CardinalDirection direction)
    {
        switch (direction.directionID)
        {
            case CardinalDirectionEnum.North:
                return Vector2Int.up;
            case CardinalDirectionEnum.East:
                return Vector2Int.right;
            case CardinalDirectionEnum.South:
                return Vector2Int.down;
            case CardinalDirectionEnum.West:
                return Vector2Int.left;
            default:
                throw new System.FormatException("Invalid direction code");
        }
    } 

    public static implicit operator CardinalDirection(Vector2Int vector)
    {
        if (vector == Vector2Int.up) return north;
        if (vector == Vector2Int.right) return east;
        if (vector == Vector2Int.down) return south;
        if (vector == Vector2Int.left) return west;

        throw new System.FormatException("the given Vector2Int cannot be converted to a valid CardinalDirection!");
    }

    public CardinalDirection Opposite
    {
        get
        {
            switch (directionID)
            {
                case CardinalDirectionEnum.North:
                    return south;
                case CardinalDirectionEnum.East:
                    return west;
                case CardinalDirectionEnum.South:
                    return north;
                case CardinalDirectionEnum.West:
                    return east;
                default:
                    throw new System.FormatException("Invalid direction code");
            }
        } 
    }

    public static CardinalDirection FromVector2(Vector2 vector2)
    {
        Vector2Int cardinalizedDirection = Vector2Int.zero;
        if(Mathf.Abs(vector2.x) > Mathf.Abs(vector2.y))
        {
            cardinalizedDirection.x = (int)(vector2.x / Mathf.Abs(vector2.x));
        }
        else
        {
            cardinalizedDirection.y = (int)(vector2.y / Mathf.Abs(vector2.y));
        }

        return (CardinalDirection)cardinalizedDirection;
    }
}
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(CardinalDirection))]
class CardinalDirectionPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //GUIContent propertylabel = new GUIContent(ObjectNames.NicifyVariableName(property.name));
        //Rect fieldPosition = new Rect(position.x + 50, position.y, 50, position.height);
        EditorGUI.PropertyField(position, property.FindPropertyRelative("directionID"), label);

        EditorGUI.EndProperty();
    }
}

#endif //UNITY_EDITOR
