namespace Platform.API;

public static class Permissions
{
    public static class SimulatorModules
    {
        public const string Read = "simulator-modules.read";

        public const string Write = "simulator-modules.write";
    }

    public static class Simulators
    {
        public const string Read = "simulators.read";

        public const string Write = "simulators.write";
    }

    public static class Users
    {
        public const string Read = "users.read";

        public const string WriteOwn = "users.write.own";
    }
}
