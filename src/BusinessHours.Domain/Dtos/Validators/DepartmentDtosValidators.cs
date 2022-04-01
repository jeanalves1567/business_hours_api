using BusinessHours.Domain.Dtos.Departments;
using BusinessHours.Domain.Errors;

namespace BusinessHours.Domain.Dtos.Validators
{
    public static class DepartmentDtosValidators
    {
        public static void Validate(this DepartmentCreateDto obj)
        {
            if (string.IsNullOrEmpty(obj.ExternalId)) throw new MissingBodyParamException("externalId");
            if (string.IsNullOrEmpty(obj.Name)) throw new MissingBodyParamException("name");
            if (string.IsNullOrEmpty(obj.Type)) throw new MissingBodyParamException("type");
        }
    }
}
