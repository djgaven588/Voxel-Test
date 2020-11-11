using System;
using System.Numerics;
using System.Runtime.CompilerServices;

public struct Vector3Int
{
    public int x;
    public int y;
    public int z;

    public Vector3Int(int value)
    {
        x = value;
        y = value;
        z = value;
    }

    public Vector3Int(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Int(Vector3 pos)
    {
        x = (int)Math.Floor(pos.X);
        y = (int)Math.Floor(pos.Y);
        z = (int)Math.Floor(pos.Z);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }

    public override bool Equals(object obj)
    {
        Vector3Int? coord = obj as Vector3Int?;

        return !(coord == null || coord.Value.x != x || coord.Value.y != y || coord.Value.z != z);
    }

    public override string ToString()
    {
        return $"X: {x}, Y: {y}, Z: {z}";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y, z);
    }

    /// <summary>
    /// Returns a deterministic hashcode.
    /// WARNING: GetHashCode should be used especially when interacting with networking, use with caution.
    /// </summary>
    /// <returns>A hashcode</returns>
    public int GetDeterministicHashcode()
    {
        unchecked
        {
            int hashCode = 107;
            hashCode = (hashCode * 397) ^ x;
            hashCode = (hashCode * 359) ^ y;
            hashCode = (hashCode * 563) ^ z;
            return hashCode;
        }
    }

    public static Vector3Int operator *(Vector3Int a, int b)
    {
        return new Vector3Int(a.x * b, a.y * b, a.z * b);
    }

    public static Vector3Int operator +(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3Int operator -(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3Int operator -(Vector3Int a)
    {
        return new Vector3Int(-a.x, -a.y, -a.z);
    }

    public static bool operator ==(Vector3Int a, Vector3Int b)
    {
        return !(a.x != b.x || a.y != b.y || a.z != b.z);
    }

    public static bool operator !=(Vector3Int a, Vector3Int b)
    {
        return !(a == b);
    }

    public static Vector3Int Left = new Vector3Int(-1, 0, 0);
    public static Vector3Int Right = new Vector3Int(1, 0, 0);
    public static Vector3Int Up = new Vector3Int(0, 1, 0);
    public static Vector3Int Down = new Vector3Int(0, -1, 0);
    public static Vector3Int Forward = new Vector3Int(0, 0, 1);
    public static Vector3Int Backward = new Vector3Int(0, 0, -1);
}
