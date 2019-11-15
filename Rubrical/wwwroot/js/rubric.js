// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="rubric.js" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <var>The is edit mode</var>
var isEditMode = false;

$("#buttonAddRow").on("click", function () {
    /// <summary>
    /// 
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
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                var emptyCells = "";
                for (var i = 0; i < data.cells.length; i++) {
                    var cell = data.cells[i];
                    emptyCells += `<td data-cell-id='${cell.id}' name='cell' contenteditable=true></td>`;
                }
                var newRow = `<tr data-row-id='${data.id}' class='table-primary'>${emptyCells}</tr>`;
                $("#rubric tbody").append(newRow);
            },
            error: function (data) {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                console.log(data);
            }
        });
    }
});

$("#buttonAddColumn").on("click", function () {
    /// <summary>
    /// 
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
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                $("#rubric").find("tr").each(function (index) {
                    /// <summary>
                    /// 
                    /// </summary>
                    /// <param name="index">The index.</param>
                    $(this).append(`<td contenteditable=true name='cell' data-cell-id='${data[index]}'></td>`);
                });
            },
            error: function (data) {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                console.log(data);
            }
        });
    }
});

$("#selectPrivacy").on("change", function () {
    /// <summary>
    /// 
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
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                console.log(data);
            },
            error: function (data) {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                console.log(data);
            }
        });
    }
});


$("#buttonEditContents").on("click", function () {
    /// <summary>
    /// 
    /// </summary>
    toggleEditMode();
});

$("#buttonDelete").on("click", function () {
    /// <summary>
    /// 
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
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                window.location.href = "/Rubric";
            },
            error: function (data) {
                /// <summary>
                /// 
                /// </summary>
                /// <param name="data">The data.</param>
                console.log(data);
            }
        });
    }
});

$("body").on("focusout", "[name=cell]", function () {
    /// <summary>
    /// 
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
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
            console.log(data);
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

$("[name=vote]").click(function () {
    /// <summary>
    /// 
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
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
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
            /// <summary>
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
            console.log(data);
        }
    });
});


function toggleEditMode() {
    /// <summary>
    /// Toggles the edit mode.
    /// </summary>
    if (isEditMode) {
        isEditMode = false;
        $("#buttonEditContents").html("<i class='fa fa-pencil'></i> Edit");
        $(".edit-toggle").each(function () {
            /// <summary>
            /// 
            /// </summary>
            $(this).css("display", "none");
        });

        $("#rubric tr td").each(function () {
            /// <summary>
            /// 
            /// </summary>
            $(this).removeAttr("contenteditable");
        });
    } else {
        isEditMode = true;
        $("#buttonEditContents").html("Leave Edit Mode");
        $(".edit-toggle").each(function () {
            /// <summary>
            /// 
            /// </summary>
            $(this).css("display", "inline");
        });

        $("#rubric tr td").each(function () {
            /// <summary>
            /// 
            /// </summary>
            $(this).attr("contenteditable", "true");
        });
    }
}

$("#buttonCopy").click(function () {
    /// <summary>
    /// 
    /// </summary>
    if (!isLoggedIn) {
        $("#mustBeLoggedIn").modal("show");
        return;
    }

    $("#myModal").modal("show");
});

$("#modifyForm").submit(function (e) {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e">The e.</param>
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
            /// 
            /// </summary>
            /// <param name="data">The data.</param>
            window.location.href = "/Rubric/RubricView?rubricId=" + data;
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

//https://stackoverflow.com/questions/38748214/exporting-html-table-to-excel-using-javascript
/// <var>The table to excel</var>
var tableToExcel = (function () {
    /// <summary>
    /// 
    /// </summary>
    var regex = /<br\s*[\/]?>/gi;

    var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">The s.</param>
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="c">The c.</param>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="p">The p.</param>
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="name">The name.</param>
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
    /// 
    /// </summary>
    tableToExcel("rubric", `Rubrical_${modelData.DateCreated.split("T")[0]}.xls`);
});

$("#pdf").click(function () {
    /// <summary>
    /// 
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

    /// <var>The data</var>
    var data = [ogCols].concat(res.data)
    doc.autoTable(cols, data);
    doc.save(`Rubrical_${modelData.DateCreated.split("T")[0]}.pdf`);
});