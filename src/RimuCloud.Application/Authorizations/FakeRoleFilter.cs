namespace NewArchive.Application.Common.Authorizations
{
    public static class FakeRoleFilter
    {
        public static readonly List<string> Managers = new List<string>()
        {
            "admin",
            "manage"
        };
    }
}
