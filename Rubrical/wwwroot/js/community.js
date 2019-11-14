$(".grade").on("click", function () {
    modelData.FilterGradeId = $(this).attr("data-grade-id");

    $.ajax({
        type: "POST",
        url: "/Community/FilterByGrade",
        data: JSON.stringify({ FilterGradeId: modelData.FilterGradeId, FilterSubjectId: modelData.FilterSubjectId, Rubrics: modelData.Rubrics, Subjects: modelData.Subjects, Grades: modelData.Grades }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data.rubrics);
            var rubricsData = "";
            for (var i = 0; i < data.rubrics.length; i++) {
                var rubric = data.rubrics[i];
                rubricsData +=
                    `<div class="col-sm-6">
                    <div class="card">
                        <div class="card-body">
                            <p id="rating">+${rubric.totalRating}</p>
                            <h5 class="card-title"><a href="/Rubric/RubricView?rubricId=@rubric.Id" class="text-white">${rubric.title}</a></h5>
                            <sub class="text-light">@rubric.Grade.GradeName @rubric.Subject.SubjectName</sub>
                            <br /><br />
                            <p class="card-text">@(rubric.Description == null ? "No description." : rubric.Description)</p>
                            <a href="/Rubric/RubricView?rubricId=@rubric.Id" class="btn btn-primary">View Rubric</a>
                            <br /><br />
                            <small class="text-light"><em>Created by ${rubric.applicationUserId}</em></small>
                            <br />
                            <small class="text-light"><em>${rubric.dateCreated}</em></small>
                        </div>
                    </div>
                </div>`;
            }
            $("#rubrics").html(rubricsData);
        },
        error: function (data) {
            console.log(data);
        }
    });
});

$(".subject").on("click", function () {
    console.log("clicked subj");
    console.log(this);
});