// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="rubric.js" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Javascript code for the view page of a Rubric object.</summary>
// ***********************************************************************
/// <var>The is edit mode</var>
var isEditMode = false;

$("#buttonAddRow").on("click", function () {
    /// <summary>
    /// When the Add Row button is clicked, send an AJAX request that
    /// 1) adds the appropriate Rubric metadata
    /// 2) displays changes to the DOM
    /// </summary>
    if (isEditMode) {
        $.ajax({
            type: "POST",
            url: "/Rubric/AddRow",
            data: JSON.stringify({ RubricId: modelData.Id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                /// <summary>
                /// AJAX successfull callback
                /// </summary>
                /// <param name="data">JSON array of new Rubric metadata.</param>
                var emptyCells = "";
                for (var i = 0; i < data.cells.length; i++) {
                    var cell = data.cells[i];
                    emptyCells += `<td data-cell-id='${cell.id}' name='cell' contenteditable=true></td>`;
                }
                var newRow = `<tr data-row-id='${data.id}' class='table-primary'>${emptyCells}</tr>`;
                $("#rubric tbody").append(newRow);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});

$("#buttonAddColumn").on("click", function () {
    /// <summary>
    /// When the Add Column button is clicked, send an AJAX request that
    /// 1) adds the appropriate Rubric metadata
    /// 2) displays changes to the DOM
    /// </summary>
    if (isEditMode) {
        $.ajax({
            type: "POST",
            url: "/Rubric/AddColumn",
            data: JSON.stringify({ RubricId: modelData.Id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                /// <summary>
			    /// Ajax successful callback
			    /// </summary>
			    /// <param name="data">JSON array of new Rubric metadata.</param>
                $("#rubric").find("tr").each(function (index) {
                    /// <summary>
                    /// Iterates over all rows
                    /// </summary>
                    /// <param name="index">Index of where to grab data-cell-id from data</param>
                    $(this).append(`<td contenteditable=true name='cell' data-cell-id='${data[index]}'></td>`);
                });
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});

$("#selectPrivacy").on("change", function () {
    /// <summary>
    /// When the selected privacy option has been changed,
    /// send an AJAX request to process this on the backend.
    /// </summary>
    if (isEditMode) {
        $.ajax({
            type: "POST",
            url: "/Rubric/EditPrivacy",
            data: JSON.stringify({ RubricId: modelData.Id, SelectedPrivacy: $("#selectPrivacy").val() }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                /// <summary>
                /// Ajax successful callback
                /// </summary>
                /// <param name="data">A success message.</param>
                console.log(data);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});


$("#buttonEditContents").on("click", function () {
    /// <summary>
    /// When the Edit button is clicked, call toggleEditMode, applying stylistic changes
    /// </summary>
    toggleEditMode();
});

$("#buttonDelete").on("click", function () {
    /// <summary>
    /// When the Delete Rubric button is clicked and a popup has been confirmed,
    /// send and AJAX request that deletes this rubric.
    /// </summary>
    if (confirm("Are you sure you want to delete this rubric?")) {
        $.ajax({
            type: "POST",
            url: "/Rubric/DeleteRubric",
            data: JSON.stringify({ RubricId: modelData.Id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                /// <summary>
                /// AJAX successfull callback.
                /// </summary>
                /// <param name="data">A success message.</param>
                window.location.href = "/Rubric";
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});

$("body").on("focusout", "[name=cell]", function () {
    /// <summary>
    /// When the user is in Edit mode and clicks off of a cell they were typing in, this triggers
    /// Updates contents of this cell on the backend so that the Rubric object saves accordingly
    /// </summary>
    var rubricId = modelData.Id;
    var element = $(this);
    var rowId = element.parent().attr("data-row-id");
    var cellId = element.attr("data-cell-id");
    var text = element[0].innerHTML;

    $.ajax({
        type: "POST",
        url: "/Rubric/EditCell",
        data: JSON.stringify({ rubricId: rubricId, rowId: rowId, cellId: cellId, text: text }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// Ajax successful callback
            /// </summary>
            /// <param name="data">A success message.</param>
            console.log(data);
        },
        error: function (data) {
            console.log(data);
        }
    });
});

$("[name=vote]").click(function () {
    /// <summary>
    /// When an upvote or downvote has been selected,
    /// determine its vote and send an AJAX request to change
    /// this rubric's total rating (and log the user's Rating)
    /// </summary>
    if (!isLoggedIn) {
        $("#mustBeLoggedIn").modal("show");
        return;
    }

    var vote = $(this).attr("data-vote-type") == "up" ? true : false;

    $.ajax({
        type: "POST",
        url: "/Rubric/EditRating",
        data: JSON.stringify({ RubricId: modelData.Id, Value: vote }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// Ajax successfull callback
            /// </summary>
            /// <param name="data">-1 if none were selected, 0 if downvote was selected, 1 if upvote was selected</param>
            $("[data-vote-type=up]").children().removeClass("upvoted");
            $("[data-vote-type=down]").children().removeClass("downvoted");
            if (data.vote == 1) {
                $("[data-vote-type=up]").children().addClass("upvoted");
            } else if (data.vote == 0) {
                $("[data-vote-type=down]").children().addClass("downvoted");
            } else {
                $("[data-vote-type=up]").children().removeClass("upvoted");
                $("[data-vote-type=down]").children().removeClass("downvoted");
            }

            $("#upvoteDiv p").text(data.totalRating);
        },
        error: function (data) {
            console.log(data);
        }
    });
});


function toggleEditMode() {
    /// <summary>
    /// Toggles the edit mode. Applies style changes.
    /// </summary>
    if (isEditMode) {
        // Turn Edit mode off
        isEditMode = false;
        $("#buttonEditContents").html("<i class='fa fa-pencil'></i> Edit");
        $(".edit-toggle").each(function () {
            /// <summary>
            /// Hides elements with the class .edit-toggle
            /// e.g. Add Row, Add Column, Privacy
            /// </summary>
            $(this).css("display", "none");
        });

        $("#rubric tr td").each(function () {
            /// <summary>
            /// Removes contenteditable attribute from cells, making them read-only
            /// </summary>
            $(this).removeAttr("contenteditable");
        });
    } else {
        // Turn Edit mode on
        isEditMode = true;
        $("#buttonEditContents").html("Leave Edit Mode");
        $(".edit-toggle").each(function () {
            /// <summary>
            /// Displays elements with the class .edit-toggle
            /// e.g. Add Row, Add Column, Privacy
            /// </summary>
            $(this).css("display", "inline");
        });

        $("#rubric tr td").each(function () {
            /// <summary>
            /// Adds contenteditable to cell elements, making them read-write
            /// </summary>
            $(this).attr("contenteditable", "true");
        });
    }
}

$("#buttonCopy").click(function () {
    /// <summary>
    /// When the Copy button is clicked, display a modal that prompts user input
    /// </summary>
    if (!isLoggedIn) {
        $("#mustBeLoggedIn").modal("show");
        return;
    }

    $("#myModal").modal("show");
});

$("#modifyForm").submit(function (e) {
    /// <summary>
    /// If the user inputs a title after pressing Copy,
    /// send an AJAX request to create a copy of this Rubric object
    /// under their My Rubrics section, using their custom title
    /// </summary>
    /// <param name="e">Refers to the form submission event that we are preventing</param>
    e.preventDefault();

    var title = $("#copyTitle").val();

    $.ajax({
        type: "POST",
        url: "/Rubric/CopyRubric",
        data: JSON.stringify({ RubricId: modelData.Id, Title: title }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// Ajax successful callback
            /// </summary>
            /// <param name="data">The new Rubric Id of the copied rubric.</param>
            window.location.href = "/Rubric/RubricView?rubricId=" + data;
        },
        error: function (data) {
            console.log(data);
        }
    });
});

var tableToExcel = (function () {
    /// <summary>
    /// Gets all of our rubric table's information and processes it into an Excel file-format
    ///
    /// https://stackoverflow.com/a/38898442
    /// Modified for our purposes, which includes naming it a custom file name
    /// As well as changing displayed output
    /// </summary>
    var regex = /<br\s*[\/]?>/gi;

    var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML.replace(regex, "  ") }

        var element = document.createElement('a');
        element.setAttribute('href', uri + base64(format(template, ctx)));
        element.setAttribute('download', name);

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);
    }
})();



$("#excel").click(function () {
    /// <summary>
    /// When the user clicks the Excel button under the Modify tab, call tableToExcel
    /// First param table id, second param download name
    /// </summary>
    tableToExcel("rubric", `Rubrical_${modelData.DateCreated.split("T")[0]}.xls`);
});

$("#pdf").click(function () {
    /// <summary>
    /// Uses the jsPDF https://github.com/MrRio/jsPDF
    /// When the user clicks on the PDF button under the Modify tab,
    /// process our table contents into an easy-to-read .pdf file format
    /// </summary>
    var doc = new jsPDF('p', 'pt');
    var elem = document.getElementById("rubric");
    var res = doc.autoTableHtmlToJson(elem);
    var ogCols = JSON.parse(JSON.stringify(res.columns));
    var cols = res.columns;
    console.log(cols)
    for (var i = 0; i < cols.length; i++) {
        cols[i].content = "";
    }
    cols[0].content = `${modelData.Title} ${modelData.DateCreated.split("T")[0]}`;
    var data = [ogCols].concat(res.data)
    doc.autoTable(cols, data);
    doc.save(`Rubrical_${modelData.DateCreated.split("T")[0]}.pdf`);
});