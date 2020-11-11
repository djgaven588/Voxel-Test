public class Block
{
    public int InternalID { get; private set; }
    public string InternalName { get; private set; }
    public string Name { get; private set; }
    public bool CanTouch { get; private set; }
    public bool CanCollide { get; private set; }
    public bool HasExtraData { get; private set; }
    public bool CanPlaceOver { get; private set; }

    public Block() { }

    public Block(int internalId, string internalName, string name,  bool canTouch = true, bool canCollide = true, bool canPlaceOver = false, bool hasExtraData = false)
    {
        InternalID = internalId;
        InternalName = internalName;
        Name = name;
        CanTouch = canTouch;
        CanCollide = canCollide;
        HasExtraData = hasExtraData;
        CanPlaceOver = canPlaceOver;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Block b))
        {
            return false;
        }

        // Optimization for a common success case.
        if (ReferenceEquals(this, b))
        {
            return true;
        }

        return InternalID == b.InternalID;
    }

    public static bool operator ==(Block a, Block b)
    {
        if (a is null || b is null)
            return false;
        return a.InternalID == b.InternalID;
    }

    public static bool operator !=(Block a, Block b)
    {
        if (a is null || b is null)
            return true;
        return a.InternalID != b.InternalID;
    }

    public override int GetHashCode()
    {
        return InternalID;
    }
}
