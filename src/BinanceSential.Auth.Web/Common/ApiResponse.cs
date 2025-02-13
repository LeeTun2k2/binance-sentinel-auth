namespace BinanceSential.Auth.Web.Common;

public class ApiResponse<T>
{
  public int StatusCode { get; set; } = 200;
  public string Message { get; set; } = "Success";
  public IEnumerable<string>? Errors { get; set; }
  public T? Data { get; set; }

  /// <summary>
  /// Default Api Response
  /// </summary>
  public ApiResponse() { }

  /// <summary>
  /// Api Response with status code and message and without data
  /// </summary>
  /// <param name="statusCode"></param>
  /// <param name="message"></param>
  public ApiResponse(int statusCode, string message)
  {
    StatusCode = statusCode;
    Message = message;
  }

  /// <summary>
  /// Api Response with Success and data
  /// </summary>
  /// <param name="data"></param>
  public ApiResponse(T data) => Data = data;
  public ApiResponse(int statusCode, T data)
  {
    StatusCode = statusCode;
    Data = data;
  }

  /// <summary>
  /// Api Response with status code, message, and data
  /// </summary>
  /// <param name="statusCode"></param>
  /// <param name="message"></param>
  /// <param name="data"></param>
  public ApiResponse(int statusCode, string message, T? data)
  {
    StatusCode = statusCode;
    Message = message;
    Data = data;
  }

  /// <summary>
  /// Api Response with status code, message, and errors
  /// </summary>
  /// <param name="statusCode"></param>
  /// <param name="message"></param>
  /// <param name="errors"></param>
  public ApiResponse(int statusCode, string message, IEnumerable<string>? errors)
  {
    StatusCode = statusCode;
    Message = message;
    Errors = errors;
  }
}
