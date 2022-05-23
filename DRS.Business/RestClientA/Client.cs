using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using DRS.Entities.Rest;

namespace DRS.Business.RestClientA
{
    public class Client
    {
        private static RestClient restClient;
        private static RestRequest restRequest = new RestRequest();
        static string parameterUrl;

        public static IRestResponse GET(Request request)
        {
            restClient = new RestClient(request.Url);
            restClient.AddDefaultHeader("Content-Type", "application/json");

            if (request.Parameters != null)
            {
                parameterUrl = ToQueryString(request.Parameters);
            }

            RestRequest restRequest = new RestRequest(request.Resource + parameterUrl, Method.GET);
            restRequest.AddHeader("Content-Type", "application/json");

            if (request.Authentication != null)
            {
                restClient.Authenticator = new HttpBasicAuthenticator(request.Authentication.Username, request.Authentication.Password);
            }

            //if (request.Token != null)
            //{
            //    restClient.AddDefaultHeader("Authorization", string.Format(request.Token.Type + " {0}", request.Token.TokenKey));
            //    restRequest.AddHeader("Authorization", string.Format(request.Token.Type + " {0}", request.Token.TokenKey));
            //}

            restRequest.RequestFormat = DataFormat.Json;

            return restClient.Get(restRequest);
        }

        public static IRestResponse POST(Request request)
        {
            restClient = new RestClient(request.Url);
            restClient.AddDefaultHeader("Content-Type", "application/json");

            if (request.Parameters != null)
            {
                parameterUrl = ToQueryString(request.Parameters);
            }

            RestRequest restRequest = new RestRequest(request.Url + request.Resource, Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");

            if (request.Authentication != null)
            {
                restClient.Authenticator = new HttpBasicAuthenticator(request.Authentication.Username, request.Authentication.Password);
            }

            //if (request.Token != null)
            //{
            //    restClient.AddDefaultHeader("Authorization", string.Format(request.Token.Type + " {0}", request.Token.TokenKey));
            //    restRequest.AddHeader("Authorization", string.Format(request.Token.Type + " {0}", request.Token.TokenKey));
            //}

            if (request.Body != null)
            {
                restRequest.AddJsonBody(request.Body);
            }

            restRequest.RequestFormat = DataFormat.Json;

            return restClient.Post(restRequest);
        }

        //public  IRestResponse POST2(Request request)
        //{
        //    restClient = new RestClient(request.Url + request.Resource);

        //    foreach (var oldAuthHeader in restClient.DefaultParameters.ToList())
        //    {
        //        restClient.DefaultParameters.Remove(oldAuthHeader);
        //    }

        //    //restClient.AddDefaultHeader("Content-Type", "application/json");
        //    //restClient.AddDefaultHeader("Accept", "*/*");
        //    //restClient.AddDefaultHeader("Accept-Encoding", "gzip,deflate,br");
        //    //restClient.AddDefaultHeader("Connection", "keep-alive");
        //    //restClient.AddDefaultHeader("Content-Lenght", "");
        //    restClient.AddDefaultHeader("Authorization", string.Format(request.Token.Type + " {0}", request.Token.TokenKey));

        //    if (request.Authentication != null)
        //    {
        //        restClient.Authenticator = new HttpBasicAuthenticator(request.Authentication.Username, request.Authentication.Password);
        //    }

        //    var restRequest = new RestRequest();
        //    IRestResponse response = restClient.ExecuteAsPost(restRequest, "POST");

        //    return response;
        //}

        //public  IRestResponse PATCH(Request request)
        //{
        //    restClient = new RestClient(request.Url);
        //    restClient.AddDefaultHeader("Content-Type", "application/json");

        //    if (request.Parameters != null)
        //    {
        //        parameterUrl = ToQueryString(request.Parameters);
        //    }

        //    Request restRequest = new Request(request.Resource + parameterUrl, Method.PATCH);
        //    restRequest.AddHeader("Content-Type", "application/json");

        //    if (request.Authentication != null)
        //    {
        //        restClient.Authenticator = new HttpBasicAuthenticator(request.Authentication.Username, request.Authentication.Password);
        //    }

        //    if (request.Token != null)
        //    {
        //        restClient.AddDefaultHeader("Authorization", string.Format(request.Token.Type + " {0}", request.Token.TokenKey));
        //        restRequest.AddHeader("Authorization", string.Format(request.Token.Type + " {0}", request.Token.TokenKey));
        //    }

        //    if (request.Body != null)
        //    {
        //        restRequest.AddJsonBody(request.Body);
        //    }

        //    restRequest. = DataFormat.Json;

        //    return restClient.PATCH(restRequest);
        //}
        public static string ToQueryString(object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("/", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}
