using Modding;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

public class CoreMod : IMod
{
    public ModVersion Version => new ModVersion(1, 0, 0);

    public string Name => "Base";

    public string[] LoadBeforeDependencies => new string[0];

    public string[] LoadAfterDependencies => new string[0];

    public void Init()
    {

    }

    public Block[] LoadBlocks(BlockManager alreadyLoadedBlocks, int startingId)
    {
        List<Block> blockList = new List<Block>();

        blockList.Add(new Block(startingId++, "Base/Block/Dirt", "Dirt"));

        Block grass = new Block(startingId++, "Base/Block/Grass", "Grass");
        blockList.Add(grass);

        blockList.Add(new Block(startingId++, "Base/Block/Stone", "Stone"));

        blockList.Add(new Block(startingId++, "Base/Block/Oak Wood", "Oak Wood"));

        blockList.Add(new Block(startingId++, "Base/Block/Oak Leaves", "Oak Leaves"));
        blockList.Add(new Block(startingId++, "Base/Block/Sand", "Sand"));
        blockList.Add(new Block(startingId++, "Base/Block/Sandstone", "Sandstone"));
        blockList.Add(new Block(startingId++, "Base/Block/Ice", "Ice"));

        blockList.Add(new PlantBlock(startingId++, "Base/Block/GrassPlant", "Grass", false, true, new Block[] { grass }));

        blockList.Add(new PlantBlock(startingId++, "Base/Block/Poppy", "Poppy", false, false, new Block[] { grass }));

        blockList.Add(new PlantBlock(startingId++, "Base/Block/Dandelion", "Dandelion", false, false, new Block[] { grass }));

        Block waterBlock = new Block(startingId++, "Base/Block/Water", "Water", true, true, true);
        blockList.Add(waterBlock);

        return blockList.ToArray();
    }

    public Biome[] LoadBiomes(BlockManager alreadyLoadedBlocks)
    {
        List<Biome> biomes = new List<Biome>();

        Block grass = alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/Grass");
        Block dirt = alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/Dirt");
        Block stone = alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/Stone");
        Block water = alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/Water");
        Block air = alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/Air");

        biomes.Add(new Biome()
        {
            BiomeName = "Mountains",
            HumidityRange = new Vector2(0.35f, 0.8f),
            TemperatureRange = new Vector2(0.15f, 0.6f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.05,
                        Amplitude = 3.75f,
                        Exponential = 2,
                        ExponentialDownscale = 4,
                        OctaveMultiplier = 1,
                        OctaveOffset = 50
                    }
                },
            SurfaceBlock = grass,
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 6,
            UndergroundBlock = stone
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Plains",
            HumidityRange = new Vector2(0.075f, 0.30f),
            TemperatureRange = new Vector2(0.20f, 0.7f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.01,
                        Amplitude = 7,
                        Exponential = 1,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 1,
                        OctaveOffset = 45
                    },
                    new Biome.NoiseSet()
                    {
                        Scale = 0.05,
                        Amplitude = 3,
                        Exponential = 2,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 0.1,
                        OctaveOffset = 0
                    }
                },
            SurfaceBlock = grass,
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 6,
            UndergroundBlock = stone
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Forest",
            HumidityRange = new Vector2(0.25f, 1f),
            TemperatureRange = new Vector2(-0.1f, 0.4f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.015,
                        Amplitude = 3,
                        Exponential = 5,
                        ExponentialDownscale = 6,
                        OctaveMultiplier = 0.5f
                    },
                    new Biome.NoiseSet()
                    {
                        Scale = 0.035,
                        Amplitude = 7,
                        Exponential = 1,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 0.5f
                    },
                    new Biome.NoiseSet()
                    {
                        Scale = 0.1,
                        Amplitude = 1,
                        Exponential = 2,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 1f
                    },
                    new Biome.NoiseSet()
                    {
                        Scale = 0.001,
                        Amplitude = 1,
                        Exponential = 3,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 1f,
                        OctaveOffset = 40
                    }
                },
            SurfaceBlock = grass,
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 6,
            UndergroundBlock = stone
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Deep Forest",
            HumidityRange = new Vector2(0.3125f, 1f),
            TemperatureRange = new Vector2(0.2f, 0.7f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.0075,
                        Amplitude = 3,
                        Exponential = 5,
                        ExponentialDownscale = 6,
                        OctaveMultiplier = 0.5f
                    },
                    new Biome.NoiseSet()
                    {
                        Scale = 0.02,
                        Amplitude = 7,
                        Exponential = 1,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 1f
                    },
                    new Biome.NoiseSet()
                    {
                        Scale = 0.05,
                        Amplitude = 2,
                        Exponential = 2,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 1f
                    },
                    new Biome.NoiseSet()
                    {
                        Scale = 0.001,
                        Amplitude = 2,
                        Exponential = 3,
                        ExponentialDownscale = 9,
                        OctaveMultiplier = 1f,
                        OctaveOffset = 40
                    }
                },
            SurfaceBlock = grass,
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 6,
            UndergroundBlock = stone
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Rainforest",
            HumidityRange = new Vector2(0.5125f, 1f),
            TemperatureRange = new Vector2(0.65f, 1f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.005,
                        Amplitude = 4,
                        Exponential = 2,
                        ExponentialDownscale = 5,
                        OctaveMultiplier = 1,
                        OctaveOffset = 72
                    }
                },
            SurfaceBlock = dirt,
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 6,
            UndergroundBlock = stone
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Tundra",
            HumidityRange = new Vector2(-0.2f, 0.5f),
            TemperatureRange = new Vector2(-0.3f, 0.4f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.0025,
                        Amplitude = 5,
                        Exponential = 2,
                        ExponentialDownscale = 3,
                        OctaveMultiplier = 1,
                        OctaveOffset = 55
                    }
                },
            SurfaceBlock = BlockManager.Inst.GetBlockOrDefault("Base/Block/Ice"),
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 3,
            UndergroundBlock = stone
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Desert",
            HumidityRange = new Vector2(-2f, 0.4f),
            TemperatureRange = new Vector2(0.35f, 1f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.02,
                        Amplitude = 5,
                        Exponential = 2,
                        ExponentialDownscale = 3,
                        OctaveMultiplier = 1,
                        OctaveOffset = 70
                    }
                },
            SurfaceBlock = BlockManager.Inst.GetBlockOrDefault("Base/Block/Sand"),
            SubsurfaceBlock = BlockManager.Inst.GetBlockOrDefault("Base/Block/Sand"),
            SubsurfaceDepth = 5,
            UndergroundBlock = BlockManager.Inst.GetBlockOrDefault("Base/Block/Sandstone"),
            Color = Color.Yellow
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Savanna",
            HumidityRange = new Vector2(0.1f, 0.325f),
            TemperatureRange = new Vector2(0.5f, 0.95f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 0.0125,
                        Amplitude = 5,
                        Exponential = 1,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 1,
                        OctaveOffset = 62
                    }
                },
            SurfaceBlock = dirt,
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 3,
            UndergroundBlock = stone
        });
        biomes.Add(new Biome()
        {
            BiomeName = "Ocean",
            HumidityRange = new Vector2(0.20f, 0.525f),
            TemperatureRange = new Vector2(0.15f, 1f),
            NoiseInformation = new Biome.NoiseSet[]
                {
                    new Biome.NoiseSet()
                    {
                        Scale = 1,
                        Amplitude = 1,
                        Exponential = 1,
                        ExponentialDownscale = 1,
                        OctaveMultiplier = 0,
                        OctaveOffset = 40
                    }
                },
            SurfaceBlock = BlockManager.Inst.GetBlockOrDefault("Base/Block/Sand"),
            SubsurfaceBlock = dirt,
            SubsurfaceDepth = 3,
            UndergroundBlock = stone
        });

        return biomes.ToArray();
    }

    public FloralEntry[] LoadFloral(BlockManager alreadyLoadedBlocks, BiomeManager biomes)
    {
        List<FloralEntry> potentialFloral = new List<FloralEntry>();

        potentialFloral.Add(new FloralEntry()
        {
            Floral = (PlantBlock)alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/GrassPlant"),
            HeightRange = new Vector2(55, 115),
            HumidityRange = new Vector2(0.3f, 1f),
            TemperatureRange = new Vector2(0.25f, 0.9f),
            SpawnChance = 0.9f
        });

        potentialFloral.Add(new FloralEntry()
        {
            Floral = (PlantBlock)alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/Dandelion"),
            HeightRange = new Vector2(60, 85),
            HumidityRange = new Vector2(0.3f, 1f),
            TemperatureRange = new Vector2(0.3f, 0.8f),
            SpawnChance = 0.1f
        });

        potentialFloral.Add(new FloralEntry()
        {
            Floral = (PlantBlock)alreadyLoadedBlocks.GetBlockOrDefault("Base/Block/Poppy"),
            HeightRange = new Vector2(62, 80),
            HumidityRange = new Vector2(0.2f, 0.7f),
            TemperatureRange = new Vector2(0.45f, 0.9f),
            SpawnChance = 0.1f
        });

        return potentialFloral.ToArray();
    }

    public Structure[] LoadStructures(BlockManager blocks, BiomeManager biomes)
    {
        List<Structure> structures = new List<Structure>();

        structures.Add(new TreeStructure(blocks, biomes));

        return structures.ToArray();
    }
}
