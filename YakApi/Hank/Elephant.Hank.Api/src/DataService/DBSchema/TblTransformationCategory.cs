// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTransformationCategory.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2017-02-21</date>
// <summary>
//     The TblTransformationCategory class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.Collections.Generic;

    /// <summary>
    /// The TblTransformationCategory class
    /// </summary>
    public class TblTransformationCategory : BaseTable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the website identifier.
        /// </summary>
        /// <value>
        /// The website identifier.
        /// </value>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the transformation.
        /// </summary>
        /// <value>
        /// The transformation.
        /// </value>
        public virtual ICollection<TblTransformation> Transformation { get; set; }
    }
}
