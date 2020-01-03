// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTransformationDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The TblTransformationDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblTransformationDto class
    /// </summary>
    public class TblTransformationDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the transformation category identifier.
        /// </summary>
        /// <value>
        /// The transformation category identifier.
        /// </value>
        [Required]
        public long TransformationCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the transformation category identifier.
        /// </summary>
        /// <value>
        /// The transformation category identifier.
        /// </value>
        [Required]
        public string TransformedValue { get; set; }
    }
}
