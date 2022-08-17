using Api.Contract.Validation;
using API_EndPoints.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace API_EndPoints.Configuration
{
    public class TranslateBodyResultServiceFilter : ActionFilterAttribute
    {
        public TranslateBodyResultServiceFilter()
        {

        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = CustomResponseResultForModelState(context.ModelState);
            }
            else if (context.Result is ObjectResult)
            {
                var response = (ObjectResult)context.Result;

                if (IsHttpStatusSupportTranslationContent(response.StatusCode))
                {
                    context.Result = CustomResponseResultForObjectResult(response);
                }

            }
            base.OnResultExecuting(context);
        }


        #region private method
        private ObjectResult CustomResponseResultForModelState(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, ErrorDetailMessage>();
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    if (!errors.ContainsKey(state.Key))
                    {
                        try
                        {
                            var detail = JsonSerializer.Deserialize<ErrorKeyValue>(error.ErrorMessage);
                            errors.Add(state.Key, ErrorDetailMessage.Build(detail.ErrorKey, detail.ErrorParams));
                        }
                        catch (System.Exception)
                        {
                            if (!string.IsNullOrEmpty(state.Key))
                            {
                                errors.Add(state.Key, ErrorDetailMessage.Build(error.ErrorMessage, state.Key));
                            }
                            else
                            {
                                errors.Add("", ErrorDetailMessage.Build(error.ErrorMessage, state.Key));
                            }
                        }
                    }
                }
            }
            var objectResult = GetObjectResult(errors);
            objectResult.StatusCode = StatusCodes.Status400BadRequest;
            return objectResult;
        }

        private ObjectResult GetObjectResult(Dictionary<string, ErrorDetailMessage> errors)
        {
            if (errors.Count > 1)
            {
                var response = ErrorDetailMessages.Build(nameof(ErrorMessages.DataIsNotValid), errors);
                return new ObjectResult(response);
            }
            return new ObjectResult(errors.Count > 0 ? errors.FirstOrDefault().Value : null);
        }

        private ObjectResult CustomResponseResultForObjectResult(ObjectResult responseResult)
        {
            var errors = new Dictionary<string, ErrorDetailMessage>();
            var customeResults = responseResult.Value as Dictionary<string, string[]>;
            if (customeResults != null)
            {
                foreach (var state in customeResults)
                {
                    if (!errors.ContainsKey(state.Key))
                    {
                        try
                        {
                            var detail = JsonSerializer.Deserialize<ErrorKeyValue>(state.Value[0]);
                            errors.Add(state.Key, ErrorDetailMessage.Build(detail.ErrorKey, detail.ErrorParams));
                        }
                        catch (System.Exception)
                        {
                            if (!string.IsNullOrEmpty(state.Key))
                            {
                                errors.Add(state.Key, ErrorDetailMessage.Build(state.Value[0], state.Key));
                            }
                            else
                            {
                                errors.Add("", ErrorDetailMessage.Build(state.Value[0], state.Key));
                                // some error doesn't have key
                            }
                        }
                    }
                }
                var dataResponseResult = GetObjectResult(errors);
                dataResponseResult.StatusCode = responseResult.StatusCode;
                return dataResponseResult;
            }
            else
            {
                return responseResult;
            }
        }

        private bool IsHttpStatusSupportTranslationContent(int? status)
        {
            if (status == null) return false;
            return status == StatusCodes.Status400BadRequest
                                        || status == StatusCodes.Status409Conflict
                                        || status == StatusCodes.Status412PreconditionFailed
                                        || status == StatusCodes.Status404NotFound
                                        || status == StatusCodes.Status304NotModified
                                        || status == StatusCodes.Status403Forbidden;
        }
        #endregion private method
    }
}
