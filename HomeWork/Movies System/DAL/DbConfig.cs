namespace Movies_System.DAL
{
    public static class DbConfig
    {
        public static bool IsTestMode { get; private set; } = false;

        public static void EnableTestMode() => IsTestMode = true;

        public static void Reset() => IsTestMode = false;
    }
}
