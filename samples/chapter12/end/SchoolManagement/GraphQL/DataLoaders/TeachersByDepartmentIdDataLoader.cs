using Microsoft.EntityFrameworkCore;

using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.DataLoaders;

public class TeachersByDepartmentIdDataLoader(
    IDbContextFactory<AppDbContext> dbContextFactory,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null)
    : GroupedDataLoader<Guid, Teacher>(batchScheduler, options)
{
    protected override async Task<ILookup<Guid, Teacher>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var teachers = await dbContext.Teachers.Where(x => keys.Contains(x.DepartmentId))
            .ToListAsync(cancellationToken);
        return teachers.ToLookup(x => x.DepartmentId);
    }
}
