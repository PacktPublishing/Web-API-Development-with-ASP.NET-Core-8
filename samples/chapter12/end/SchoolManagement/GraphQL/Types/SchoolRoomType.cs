using SchoolManagement.Models;

namespace SchoolManagement.GraphQL.Types;

public class SchoolRoomType : InterfaceType<ISchoolRoom>
{
    protected override void Configure(IInterfaceTypeDescriptor<ISchoolRoom> descriptor)
    {
        descriptor.Name("SchoolRoom");
    }
}

public class ClassroomType : ObjectType<Classroom>
{
    protected override void Configure(IObjectTypeDescriptor<Classroom> descriptor)
    {
        descriptor.Name("Classroom");
        descriptor.Implements<SchoolRoomType>();
    }
}

public class LabRoomType : ObjectType<LabRoom>
{
    protected override void Configure(IObjectTypeDescriptor<LabRoom> descriptor)
    {
        descriptor.Name("LabRoom");
        descriptor.Implements<SchoolRoomType>();
    }
}
