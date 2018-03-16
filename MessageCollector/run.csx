#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    string requestBody = new StreamReader(req.Body).ReadToEnd();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    string name = data?.name;
    string cellPhone = data?.cellPhone;
    string message = data?.message;
    string uuid = System.Guid.NewGuid().ToString();

    List<String> errors = new List<String>();
    string errorString = "";
    bool isValid = true;

    if (name == null) {
        errors.Add("name");
    }

    if (cellPhone == null) {
        errors.Add("cellPhone");
    }

    if (message == null) {
        errors.Add("message");
    }
    if (errors.Count > 0) {
        isValid = false;
        errorString = String.Join(", ", errors.ToArray());
    }

    return isValid
        ? (ActionResult)new OkObjectResult($"Message to {name} on {cellPhone}: {message}")
        : new BadRequestObjectResult($"Missing required parameters: {errorString}");
}
