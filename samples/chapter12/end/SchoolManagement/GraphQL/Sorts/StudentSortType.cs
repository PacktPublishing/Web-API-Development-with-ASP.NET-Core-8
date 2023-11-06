using HotChocolate.Data.Sorting;

using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.Sorts;

public class StudentSortType : SortInputType<Student>
{
    protected override void Configure(ISortInputTypeDescriptor<Student> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(x => x.FirstName);
        descriptor.Field(x => x.LastName);
        descriptor.Field(x => x.DateOfBirth);
    }
}
