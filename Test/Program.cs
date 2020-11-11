using Modding;
using System;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockManager.Inst.Init();
            StructureManager.Inst.Init();

            ModManager.Inst.LoadAllMods(new string[] { });

            ChunkStructureGenerator.Init();
            ChunkManager chunkManager = new ChunkManager(0, "World", "World", false);
            while (true)
            {
                Thread.Sleep(10);
            }
        }
    }
}
