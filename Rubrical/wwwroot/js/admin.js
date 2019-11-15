$("[name=admin-toggle]").click(function () {
    var el = $(this);
    var userId = el.attr("data-user-id");

    $.ajax({
        type: "POST",
        url: "/Admin/UpdateRole",
        data: JSON.stringify({ Id: userId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);

            location.reload();
        },
        error: function (data) {
            console.log(data);
        }
    });
});

$("[name=admin-delete]").click(function () {
    var el = $(this);
    var userId = el.attr("data-user-id");

    $.ajax({
        type: "POST",
        url: "/Admin/DeleteUser",
        data: JSON.stringify({ Id: userId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);

            location.reload();
        },
        error: function (data) {
            console.log(data);
        }
    });
});