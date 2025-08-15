using Microsoft.EntityFrameworkCore;
using models.car;

class CarDb : DbContext
{

    public CarDb(DbContextOptions<CarDb> options) : base(options) {}

    public DbSet<Car> Cars => Set<Car>();
}