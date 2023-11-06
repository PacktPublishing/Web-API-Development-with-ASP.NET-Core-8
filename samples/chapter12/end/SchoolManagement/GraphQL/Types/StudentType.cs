using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.Types;

public class StudentType : ObjectType<Student>
{
    protected override void Configure(IObjectTypeDescriptor<Student> descriptor)
    {
        base.Configure(descriptor);
    }
}
