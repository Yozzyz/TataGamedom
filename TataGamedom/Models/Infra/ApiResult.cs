using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.Infra
{
	public class ApiResult
	{

			public bool IsSuccess { get; private set; }
			public bool IsFail => !IsSuccess;
			public string Message { get; private set; }

			public static ApiResult Success(string succMessage) => new ApiResult { IsSuccess = true, Message = succMessage };
			public static ApiResult Fail(string errorMessage) => new ApiResult { IsSuccess = false, Message = errorMessage };

	}
}