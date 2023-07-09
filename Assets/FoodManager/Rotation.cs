using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CRotation : ScriptableObject
{
    public static Dir GetNextDir(Dir dir)
    {
        //顺时针
        switch (dir)
        {
            default:
            case Dir.Down: return Dir.Left;
            case Dir.Left: return Dir.Up;
            case Dir.Up: return Dir.Right;
            case Dir.Right: return Dir.Down;
        }
    }

    //朝向
    public static Vector2Int GetDirForwardVector(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return new Vector2Int(0, -1);
            case Dir.Left: return new Vector2Int(-1, 0);
            case Dir.Up: return new Vector2Int(0, +1);
            case Dir.Right: return new Vector2Int(+1, 0);
        }
    }

    //根据位置变化确定旋转方向
    public static Dir GetDir(Vector2Int from, Vector2Int to)
    {
        if (from.x < to.x)
        {
            return Dir.Right;
        }
        else
        {
            if (from.x > to.x)
            {
                return Dir.Left;
            }
            else
            {
                if (from.y < to.y)
                {
                    return Dir.Up;
                }
                else
                {
                    return Dir.Down;
                }
            }
        }
    }

    public enum Dir
    {
        Down,
        Left,
        Up,
        Right,
    }

    //获取旋转角度
    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return 0;
            case Dir.Left: return 90;
            case Dir.Up: return 180;
            case Dir.Right: return 270;
        }
    }

    public uint[,] GetFoodNewShape(uint vShape, Dir dir)
    {

        if(vShape == 1)
        {
            switch (dir)
            {
                default:
                case Dir.Down:                                       
                case Dir.Up:
                case Dir.Left:
                case Dir.Right:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 0, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat;
            }
        }
        else if (vShape == 2)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Up:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 0, 0, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                case Dir.Right:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat1;
            }
        }
        else if (vShape == 3)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Up:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                case Dir.Right:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat1;
            }
        }
        else if (vShape == 4)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat;
                case Dir.Up:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat1;

                case Dir.Left:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat3;
            }
        }
        else if (vShape == 5)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat3;
                case Dir.Left:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat;
                case Dir.Up:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat1;
            }
        }
        else if (vShape == 6)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 0, 0, 1, 0}};
                    return shapeMat;
                case Dir.Left:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 1, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat1;
                case Dir.Up:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 0, 1, 1, 0}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 0, 1, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat3;
            }
        }
        else if (vShape == 7)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 1, 0, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 0, 1, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat1;
                case Dir.Up:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 0, 1, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 0, 1, 0}};
                    return shapeMat3;
            }
        }
        else if (vShape == 8)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Up:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 1, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                case Dir.Right:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 0, 0, 0},
                                         { 1, 1, 1, 0}};
                    return shapeMat1;
            }
        }
        else if (vShape == 9)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 1, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 1, 1, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat1;
                case Dir.Up:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 1, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 1, 1, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat3;
            }
        }

        else if (vShape == 10)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Up:
                    uint[,] shapeMat = { { 1, 0, 0, 0},
                                         { 1, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                case Dir.Right:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 0, 0, 0},
                                         { 1, 1, 1, 1}};
                    return shapeMat1;
            }
        }
        else if (vShape == 11)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 1, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 1, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat1;
                case Dir.Up:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 0, 1, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 1, 1, 0}};
                    return shapeMat3;
            }
        }
        else if (vShape == 12)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Left:
                case Dir.Up:
                case Dir.Right:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat3;
            }
        }
        else if (vShape == 13)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Up:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 1, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                case Dir.Right:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 0, 1, 1, 0}};
                    return shapeMat1;
            }
        }
        else if (vShape == 14)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 1, 0},
                                         { 0, 0, 1, 0}};
                    return shapeMat;
                case Dir.Left:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 1, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 1, 1, 0, 0}};
                    return shapeMat1;
                case Dir.Up:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 1, 1, 0}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat3 = { { 0, 0, 0, 0},
                                         { 1, 1, 0, 0 },
                                         { 1, 0, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat3;
            }
        }
        else if (vShape == 15)
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 1, 1, 1, 1},
                                         { 0, 1, 0, 0}};
                    return shapeMat;
                case Dir.Left:
                    uint[,] shapeMat1 = { { 0, 1, 0, 0},
                                         { 1, 1, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 0, 1, 0, 0}};
                    return shapeMat1;
                case Dir.Up:
                    uint[,] shapeMat2 = { { 0, 0, 0, 0},
                                         { 0, 0, 0, 0 },
                                         { 0, 0, 1, 0},
                                         { 1, 1, 1, 1}};
                    return shapeMat2;
                case Dir.Right:
                    uint[,] shapeMat3 = { { 1, 0, 0, 0},
                                         { 1, 0, 0, 0 },
                                         { 1, 1, 0, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat3;
            }
        }
        else
        {
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Up:
                    uint[,] shapeMat = { { 0, 0, 0, 0},
                                         { 1, 1, 0, 0 },
                                         { 0, 1, 0, 0},
                                         { 0, 1, 1, 0}};
                    return shapeMat;
                case Dir.Left:
                case Dir.Right:
                    uint[,] shapeMat1 = { { 0, 0, 0, 0},
                                         { 0, 0, 1, 0 },
                                         { 1, 1, 1, 0},
                                         { 1, 0, 0, 0}};
                    return shapeMat1;
            }
        }
    }

    //public uint[,] GetFoodShape(uint vShape, Dir dir)
    //{

    //}
}
