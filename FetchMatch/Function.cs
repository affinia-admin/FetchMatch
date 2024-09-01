using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FetchMatch.Package;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Intrinsics.X86;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FetchMatch;

public class Function
{
    private const string USR = "usrn";

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest proxyRequest, ILambdaContext context)
    {
        string shortkey = proxyRequest.Headers[USR];
        if (string.IsNullOrEmpty(shortkey)) return CreateResponse(null, 200);
        MatchStoreUtil matchStoreUtil = new MatchStoreUtil();
        return CreateResponse(await matchStoreUtil.RetrieveMatch(shortkey), 200);
    }

    private APIGatewayHttpApiV2ProxyResponse CreateResponse(MatchStore match, int statusCode)
    {
        if (match == null)
        {
            MatchStore match1 = new MatchStore();
            match1.MS = "e";
            return new APIGatewayHttpApiV2ProxyResponse
            {
                Body = JsonConvert(match1),
                StatusCode = 200,
            };
        }
        return new APIGatewayHttpApiV2ProxyResponse
        {
            Body = JsonConvert(match),
            StatusCode = 200,
        };
    }

    private string JsonConvert(MatchStore matchStore)
    {
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(matchStore, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented
        });

        return json;
    }
}
