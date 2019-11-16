// ***********************************************************************
// Assembly         : Rubrical
// Author           : Petar Gligic
// Created          : 11-15-2019
//
// Last Modified By : Petar Gligic
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="community.js" company="Rubrical">
//     Copyright (c)Rubrical. All rights reserved.
// </copyright>
// <summary>Javascript code for the Community Rubrics page.</summary>
// ***********************************************************************
$(".grade").on("click", function () {
    /// <summary>
    /// When a grade is selected, style accordingly then send an
    /// AJAX request that filters rubrics by the selected/unselected grade.
    /// </summary>
    var selectedGradeId = $(this).attr("data-grade-id");
    $(".grade").removeClass("text-info");

    if (modelData.FilterGradeId == selectedGradeId) {
        modelData.FilterGradeId = 0;
        $(this).removeClass("text-info");
    } else {
        $(this).addClass("text-info");
        modelData.FilterGradeId = selectedGradeId;
    }

    var sortType = $("#selectSort option:selected").val();

    $.ajax({
        type: "POST",
        url: "/Community/FilterRubrics",
        data: JSON.stringify({ FilterGradeId: modelData.FilterGradeId, FilterSubjectId: modelData.FilterSubjectId, Rubrics: modelData.Rubrics, Subjects: modelData.Subjects, Grades: modelData.Grades, SortType: sortType }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// Ajax successful callback
            /// </summary>
            /// <param name="data">Returns a JSON array of filtered rubrics.</param>
            var rubricsData = "";
            for (var i = 0; i < data.length; i++) {
                var rubric = data[i];
                var desc = (rubric.description == null) ? "No description." : rubric.description;
                rubricsData +=
                    `<div class="col-sm-6">
                    <div class="card border-info">
                        <div class="card-body">
                            <p id="rating">${rubric.totalRating}</p>
                            <h5 class="card-title"><a href="/Rubric/RubricView?rubricId=${rubric.rubricId}" class="text-white">${rubric.title}</a></h5>
                            <sub class="text-light">${rubric.grade.gradeName} ${rubric.subject.subjectName}</sub>
                            <br /><br />
                            <p class="card-text">${desc}</p>
                            <a href="/Rubric/RubricView?rubricId=${rubric.rubricId}" class="btn btn-primary">View Rubric</a>
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

$("#selectSort").change(function () {
    /// <summary>
    /// When a type of sort is selected from the dropdown, such as Upvote Low-High,
    /// grab its value and pass it to an AJAX request to sort rubrics by.
    /// </summary>
    var sortType = $("#selectSort option:selected").val();

    $.ajax({
        type: "POST",
        url: "/Community/FilterRubrics",
        data: JSON.stringify({ FilterGradeId: modelData.FilterGradeId, FilterSubjectId: modelData.FilterSubjectId, Rubrics: modelData.Rubrics, Subjects: modelData.Subjects, Grades: modelData.Grades, SortType: sortType }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// Ajax successful callback
            /// </summary>
            /// <param name="data">Returns a JSON array of sorted rubrics.</param>
            console.log(data);
            var rubricsData = "";
            for (var i = 0; i < data.length; i++) {
                var rubric = data[i];
                var desc = (rubric.description == null) ? "No description." : rubric.description;
                rubricsData +=
                    `<div class="col-sm-6">
                    <div class="card border-info">
                        <div class="card-body">
                            <p id="rating">${rubric.totalRating}</p>
                            <h5 class="card-title"><a href="/Rubric/RubricView?rubricId=${rubric.rubricId}" class="text-white">${rubric.title}</a></h5>
                            <sub class="text-light">${rubric.grade.gradeName} ${rubric.subject.subjectName}</sub>
                            <br /><br />
                            <p class="card-text">${desc}</p>
                            <a href="/Rubric/RubricView?rubricId=${rubric.rubricId}" class="btn btn-primary">View Rubric</a>
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
    /// <summary>
    /// When a subject is selected, style accordingly then send an
    /// AJAX request that filters rubrics by the selected/unselected subject.
    /// </summary>
    var selectedSubjectId = $(this).attr("data-subject-id");
    $(".subject").removeClass("text-info");

    if (modelData.FilterSubjectId == selectedSubjectId) {
        modelData.FilterSubjectId = 0;
        $(this).removeClass("text-info");
    } else {
        $(this).addClass("text-info");
        modelData.FilterSubjectId = selectedSubjectId;
    }

    var sortType = $("#selectSort option:selected").val();

    $.ajax({
        type: "POST",
        url: "/Community/FilterRubrics",
        data: JSON.stringify({ FilterGradeId: modelData.FilterGradeId, FilterSubjectId: modelData.FilterSubjectId, Rubrics: modelData.Rubrics, Subjects: modelData.Subjects, Grades: modelData.Grades, SortType: sortType }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            /// <summary>
            /// Ajax successful callback
            /// </summary>
            /// <param name="data">Returns a JSON array of filtered rubrics.</param>
            var rubricsData = "";
            for (var i = 0; i < data.length; i++) {
                var rubric = data[i];
                var desc = (rubric.description == null) ? "No description." : rubric.description;
                rubricsData +=
                    `<div class="col-sm-6">
                    <div class="card border-info">
                        <div class="card-body">
                            <p id="rating">${rubric.totalRating}</p>
                            <h5 class="card-title"><a href="/Rubric/RubricView?rubricId=${rubric.rubricId}" class="text-white">${rubric.title}</a></h5>
                            <sub class="text-light">${rubric.grade.gradeName} ${rubric.subject.subjectName}</sub>
                            <br /><br />
                            <p class="card-text">${desc}</p>
                            <a href="/Rubric/RubricView?rubricId=${rubric.rubricId}" class="btn btn-primary">View Rubric</a>
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