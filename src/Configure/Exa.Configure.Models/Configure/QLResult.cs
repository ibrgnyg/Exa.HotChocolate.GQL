using Exa.Configure.Models.Enums;
using Microsoft.Extensions.Logging;

namespace Exa.Configure.Models.Configure
{
    public class QLResult
    {
        private readonly ILogger<QLResult> _logger;

        public QLResult(ILogger<QLResult> logger)
        {
            _logger = logger;
        }

        public QLResult() { }

        public string Message { get; set; } = string.Empty;
        public bool IsError { get; set; } = false;
        public QLResultType ResultType { get; set; } = QLResultType.Success;


        public void SetExceptionEvent(Exception ex, string mess = "error")
        {
            Message = mess;
            ResultType = QLResultType.Exception;
            _logger.LogError(ex.ToString());
        }

        public QLResult SetErrorEvent(string localizerMessage, QLResultType qLResultType = QLResultType.Error, string mess = "")
        {
            Message = mess;
            ResultType = qLResultType;
            return this;
        }

        public QLResult SetSuccessEvent(string mess, QLResultType qLResultType = QLResultType.Success)
        {
            Message = mess;
            ResultType = qLResultType;
            return this;
        }
    }
}
