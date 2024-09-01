using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace FetchMatch.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {
        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        string json = $"{{\"actType\":\"p\",\"month\":\"07\",\"year\":\"2024\"}}";
        APIGatewayHttpApiV2ProxyRequest request = new APIGatewayHttpApiV2ProxyRequest()
        {
            Body = json,
            Headers = new Dictionary<string, string>() { { "usrn", "asdfasdfadsf" } }
        };
        try
        {
            var upperCase = function.FunctionHandler(request, context);
            Assert.Equal(200, upperCase.Result.StatusCode);
            Assert.NotNull(upperCase.Result.Body);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
