using CXMDIRECT.AbstractClasses;
using CXMDIRECT.Models;

namespace CXMDIRECT.Controllers
{
    internal class LogController : LogControllerAbstractClass
    {
        internal override async Task<ExceptionLog> Add(Exception exception, List<(string name, string value)> parameters)
        {
            
            try
            {
                ExceptionLogDbModel model = new()
                {
                    ExtensionType = exception.GetType().Name,
                    InstanceDate = DateTime.Now,
                    Parameters = string.Join(", ", parameters.ToArray()),
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                };

                LogDbController logDbController = new();

                await logDbController.Add(model);
                
                return ConvertToExceptionLog(model);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        private static ExceptionLog ConvertToExceptionLog(ExceptionLogDbModel model)
        {
            return new ExceptionLog
            {
                Id = model.Id,
                ExtensionType = model.ExtensionType,
                InstanceDate = model.InstanceDate,
                Parameters = model.Parameters,
                Message = model.ExtensionType == typeof(SecureException).Name ? model.Message : $"{model.Message} ID = {model.Id}",
                StackTrace = model.StackTrace
            };
        }
    }
}
