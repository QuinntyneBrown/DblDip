using BuildingBlocks.EventStore;

namespace ShootQ.Testing.Builders
{
    public class AppDbContextBuilder
    {
        private AppDbContext _appDbContext;

        public static AppDbContext WithDefaults()
        {
            throw new System.Exception();
        }

        public AppDbContextBuilder()
        {
            throw new System.Exception();
        }

        public AppDbContext Build()
        {
            return _appDbContext;
        }
    }
}
