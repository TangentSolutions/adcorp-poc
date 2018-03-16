#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public class Message {
    public String Name { get; set; }
    public String Content { get; set; }
    public String CellPhone { get; set; }
    public String UUID { get; set; }
}

public class Response {
    public Object Return { get; set; }
    public Message Message { get; set; } 
}


public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    string requestBody = new StreamReader(req.Body).ReadToEnd();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    string name = data?.name;
    string cellPhone = data?.cellPhone;
    string messageBody = data?.message;

    List<String> errors = new List<String>();
    string errorString = "";

    if (name == null) {
        errors.Add("name");
    }

    if (cellPhone == null) {
        errors.Add("cellPhone");
    }

    if (messageBody == null) {
        errors.Add("message");
    }

    if (errors.Count == 0) {
        Message message = new Message() { Content = messageBody, Name = name, CellPhone = cellPhone, UUID = System.Guid.NewGuid().ToString()};
        // return JsonConvert.SerializeObject(new Response() { Message = message, Return = new OkObjectResult("") });
        string returnValue = JsonConvert.SerializeObject(new Response() { Message = message, Return = message });
        return (ActionResult)new OkObjectResult($"{returnValue}");
    } else {
        errorString = String.Join(", ", errors.ToArray());
        string returnValue = JsonConvert.SerializeObject(new Response() { Return = errorString });
        // return JsonConvert.SerializeObject(new Response() { Message = message, Return = new BadRequestObjectResult($"{errorString}") });
        return new BadRequestObjectResult($"Missing required parameters: {errorString}");
    }
}
