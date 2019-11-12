var isEditMode = false;

$("#buttonAddRow").on("click", function () {
    if (isEditMode) {
        $.ajax({
            type: "POST",
            url: "/Rubric/AddRow",
            data: JSON.stringify({ RubricId: modelData.Id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log(data);
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
    if (isEditMode) {
        $.ajax({
            type: "POST",
            url: "/Rubric/AddColumn",
            data: JSON.stringify({ RubricId: modelData.Id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log(data);
                $("#rubric").find("tr").each(function (index) {
                    $(this).append(`<td contenteditable=true name='cell' data-cell-id='${data[index]}'></td>`);
                });
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});

$("#buttonEditContents").on("click", function () {
    toggleEditMode();
});

$("body").on("focusout", "[name=cell]", function () {
    var rubricId = modelData.Id;
    var element = $(this);
    var rowId = element.parent().attr("data-row-id");
    var cellId = element.attr("data-cell-id");
    var text = element.text();

    $.ajax({
        type: "POST",
        url: "/Rubric/EditCell",
        data: JSON.stringify({ rubricId: rubricId, rowId: rowId, cellId: cellId, text: text }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
        }
    });
});








function toggleEditMode() {
    if (isEditMode) {
        isEditMode = false;
        $("#buttonEditContents").html("<i class='fa fa-pencil'></i> Edit");
        $(".edit-toggle").each(function () {
            $(this).css("display", "none");
        });

        $("#rubric tr td").each(function () {
            $(this).removeAttr("contenteditable");
        });
    } else {
        isEditMode = true;
        $("#buttonEditContents").html("Leave Edit Mode");
        $(".edit-toggle").each(function () {
            $(this).css("display", "inline");
        });

        $("#rubric tr td").each(function () {
            $(this).attr("contenteditable", "true");
        });
    }
}