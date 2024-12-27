using System;
using System.Linq;
using VTest.Services;
using Unity.Entities;

namespace VTest;

internal static class Core
{
    public static World Server { get; } = GetServerWorld() ?? throw new Exception("There is no Server world (yet)...");
    public static EntityManager EntityManager => Server.EntityManager;

    public static DiceService DiceService { get; internal set;}
    
    public static bool hasInitialized = false;

    public static void Initialize()
    {
        if (hasInitialized) return;

        DiceService = new DiceService();
        hasInitialized = true;
    }

    static World GetServerWorld()
    {
        return World.s_AllWorlds.ToArray().FirstOrDefault(world => world.Name == "Server");
    }
}