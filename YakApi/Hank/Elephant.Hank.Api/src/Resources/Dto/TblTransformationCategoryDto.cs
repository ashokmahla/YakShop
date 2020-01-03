// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTransformationCategoryDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The TblTransformationCategoryDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblTransformationCategory class
    /// </summary>
    public class TblTransformationCategoryDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the website identifier.
        /// </summary>
        /// <value>
        /// The website identifier.
        /// </value>
        [Required]
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the transformation.
        /// </summary>
        /// <value>
        /// The transformation.
        /// </value>
        public ICollection<TblTransformationDto> Transformation { get; set; }
    }
}
