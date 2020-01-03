// ---------------------------------------------------------------------------------------------------
// <copyright file="ITransformationCategoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The ITransformationCategoryService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ITransformationCategoryService interface
    /// </summary>
    public interface ITransformationCategoryService : IBaseService<TblTransformationCategoryDto>
    {
        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TransformationCategory object
        /// </returns>
        ResultMessage<TblTransformationCategoryDto> GetByNameAndWebsiteId(string name, long websiteId);

        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>TblTransformationCategoryDto object</returns>
        ResultMessage<IEnumerable<TblTransformationCategoryDto>> GetByWebsiteId(long websiteId);
    }
}
