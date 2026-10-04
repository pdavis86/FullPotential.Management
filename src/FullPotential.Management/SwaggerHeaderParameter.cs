using FullPotential.Management.Controllers;
using FullPotential.Management.Features.Security;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace FullPotential.Management;

public class SwaggerHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        var authorizeRequired = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeTokenAttribute>().Any()
            || context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeTokenAttribute>().Any() == true;

        if (authorizeRequired)
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
