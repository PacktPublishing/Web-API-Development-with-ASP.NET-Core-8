using HotChocolate.Data.Filters;

using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.Filters;

public class StudentFilterType : FilterInputType<Student>
{
    protected override void Configure(IFilterInputTypeDescriptor<Student> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(t => t.Id);
        descriptor.Field(t => t.GroupId);
        descriptor.Field(t => t.FirstName).Type<StudentStringOperationFilterInputType>();
        descriptor.Field(t => t.LastName).Type<StudentStringOperationFilterInputType>(); ;
        descriptor.Field(t => t.DateOfBirth);
    }

    // The following configuration uses the BindFieldsImplicitly method to bind all the fields of the Student class to the StudentFilterType class
    // and explicitly ignores some properties.
    //override protected void Configure(IFilterInputTypeDescriptor<Student> descriptor)
    //{
    //    descriptor.BindFieldsImplicitly();
    //    descriptor.Ignore(t => t.Group);
    //    descriptor.Ignore(t => t.Courses);
    //}
}

public class StudentStringOperationFilterInputType : StringOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Operation(DefaultFilterOperations.Equals).Type<StringType>();
        descriptor.Operation(DefaultFilterOperations.Contains).Type<StringType>();
    }
}

public class CustomStudentFilterType : FilterInputType<Student>
{
    protected override void Configure(IFilterInputTypeDescriptor<Student> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Name("CustomStudentFilterInput");
        descriptor.AllowAnd(false).AllowOr(false);
        descriptor.Field(t => t.GroupId).Type<CustomStudentGuidOperationFilterInputType>();
    }
}

public class CustomStudentGuidOperationFilterInputType : UuidOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor.Name("CustomStudentGuidOperationFilterInput");
        descriptor.Operation(DefaultFilterOperations.Equals).Type<IdType>();
        descriptor.Operation(DefaultFilterOperations.In).Type<ListType<IdType>>();
    }
}


