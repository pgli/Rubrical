// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="site.js" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Global javascript code.</summary>
// ***********************************************************************
function decodeHtml(html) {
    /// <summary>
    /// Decodes HTML passed to it.
    /// </summary>
    /// <param name="html">The HTML.</param>
    var txt = document.createElement("textarea");
    txt.innerHTML = html;
    return txt.value;
}