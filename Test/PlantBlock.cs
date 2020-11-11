public class PlantBlock : Block
{
    private Block[] _canGrowOn;

    public PlantBlock(int internalId, string internalName, string name, bool canCollide, bool canPlaceOver, Block[] canGrowOn) : base(internalId, internalName, name, true, canCollide, canPlaceOver)
    {
        _canGrowOn = canGrowOn;
    }

    public bool CanGrowOn(Block block)
    {
        for (int i = 0; i < _canGrowOn.Length; i++)
        {
            if (_canGrowOn[i] == block)
            {
                return true;
            }
        }

        return false;
    }
}