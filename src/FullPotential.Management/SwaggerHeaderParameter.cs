using FullPotential.Management.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FullPotential.Management;

public class SwaggerHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        if (operation.Parameters.FirstOrDefault(p => p.Name == "username") == null)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                },
                Name = AppControllerBase.AuthHeaderName,
                Description = AppControllerBase.AuthHeaderDescription
            });
        }
    }
}
