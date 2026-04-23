using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Reflection;

namespace Platform.API.OpenApi;

public sealed class OpenApiSkipPropertyTransformer : IOpenApiSchemaTransformer
{
    public async Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var propertiesToIgnore = context.JsonTypeInfo.Type.GetProperties()
            .Where(p => p.GetCustomAttributes(typeof(OpenApiIgnoreAttribute), true).Length != 0);

        foreach (var prop in propertiesToIgnore)
        {
            var jsonProperty = context.JsonTypeInfo.Properties
                .FirstOrDefault(p => p.AttributeProvider is PropertyInfo pi && pi == prop);

            if (jsonProperty != null)
            {
                schema.Properties?.Remove(jsonProperty.Name);
            }
        }
    }
}
