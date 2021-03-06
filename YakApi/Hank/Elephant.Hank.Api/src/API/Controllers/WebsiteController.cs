﻿// ---------------------------------------------------------------------------------------------------
// <copyright file="WebsiteController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The WebsiteController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The WebsiteController class
    /// </summary>
    [RoutePrefix("api/website")]
    public class WebsiteController : BaseApiController
    {
        /// <summary>
        /// The website service
        /// </summary>
        private readonly IWebsiteService websiteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebsiteController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="websiteService">The website service.</param>
        public WebsiteController(
            ILoggerService loggerService,
            IWebsiteService websiteService)
            : base(loggerService)
        {
            this.websiteService = websiteService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblWebsiteDto objects</returns>
        [CustomAuthorize(ActionType = ActionTypes.Read)]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblWebsiteDto>>();
            try
            {
                result = this.websiteService.GetAllUserAuthenticatedWebsites(this.UserId, this.IsAdminUser);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="websiteId">The identifier.</param>
        /// <returns>TblActionDto objects</returns>
        [Route("{websiteId}")]
        [CustomAuthorize(ActionType = ActionTypes.Read)]
        public IHttpActionResult GetById(long websiteId)
        {
            var result = new ResultMessage<TblWebsiteDto>();
            try
            {
                result = this.websiteService.GetById(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="websiteId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{websiteId}")]
        [HttpDelete]
        [CustomAuthorize(Roles = RoleName.TestAdminRole, ActionType = ActionTypes.Delete)]
        public IHttpActionResult DeleteById(long websiteId)
        {
            var result = new ResultMessage<TblWebsiteDto>();
            try
            {
                result = this.websiteService.DeleteById(websiteId, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="websiteDto">The website dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [CustomAuthorize(Roles = RoleName.TestAdminRole, ActionType = ActionTypes.Write)]
        public IHttpActionResult Add([FromBody]TblWebsiteDto websiteDto)
        {
            var data = this.websiteService.GetByName(websiteDto.Name);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + websiteDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(websiteDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="websiteDto">The website dto.</param>
        /// <param name="websiteId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{websiteId}")]
        [HttpPut]
        [CustomAuthorize(Roles = RoleName.TestAdminRole, ActionType = ActionTypes.Write)]
        public IHttpActionResult Update([FromBody]TblWebsiteDto websiteDto, long websiteId)
        {
            var data = this.websiteService.GetByName(websiteDto.Name);

            if (!data.IsError && data.Item != null && websiteId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + websiteDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            websiteDto.Id = websiteId;

            return this.AddUpdate(websiteDto);
        }

        /// <summary>
        /// Get the list of variable type test steps
        /// </summary>
        /// <param name="websiteId">the test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{websiteId}/variable-for-autocomplete")]
        [CustomAuthorize]
        public IHttpActionResult GetAllVariableByTestIdForAutoComplete(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<string>>();
            try
            {
                result = this.websiteService.GetAllVariableByWebsiteIdForAutoComplete(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #region All private

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="websiteDto">The website dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblWebsiteDto websiteDto)
        {
            var result = new ResultMessage<TblWebsiteDto>();
            try
            {
                result = this.websiteService.SaveOrUpdate(websiteDto, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion
    }
}