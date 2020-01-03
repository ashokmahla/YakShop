// ---------------------------------------------------------------------------------------------------
// <copyright file="TransformationCategoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The TransformationCategoryService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The TransformationCategoryService class
    /// </summary>
    public class TransformationCategoryService : GlobalService<TblTransformationCategoryDto, TblTransformationCategory>, ITransformationCategoryService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblTransformationCategory> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationCategoryService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TransformationCategoryService(IMapperFactory mapperFactory, IRepository<TblTransformationCategory> table)
            : base(mapperFactory, table)
        {
            this.table = table;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TransformationCategory object
        /// </returns>
        public ResultMessage<TblTransformationCategoryDto> GetByNameAndWebsiteId(string name, long websiteId)
        {
            var result = new ResultMessage<TblTransformationCategoryDto>();

            var entity = this.Table.Find(x => x.Name.ToLower() == name.ToLower() && x.WebsiteId == websiteId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblTransformationCategory, TblTransformationCategoryDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>TblTransformationCategoryDto object</returns>
        public ResultMessage<IEnumerable<TblTransformationCategoryDto>> GetByWebsiteId(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblTransformationCategoryDto>>();

            var entities = this.Table.Find(x => x.WebsiteId == websiteId && x.IsDeleted != true).ToList();

            if (entities == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblTransformationCategory, TblTransformationCategoryDto>();
                result.Item = entities.Select(mapper.Map);
            }

            return result;
        }
    }
}
