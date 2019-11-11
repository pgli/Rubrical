var isEditMode = false;

$("#buttonAddRow").on("click", function () {
    if (isEditMode) {
        var emptyCells = "";
        for (var i = 0; i < $("#rubric")[0].rows[0].cells.length; i++) {
            emptyCells += "<td contenteditable=true></td>";
        }
        var newRow = "<tr class='table-primary'>" + emptyCells + "</tr>";
        $("#rubric tbody").append(newRow);
    }
});

$("#buttonAddColumn").on("click", function () {
    if (isEditMode) {
        $("#rubric").find("tr").each(function () {
            $(this).append("<td contenteditable=true></td>");
        });
    }
});

$("#buttonEditContents").on("click", function () {
    toggleEditMode();
});

$("#buttonSaveChanges").on("click", function () {
    var myRows = [];
    var rows = $("#rubric tr").each(function (index) {
        cells = $(this).find("td");
        myRows[index] = {};
        cells.each(function (cellIndex) {
            myRows[index][cellIndex] = $(this).html();
        });
    });

    var rubricInfo = {};
    rubricInfo.rows = myRows;

    console.log(`rubricInfo: ${rubricInfo}`);
    console.log(`rubricInfo stringifiy: ${JSON.stringify(rubricInfo)}`);

    $.ajax({
        type: "POST",
        url: "/Rubric/SaveChanges",
        data: JSON.stringify({rows: rubricInfo}), 
        //data: rubricInfo,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            toggleEditMode();
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