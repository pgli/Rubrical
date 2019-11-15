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

$("#selectPrivacy").on("change", function () {
    if (isEditMode) {
        $.ajax({
            type: "POST",
            url: "/Rubric/EditPrivacy",
            data: JSON.stringify({ RubricId: modelData.Id, SelectedPrivacy: $("#selectPrivacy").val() }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log(data);
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

$("#buttonDelete").on("click", function () {
    if (confirm("Are you sure you want to delete this rubric?")) {
        $.ajax({
            type: "POST",
            url: "/Rubric/DeleteRubric",
            data: JSON.stringify({ RubricId: modelData.Id }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                window.location.href = "/Rubric";
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});

$("body").on("focusout", "[name=cell]", function () {
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
            console.log(data);
        },
        error: function (data) {
            console.log(data);
        }
    });
});

$("[name=vote]").click(function () {
    var vote = $(this).attr("data-vote-type") == "up" ? true : false;

    $.ajax({
        type: "POST",
        url: "/Rubric/EditRating",
        data: JSON.stringify({ RubricId: modelData.Id, Value: vote }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);

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