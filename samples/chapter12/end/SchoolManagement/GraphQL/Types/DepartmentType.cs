using SchoolManagement.GraphQL.DataLoaders;
using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.Types;

public class DepartmentType : ObjectType<Department>
{
    protected override void Configure(IObjectTypeDescriptor<Department> descriptor)
    {
        descriptor.Field(x => x.Teachers)
            .Description("This is the list of teachers in the department.")
            .Type<ListType<TeacherType>>()
            .ResolveWith<DepartmentResolvers>(x => x.GetTeachers(default, default, default));
    }
}

public class DepartmentResolvers
{
    public async Task<List<Teacher>> GetTeachers([Parent] Department department,
        TeachersByDepartmentIdDataLoader teachersByDepartmentIdDataLoader, CancellationToken cancellationToken)
    {

        var teachers = await teachersByDepartmentIdDataLoader.LoadAsync(department.Id, cancellationToken);
        return teachers.ToList();
    }
}