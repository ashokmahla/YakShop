// ---------------------------------------------------------------------------------------------------
// <copyright file="ITransformationService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The ITransformationService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ITransformationService interface
    /// </summary>
    public interface ITransformationService : IBaseService<TblTransformationDto>
    {
        /// <summary>
        /// Gets the by name and category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// the transformation object
        /// </returns>
        ResultMessage<TblTransformationDto> GetByCategoryIdAndValue(long categoryId, string value);

        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>TblTransformationDto object by category id</returns>
        ResultMessage<IEnumerable<TblTransformationDto>> GetByCategoryId(long categoryId);
    }
}
