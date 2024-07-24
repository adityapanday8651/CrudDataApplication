using CrudDataApplication.Dto;

namespace CrudDataApplication.Services
{
    public class CommonUtilityHelper
    {
        protected CommonUtilityHelper() { }
        public static ResponseModelDto CreateResponseData(bool status, string message, object? data)
        {
            return new ResponseModelDto
            {
                Status = status,
                Message = message,
                Data = data
            };
        }

    }
}
