// ---------------------------------------------------------------------------------------------------
// <copyright file="TransformationService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The TransformationService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
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
    ///  The TransformationService class
    /// </summary>
    public class TransformationService : GlobalService<TblTransformationDto, TblTransformation>, ITransformationService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblTransformation> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TransformationService(IMapperFactory mapperFactory, IRepository<TblTransformation> table)
            : base(mapperFactory, table)
        {
            this.table = table;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the by name and category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>returns TblTransformationDto object</returns>
        public ResultMessage<TblTransformationDto> GetByCategoryIdAndValue(long categoryId, string value)
        {
            var result = new ResultMessage<TblTransformationDto>();

            var entity = this.Table.Find(x => x.Value.ToLower() == value.ToLower() && x.TransformationCategoryId == categoryId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblTransformation, TblTransformationDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>TblTransformationDto object by category id</returns>
        public ResultMessage<IEnumerable<TblTransformationDto>> GetByCategoryId(long categoryId)
        {
            var result = new ResultMessage<IEnumerable<TblTransformationDto>>();

            var entities = this.Table.Find(x => x.TransformationCategoryId == categoryId && x.IsDeleted != true).ToList();

            if (entities == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblTransformation, TblTransformationDto>();
                result.Item = entities.Select(mapper.Map);
            }

            return result;
        }
    }
}
