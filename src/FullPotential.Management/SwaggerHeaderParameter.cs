namespace FullPotential.Management;

using FullPotential.Management.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        if (operation.Parameters.FirstOrDefault(p => p.Name == "userName") == null)
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
