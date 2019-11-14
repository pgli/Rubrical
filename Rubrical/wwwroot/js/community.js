$(".grade").on("click", function () {
    var selectedGradeId = $(this).attr("data-grade-id");
    $(".grade").removeClass("active");

    if (modelData.FilterGradeId == selectedGradeId) {
        modelData.FilterGradeId = 0;
        $(this).removeClass("active");
    } else {
        $(this).addClass("active");
        modelData.FilterGradeId = selectedGradeId;
    }

    $.ajax({
        type: "POST",
        url: "/Community/FilterRubrics",
        data: JSON.stringify({ FilterGradeId: modelData.FilterGradeId, FilterSubjectId: modelData.FilterSubjectId, Rubrics: modelData.Rubrics, Subjects: modelData.Subjects, Grades: modelData.Grades }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            var rubricsData = "";
            for (var i = 0; i < data.length; i++) {
                var rubric = data[i];
                var desc = (rubric.description == null) ? "No description." : rubric.description;
                rubricsData +=
                    `<div class="col-sm-6">
                    <div class="card">
                        <div class="card-body">
                            <p id="rating">+${rubric.totalRating}</p>
                            <h5 class="card-title"><a href="/Rubric/RubricView?rubricId=${rubric.id}" class="text-white">${rubric.title}</a></h5>
                            <sub class="text-light">${rubric.grade.gradeName} ${rubric.subject.subjectName}</sub>
                            <br /><br />
                            <p class="card-text">${desc}</p>
                            <a href="/Rubric/RubricView?rubricId=${rubric.id}" class="btn btn-primary">View Rubric</a>
                            <br /><br />
                            <small class="text-light"><em>Created by ${rubric.userName}</em></small>
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
    var selectedSubjectId = $(this).attr("data-subject-id");
    $(".subject").removeClass("active");

    console.log(modelData.FilterSubjectId)
    console.log(selectedSubjectId)
    if (modelData.FilterSubjectId == selectedSubjectId) {
        console.log("a")
        modelData.FilterSubjectId = 0;
        $(this).removeClass("active");
    } else {
        console.log("b")
        $(this).addClass("active");
        modelData.FilterSubjectId = selectedSubjectId;
    }

    $.ajax({
        type: "POST",
        url: "/Community/FilterRubrics",
        data: JSON.stringify({ FilterGradeId: modelData.FilterGradeId, FilterSubjectId: modelData.FilterSubjectId, Rubrics: modelData.Rubrics, Subjects: modelData.Subjects, Grades: modelData.Grades }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            var rubricsData = "";
            for (var i = 0; i < data.length; i++) {
                var rubric = data[i];
                var desc = (rubric.description == null) ? "No description." : rubric.description;
                rubricsData +=
                    `<div class="col-sm-6">
                    <div class="card">
                        <div class="card-body">
                            <p id="rating">+${rubric.totalRating}</p>
                            <h5 class="card-title"><a href="/Rubric/RubricView?rubricId=${rubric.id}" class="text-white">${rubric.title}</a></h5>
                            <sub class="text-light">${rubric.grade.gradeName} ${rubric.subject.subjectName}</sub>
                            <br /><br />
                            <p class="card-text">${desc}</p>
                            <a href="/Rubric/RubricView?rubricId=${rubric.id}" class="btn btn-primary">View Rubric</a>
                            <br /><br />
                            <small class="text-light"><em>Created by ${rubric.userName}</em></small>
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