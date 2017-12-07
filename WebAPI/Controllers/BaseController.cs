// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;
using ClassLibrary;

namespace WebAPI.Controllers
{
    public abstract class BaseController : ApiController
    {
        public readonly IDataLayerAccess DataLayerAccess;

        public BaseController(IDataLayerAccess dataLayerAccess)
        {
            this.DataLayerAccess = dataLayerAccess;
        }
    }
}