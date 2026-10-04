// Resharper disable UnusedAutoPropertyAccessor.Global

namespace FullPotential.Models.Utilities
{
    public class GenericResponse
    {
        public bool IsSuccess { get; set; }

        public string ErrorCode { get; set; }

        public object Result { get; set; }

        public GenericResponse()
        {
            // Empty ctor
        }

        public GenericResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public GenericResponse(object result)
        {
            IsSuccess = true;
            Result = result;
        }
    }
}
