// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
namespace Sidata.Abstractions.BaseClasses
{
    /// <summary>
    /// generic Result Record sebagai return value standard utk semua return.
    /// T dapat berupa tipe apa pun ... tidak dibatasi
    /// </summary>
    public sealed record Result<T>
    {
        /// <summary>
        /// nilai yang dihasilkan
        /// </summary>
        public T? Value { get; }
        
        /// <summary>
        /// pesan kesalahan
        /// </summary>
        public string? ErrorMessage { get; }
        /// <summary>
        /// full exception object
        /// </summary>
        public Exception? Exception { get; }
        
        /// <summary>
        /// flag success
        /// </summary>
        public bool IsSuccess { get; }
        /// <summary>
        /// flag not success
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// constructor with success value
        /// </summary>
        public Result(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        /// <summary>
        /// constructor with failure value
        /// </summary>
        public Result(string errorMessage, Exception? exception = null)
        {
            ErrorMessage = errorMessage;
            Exception = exception;
            IsSuccess = false;
        }

        /// <summary>
        /// extension for save success value
        /// </summary>
        /// <remarks>
        /// sample: Result&lt;T&gt;.Success(valueOfT);
        /// or Result&lt;T&gt;.Failure("errormessage", ex);
        /// or Result&lt;T&gt;.Error(ex, ':');
        /// </remarks>
        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(string errorMessage, Exception? ex = null) 
                                => new(errorMessage, ex);
        public static Result<T> Error(Exception ex, char splitter = '|')
        {
            return new(ex.Message
                         .Split(splitter)
                         .Last()
                         .Trim(), ex);
        }

        // Pattern matching support
        public TResult Match<TResult>(
            Func<T, TResult> onSuccess,
            Func<string, TResult> onFailure) =>
            IsSuccess ? onSuccess(Value!) : onFailure(ErrorMessage!);

        // Untuk chaining
        public Result<TNext> Bind<TNext>(Func<T, Result<TNext>> next) =>
            IsSuccess ? next(Value!) 
                      : Result<TNext>.Failure(ErrorMessage!, Exception);
        // Tambahan di Result.cs untuk async chaining
        public async Task<Result<TNext>> BindAsync<TNext>(Func<T, Task<Result<TNext>>> next) =>
            IsSuccess ? await next(Value!) 
                      : Result<TNext>.Failure(ErrorMessage!, Exception);
    }
}