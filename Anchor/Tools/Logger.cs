using BepInEx.Logging;

namespace Anchor.Tools
{
    public static class Logging<T> where T : class
    {
        private static ManualLogSource logger;

        private static ManualLogSource Instance => logger ??= Logger.CreateLogSource(typeof(T).Name);

        public static void LogInfo(string message) => Instance.LogInfo(message);

        public static void LogWarning(string message) => Instance.LogWarning(message);

        public static void LogError(string message) => Instance.LogError(message);

        public static void LogFatal(object message) => Instance.LogFatal(message);
    }
}