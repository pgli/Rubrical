// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="admin.js" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
$("[name=admin-toggle]").click(function () {
    /// <summary>
    /// 
    /// </summary>
    var el = $(this);
    var userId = el.attr("data-user-id");

    $.ajax({
        type: "POST",
        url: "/Admin/UpdateRole",
        data: JSON.stringify({ Id: userId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
            location.reload();
        },
        error: function (data) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
            console.log(data);
        }
    });
});

$("[name=admin-delete]").click(function () {
    /// <summary>
    /// 
    /// </summary>
    var el = $(this);
    var userId = el.attr("data-user-id");

    $.ajax({
        type: "POST",
        url: "/Admin/DeleteUser",
        data: JSON.stringify({ Id: userId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
            location.reload();
        },
        error: function (data) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
            console.log(data);
        }
    });
});