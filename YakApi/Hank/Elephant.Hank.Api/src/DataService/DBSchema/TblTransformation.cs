// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTransformation.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The TblTransformation class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The TblTransformation class
    /// </summary>
    public class TblTransformation : BaseTable
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

        /// <summary>
        /// Gets or sets the transformation category.
        /// </summary>
        /// <value>
        /// The transformation category.
        /// </value>
        [ForeignKey("TransformationCategoryId")]
        public virtual TblTransformationCategory TransformationCategory { get; set; }
    }
}
