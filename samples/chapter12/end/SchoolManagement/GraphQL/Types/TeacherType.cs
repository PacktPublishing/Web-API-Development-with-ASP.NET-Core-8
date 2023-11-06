using SchoolManagement.GraphQL.DataLoaders;
using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.Types;

public class TeacherType : ObjectType<Teacher>
{
    protected override void Configure(IObjectTypeDescriptor<Teacher> descriptor)
    {
        //base.Configure(descriptor);
        descriptor.Field(x => x.Department)
            .Name("department") // This configuration can be omitted if the name of the field is the same as the name of the property.
            .Description("This is the department to which the teacher belongs.")
            //.Resolve(async context =>
            //{
            //    var dbContextFactory = context.Service<IDbContextFactory<AppDbContext>>();
            //    await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            //    var department = await dbContext.Departments
            //        .FindAsync(context.Parent<Teacher>().DepartmentId);
            //    return department;
            //});
            .ResolveWith<TeacherResolvers>(x => x.GetDepartment(default, default, default));
    }
}

public class TeacherResolvers
{
    // The following code is commented out because it is replaced by the data loader.
    //public async Task<Department> GetDepartment([Parent] Teacher teacher, [Service] IDbContextFactory<AppDbContext> dbContextFactory)
    //{
    //    await using var dbContext = await dbContextFactory.CreateDbContextAsync();
    //    var department = await dbContext.Departments.FindAsync(teacher.DepartmentId);
    //    return department;
    //}

    public async Task<Department> GetDepartment([Parent] Teacher teacher,
        DepartmentByTeacherIdBatchDataLoader departmentByTeacherIdBatchDataLoader, CancellationToken cancellationToken)
    {
        var department = await departmentByTeacherIdBatchDataLoader.LoadAsync(teacher.DepartmentId, cancellationToken);
        return department;
    }
}
