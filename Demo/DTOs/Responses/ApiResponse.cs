using System.Text.Json.Serialization;

namespace Demo.DTOs.Responses;

public class ApiResponse
{
    public int Status { get; set; }
    public string Message { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Object Data { get; set; }
    
    public ApiResponse(int status, string message, Object data)
    {
        Status = status;
        Message = message;
        Data = data;
    }

    
  
}