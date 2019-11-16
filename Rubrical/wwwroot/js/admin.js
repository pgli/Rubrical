// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="admin.js" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Javascript code for the Admin page.</summary>
// ***********************************************************************
$("[name=admin-toggle]").click(function () {
    /// <summary>
    /// When the checkbox next to a user in the Users column of the
    /// Admin control panel is selected, this onclick function will
    /// complete an AJAX request which toggles their administrative rights.
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
            /// Ajax successful callback
            /// </summary>
            /// <param name="data">The data returned when successful.</param>
            location.reload();
        },
        error: function (data) {
            console.log(data);
        }
    });
});

$("[name=admin-delete]").click(function () {
    /// <summary>
    /// When the Delete button next to a user on the Users column
    /// of the Admin panel is click, this onclick function sends
    /// an AJAX request to delete the associated user.
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
            /// Ajax successful callback
            /// </summary>
            /// <param name="data">The data returned when successful.</param>
            location.reload();
        },
        error: function (data) {
            console.log(data);
        }
    });
});