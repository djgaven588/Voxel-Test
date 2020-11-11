using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class ChunkOperator
{
    public bool ContinueUsingThreads = true;

    private SemaphoreSlim _threadHolder = new SemaphoreSlim(0, int.MaxValue);

    private DateTime _lastSaveTime = DateTime.UtcNow;

    private readonly ChunkManager _chunks;

    private readonly Block Air;
    private readonly int _seed;

    public readonly ChunkTerrainGenerator _terrainGenerator;

    public enum GlobalChunkState
    {
        Loading,
        Structure,
        StructurePush,
        InitialMeshing,
        Ready
    }

    public GlobalChunkState ChunkState { get { return _state; } private set { _state = value; UpdateStage(); } }
    private GlobalChunkState _state = GlobalChunkState.Loading;

    public bool ChunksReady { get { return ChunkState == GlobalChunkState.Ready; } }

    private int _chunksLoaded = 0;
    private int _chunksStructured = 0;
    private int _chunksStructurePushed = 0;
    private int _chunksMeshed = 0;

    private Chunk[] _chunkRefs;

    public ChunkOperator(ChunkManager chunks, string saveName, string worldName, int seed)
    {
        _terrainGenerator = new ChunkTerrainGenerator();
        _terrainGenerator.Init();

        _chunks = chunks;

        _seed = seed;

        Air = BlockManager.Inst.GetBlockOrDefault("Base/Block/Air");

        int threadCount = 4;//Mathf.Max(Environment.ProcessorCount / 2 - 1, 2);
        Console.WriteLine($"Worker thread count: {threadCount}");
        _threadHolder.Release(threadCount);
    }

    public void Start()
    {
        ChunkState = GlobalChunkState.Loading;
    }

    private void UpdateStage()
    {
        if (ChunkState == GlobalChunkState.Loading)
        {
            _chunkRefs = new Chunk[_chunks.WorldSize.x * _chunks.WorldSize.y * _chunks.WorldSize.z];
            Console.WriteLine("Loading...");
            int index = 0;
            for (int x = 0; x < _chunks.WorldSize.x; x++)
            {
                for (int z = 0; z < _chunks.WorldSize.z; z++)
                {
                    for (int y = 0; y < _chunks.WorldSize.y; y++)
                    {
                        Chunk chunk = new Chunk(new Vector3Int(x, y, z), _chunks, Air);

                        _chunks.Chunks.TryAdd(chunk.Position, chunk);
                        _chunkRefs[index] = chunk;
                        index++;
                        MarkForLoading(chunk);
                    }
                }
            }
        }
        else
        {
            switch (ChunkState)
            {
                case GlobalChunkState.Structure:
                    Console.WriteLine("Structuring...");
                    for (int i = 0; i < _chunkRefs.Length; i++)
                    {
                        MarkForStructureGeneration(_chunkRefs[i]);
                    }
                    Console.WriteLine("All structuring started.");
                    break;
                case GlobalChunkState.StructurePush:
                    Console.WriteLine("Pushing...");
                    for (int i = 0; i < _chunkRefs.Length; i++)
                    {
                        MarkForStructurePush(_chunkRefs[i]);
                    }
                    Console.WriteLine("All pushing started.");
                    break;
                case GlobalChunkState.InitialMeshing:
                    Console.WriteLine("Meshing...");
                    break;
                case GlobalChunkState.Ready:
                    Console.WriteLine("Complete!");
                    break;
                default:
                    break;
            }
        }
    }

    private void LoadChunk(Chunk chunk)
    {
        _terrainGenerator.GenerateBiomes(chunk, _seed);

        int loadedChunks = Interlocked.Increment(ref _chunksLoaded);

        if (loadedChunks >= _chunks.ChunkCount)
        {
            ChunkState = GlobalChunkState.Structure;
        }
    }

    private void StructureChunk(Chunk chunk)
    {
        if (_chunks.GetSurroundingNeighbors(chunk.Position, out Chunk[] structureNeighbors))
        {
            ChunkStructureGenerator.GenerateStructures(chunk, structureNeighbors);

            int structured = Interlocked.Increment(ref _chunksStructured);

            if (structured >= _chunks.ChunkCount)
            {
                ChunkState = GlobalChunkState.StructurePush;
            }
        }
        else
        {
            MarkForStructureGeneration(chunk);
        }
    }

    private void StructurePushChunk(Chunk chunk)
    {
        chunk.PushedChangesUpdate();

        int chunksPushed = Interlocked.Increment(ref _chunksStructurePushed);

        if (chunksPushed >= _chunks.ChunkCount)
        {
            ChunkState = GlobalChunkState.InitialMeshing;
        }
    }

    private void MarkForLoading(Chunk chunk)
    {
        DispatchJob(() => LoadChunk(chunk));
    }

    private void MarkForStructureGeneration(Chunk chunk)
    {
        DispatchJob(() => StructureChunk(chunk));
    }

    private void MarkForStructurePush(Chunk chunk)
    {
        DispatchJob(() => StructurePushChunk(chunk));
    }

    private volatile int _threadCounter = 0;
    private void DispatchJob(Action job)
    {
        async Task Dispatch()
        {
            try
            {
                if (ContinueUsingThreads == false)
                    return;

                await _threadHolder.WaitAsync().ConfigureAwait(false);
                //Profiler.BeginThreadProfiling("Background Job", $"Thread: {_threadCounter}");
                job();
                _threadHolder.Release(1);
                //Profiler.EndThreadProfiling();
                _threadCounter = (_threadCounter + 1) % 2;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        Task.Run(Dispatch);
    }
}
