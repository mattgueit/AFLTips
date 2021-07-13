using System;
using System.Threading.Tasks;
using AFLTips.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AFLTips.Server.Handlers
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> CatchExceptionsAsync(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func();
            }
            catch (MissingDataException ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> CatchExceptionsAsync<TRequest>(Func<TRequest, Task<IActionResult>> func, TRequest request)
        {
            try
            {
                return await func(request);
            }
            catch (MissingDataException ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
