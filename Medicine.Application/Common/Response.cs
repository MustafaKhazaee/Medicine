
namespace Medicine.Application.Common;

public record Response<T>
{
    private Response() {}

    public T Data { get; private init; }
    public bool Result { get; private init; }
    public string? Message { get; private init; }

    public static Response<T> Create(T data, bool result, string? message = default)
        => result ? Success(data, message) : Fail(data, message);

    public static Response<T> Success(T data, string? message = default)
        => new()
        {
            Data = data,
            Result = true,
            Message = message
        };

    public static Response<T> Fail(T data, string? message = default)
        => new()
        {
            Data = data,
            Result = false,
            Message = message
        };
}
