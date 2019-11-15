// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="site.js" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
function decodeHtml(html) {
    /// <summary>
    /// Decodes the HTML.
    /// </summary>
    /// <param name="html">The HTML.</param>
    var txt = document.createElement("textarea");
    txt.innerHTML = html;
    return txt.value;
}