// ---------------------------------------------------------------------------------------------------
// <copyright file="TransformationController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TransformationController class
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
    /// The TransformationController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/transformation-category/{transformationCategoryId}/transformation")]
    public class TransformationController : BaseApiController
    {
        /// <summary>
        /// The actions service
        /// </summary>
        private readonly ITransformationService transformationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="transformationService">The transformation service.</param>
        public TransformationController(ILoggerService loggerService, ITransformationService transformationService)
            : base(loggerService)
        {
            this.transformationService = transformationService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="transformationCategoryId">The transformation category identifier.</param>
        /// <returns>
        /// List of TblTransformationDto objects
        /// </returns>
        [Route("")]
        public IHttpActionResult GetAll(long transformationCategoryId)
        {
            var result = new ResultMessage<IEnumerable<TblTransformationDto>>();
            try
            {
                result = this.transformationService.GetByCategoryId(transformationCategoryId);
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
        /// <param name="transformationId">The identifier.</param>
        /// <returns>TblTransformationDto objects</returns>
        [Route("{transformationId}")]
        public IHttpActionResult GetById(long transformationId)
        {
            var result = new ResultMessage<TblTransformationDto>();
            try
            {
                result = this.transformationService.GetById(transformationId);
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
        /// <param name="transformationId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{transformationId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long transformationId)
        {
            var result = new ResultMessage<TblTransformationDto>();
            try
            {
                result = this.transformationService.DeleteById(transformationId, this.UserId);
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
        /// <param name="transformationDto">The transformation dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TblTransformationDto transformationDto)
        {
            var data = this.transformationService.GetByCategoryIdAndValue(transformationDto.TransformationCategoryId, transformationDto.Value);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "transformation already exists with '" + transformationDto.Value + "' Value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(transformationDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="transformationDto">The transformation dto.</param>
        /// <param name="transformationId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{transformationId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblTransformationDto transformationDto, long transformationId)
        {
            var data = this.transformationService.GetByCategoryIdAndValue(transformationDto.TransformationCategoryId, transformationDto.Value);

            if (!data.IsError && data.Item != null && transformationId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "transformation already exists with '" + transformationDto.Value + "' Value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            transformationDto.Id = transformationId;
            return this.AddUpdate(transformationDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="transformationDto">The transformation dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblTransformationDto transformationDto)
        {
            var result = new ResultMessage<TblTransformationDto>();
            try
            {
                result = this.transformationService.SaveOrUpdate(transformationDto, this.UserId);
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