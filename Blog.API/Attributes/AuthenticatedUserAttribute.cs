using Blog.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthenticatedUserAttribute() : TypeFilterAttribute(typeof(AuthenticatedUserFilter));