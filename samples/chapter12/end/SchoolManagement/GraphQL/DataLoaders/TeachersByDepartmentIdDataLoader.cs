using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.DataLoaders;

public class TeachersByDepartmentIdDataLoader : GroupedDataLoader<Guid, Teacher>
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public TeachersByDepartmentIdDataLoader(IDbContextFactory<AppDbContext> dbContextFactory,
        IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<Guid, Teacher>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var teachers = await dbContext.Teachers.Where(x => keys.Contains(x.DepartmentId))
            .ToListAsync(cancellationToken);
        return teachers.ToLookup(x => x.DepartmentId);
    }
}
