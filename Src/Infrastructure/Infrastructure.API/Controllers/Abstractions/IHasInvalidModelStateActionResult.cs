﻿namespace Infrastructure.API.Controllers.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public interface IHasInvalidModelStateActionResult
{
    Func<ModelStateDictionary, IActionResult> InvalidModelState { get; }
}