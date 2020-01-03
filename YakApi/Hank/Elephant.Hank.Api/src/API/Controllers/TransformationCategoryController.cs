// ---------------------------------------------------------------------------------------------------
// <copyright file="TransformationCategoryController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The TransformationCategoryController class
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
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The TransformationCategoryController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/transformation-category")]
    public class TransformationCategoryController : BaseApiController
    {
        /// <summary>
        /// The actions service
        /// </summary>
        private readonly ITransformationCategoryService transformationCategoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationCategoryController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="transformationCategoryService">The transformationCategory service.</param>
        public TransformationCategoryController(ILoggerService loggerService, ITransformationCategoryService transformationCategoryService)
            : base(loggerService)
        {
            this.transformationCategoryService = transformationCategoryService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// List of TblTransformationCategoryDto objects
        /// </returns>
        [Route("")]
        public IHttpActionResult GetAll(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblTransformationCategoryDto>>();
            try
            {
                result = this.transformationCategoryService.GetByWebsiteId(websiteId);
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
        /// <param name="transformationCategoryId">The identifier.</param>
        /// <returns>TblTransformationCategoryDto objects</returns>
        [Route("{transformationCategoryId}")]
        [AllowAnonymous]
        public IHttpActionResult GetById(long transformationCategoryId)
        {
            var result = new ResultMessage<TblTransformationCategoryDto>();
            try
            {
                result = this.transformationCategoryService.GetById(transformationCategoryId);
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
        /// <param name="transformationCategoryId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{transformationCategoryId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long transformationCategoryId)
        {
            var result = new ResultMessage<TblTransformationCategoryDto>();
            try
            {
                result = this.transformationCategoryService.DeleteById(transformationCategoryId, this.UserId);
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
        /// <param name="transformationCategoryDto">The transformationCategory dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TblTransformationCategoryDto transformationCategoryDto)
        {
            var data = this.transformationCategoryService.GetByNameAndWebsiteId(transformationCategoryDto.Name, transformationCategoryDto.WebsiteId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "transformationCategory already exists with '" + transformationCategoryDto.Name + "' Name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(transformationCategoryDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="transformationCategoryDto">The transformationCategory dto.</param>
        /// <param name="transformationCategoryId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{transformationCategoryId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblTransformationCategoryDto transformationCategoryDto, long transformationCategoryId)
        {
            var data = this.transformationCategoryService.GetByNameAndWebsiteId(transformationCategoryDto.Name, transformationCategoryDto.WebsiteId);

            if (!data.IsError && data.Item != null && transformationCategoryId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "transformationCategory already exists with '" + transformationCategoryDto.Name + "' Name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            transformationCategoryDto.Id = transformationCategoryId;
            return this.AddUpdate(transformationCategoryDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="transformationCategoryDto">The transformationCategory dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblTransformationCategoryDto transformationCategoryDto)
        {
            var result = new ResultMessage<TblTransformationCategoryDto>();
            try
            {
                result = this.transformationCategoryService.SaveOrUpdate(transformationCategoryDto, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }
    }
}